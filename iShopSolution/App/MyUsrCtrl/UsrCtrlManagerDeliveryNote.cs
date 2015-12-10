using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Business.Entity;
using Business.IRepository;
using Business.Repository;

namespace App.MyUsrCtrl
{
    public partial class UsrCtrlManagerDeliveryNote : MyUserControl
    {
        private DateTime _from;
        private DateTime _to;
        private int _status;

        private IEnumerable<DeliveryNote> _list;
        private IDeliveryNoteRepository _deliveryNoteRepository;
        public UsrCtrlManagerDeliveryNote()
        {
            InitializeComponent();
            _deliveryNoteRepository = new DeliveryNoteRepository();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            IsRemove = true;
        }

        private void UsrCtrlManagerDeliveryNote_Load(object sender, EventArgs e)
        {
            Reload();
            dtFrom.Value = DateTime.Now.AddYears(-1);
            dtTo.Value = DateTime.Now;
            _from = dtFrom.Value;
            _to = dtTo.Value;
            _status = -1;
        }
        public void Reload()
        {
            LoadCboStatus();
            //LoadLstNote();
            EmtySubItemlstDetails();
        }

        private void LoadLstNote(IEnumerable<DeliveryNote> list = null)
        {
            if (list == null)
            {
                _list = _deliveryNoteRepository.GetAll();
                list = _list;
            }


            lstNote.Items.Clear();

            var averPrice = new WeightedAverageUnitPriceRepository();
            foreach (var note in list)
            {
                var item = new ListViewItem(note.StrStatus) { Tag = note.Id };
                item.SubItems.Add(String.Format("{0:dd/MM/yyyy HH:mm:ss}", note.Date));
                item.SubItems.Add(note.NoteDetails
                                      .Count().ToString(CultureInfo.InvariantCulture));
                item.SubItems.Add(note.NoteDetails
                                      .Sum(d => d.Unit).ToString(CultureInfo.InvariantCulture));

                var totalPrice = note.NoteDetails.Sum(detail => detail.Unit * averPrice.GetPrice(detail.ProductId));
                item.SubItems.Add(string.Format("{0:0,0 VND}", totalPrice));
                lstNote.Items.Add(item);
            }
        }

        private void LoadCboStatus()
        {
            var list = new Dictionary<int, string> { { -1, "All" }, { 0, "Waiting" }, { 1, "Process" } }.ToList();
            cboStatus.DataSource = list;
        }

        private void EmtySubItemlstDetails(bool ishave = false)
        {

            foreach (var item in lstDetails.Items.Cast<ListViewItem>())
            {
                if (!ishave)
                    item.SubItems.Add(string.Empty);
                else
                    item.SubItems[1].Text = string.Empty;
            }

        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            _status = (int)cboStatus.SelectedValue;
            FilterNote();
        }


        private void FilterNote()
        {
            try
            {
                //var status = (int)cboStatus.SelectedValue;
                _list = _deliveryNoteRepository.GetAll().ToList();
                switch (_status)
                {
                    case 0:
                        _list = _list.Where(n => !n.Status).ToList();
                        break;
                    case 1:
                        _list = _list.Where(n => n.Status).ToList();
                        break;
                    default:
                        _list = _deliveryNoteRepository.GetAll().ToList();
                        break;

                }

                _list = _list.Where(n =>
                                    DateTime.Compare(n.Date, _from) >= 0 &&
                                    DateTime.Compare(n.Date, _to) <= 0);
                LoadLstNote(_list);
            }
            catch (Exception exception)
            {
                Debug.WriteLine("filter note: " + exception.Message);
            }

        }

        private void dtFrom_ValueChanged(object sender, EventArgs e)
        {
            _from = dtFrom.Value;
            FilterNote();
        }

        private void dtTo_ValueChanged(object sender, EventArgs e)
        {
            _to = dtTo.Value;
            FilterNote();
        }

        private void lstNote_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstNote.SelectedItems.Count < 1) return;

            var item = lstNote.SelectedItems[0];
            if (item == null) return;

            var noteId = (int)item.Tag;
            var note = _list.FirstOrDefault(n => n.Id == noteId);
            if (note == null) return;

            lstDetails.Tag = noteId;
            for (var i = 0; i < lstDetails.Items.Count; i++)
            {
                lstDetails.Items[i].SubItems[1].Text = item.SubItems[i].Text;
            }

            LoadLstNoteDetails(note.NoteDetails);
        }

        private void LoadLstNoteDetails(IEnumerable<DeliveryNoteDetail> list)
        {
            if (list == null) return;
            lstNoteDetail.Items.Clear();
            var averPrice = new WeightedAverageUnitPriceRepository();
            foreach (var detail in list)
            {
                var item = new ListViewItem(detail.Product.Name) { Tag = detail.Id };
                item.SubItems.Add(detail.Unit.ToString(CultureInfo.InvariantCulture));

                var price = averPrice.GetPriceByDate(detail.ProductId, detail.DeliveryNote.Date);
                var strPrice = price > 0 ? string.Format("{0:0,0 VND}", price) : "unidentified";
                var strCostPrice = price > 0 ? string.Format("{0:0,0 VND}", detail.Unit * price) : "unidentified";
                item.SubItems.Add(strPrice);
                item.SubItems.Add(strCostPrice);

                for (var i = 1; i <= 2; i++)
                {
                    var repo = detail.Repos.Where(r => r.Repository == i).ToList();
                    item.SubItems.Add(repo.Any() ? string.Format("{0:0,0}", repo.Sum(r => r.ReceiptUnit)) : string.Empty);
                }

                lstNoteDetail.Items.Add(item);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dtFrom.Value = DateTime.Now.AddYears(-1);
            dtTo.Value = DateTime.Now;
            _from = dtFrom.Value;
            _to = dtTo.Value;
            _status = -1;
            FilterNote();
            
            cboStatus.SelectedIndex = 0;
            EmtySubItemlstDetails(true);
            lstNoteDetail.Items.Clear();

            lstNote.Items.Clear();

        }
    }
}
