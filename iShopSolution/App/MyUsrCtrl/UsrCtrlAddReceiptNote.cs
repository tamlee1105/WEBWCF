using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Business.Entity;
using Business.IRepository;
using Business.Repository;

namespace App.MyUsrCtrl
{
    public partial class UsrCtrlAddReceiptNote : MyUserControl
    {
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;
        private ReceiptNote _note;
        public UsrCtrlAddReceiptNote()
        {
            InitializeComponent();
            _categoryRepository = new CategoryRepository();
            _productRepository = new ProductRepository();
            //_note = new ReceiptNote();
        }

        public UsrCtrlAddReceiptNote(ReceiptNote note)
            : this()
        {
            _note = note;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            IsRemove = true;
        }

        private void UsrCtrlReceiptNote_Load(object sender, EventArgs e)
        {
            try
            {
                LoadCboCategory();
                LoadCboRepository();
                LoadListViewProductSearch();
                LoadListViewNoteDetails();
            }
            catch (Exception exception)
            {
                Utilities.ShowMessageError("Error: " + exception.Message);
            }

        }

        private void LoadListViewNoteDetails()
        {
            if (_note.ReceiptNoteDetails == null || !_note.ReceiptNoteDetails.Any()) return;

            lstReceiptNoteDetail.Items.Clear();

            foreach (var noteDetail in _note.ReceiptNoteDetails)
            {
                var item = new ListViewItem(noteDetail.Product.Name) { Tag = noteDetail.ProductId };
                item.SubItems[0].Tag = noteDetail.Id;
                item.SubItems.Add(string.Format("{0:0,0}", noteDetail.Unit));
                item.SubItems.Add(string.Format("{0:0,0}", noteDetail.CostPrice));
                item.SubItems.Add(noteDetail.Repository == 1 ? "A" : "B");
                lstReceiptNoteDetail.Items.Add(item);
            }

            if (lstReceiptNoteDetail.Items.Count < 1) return;

            lstReceiptNoteDetail.Items[0].Selected = true;
            lstvProduct.Enabled = false;
            btnSearch.Enabled = false;

        }

        private void LoadCboCategory()
        {
            var list = _categoryRepository.GetAll().ToList();
            list.Insert(0, new Category { Id = 0, Name = "All category" });
            cboCategory.DataSource = list;
        }

        private void LoadCboRepository()
        {
            var list = new Dictionary<byte, string> { { 1, "A" }, { 2, "B" } };
            cboRepository.DataSource = list.ToList();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            txtProductName.Text = string.Empty;
            txtProductName.Tag = null;
            var list = _productRepository.Find(txtKeyWord.Text, (int)cboCategory.SelectedValue);
            if (!list.Any())
            {
                Utilities.ShowMessage("No product match key search\nPlease try again!!!");
                //return;
            }

            LoadListViewProductSearch(list);
        }

        private void LoadListViewProductSearch(IEnumerable<Product> list = null)
        {
            lstvProduct.Items.Clear();
            if (list == null)
                list = _productRepository.GetAll();
            foreach (var p in list)
            {
                AddItemInlstProduct(p);
            }
        }

        private void AddItemInlstProduct(Product product)
        {
            var item = new ListViewItem(product.Name) { Tag = product.Id };
            item.SubItems.Add(product.CateName);
            lstvProduct.Items.Add(item);
        }

        private void lstvProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstvProduct.SelectedItems.Count < 1) return;

            txtUnit.Text = "0";
            txtCostPrice.Text = "0";
            var item = lstvProduct.SelectedItems[0];
            txtProductName.Text = item.SubItems[0].Text;
            txtProductName.Tag = item.Tag;

            EnableAddButton(true);
        }

        private void EnableAddButton(bool enable)
        {
            btnAdd.Enabled = enable;
            EnableUpdateDelete(!enable);
        }

        private void EnableUpdateDelete(bool enable)
        {
            btnUpdate.Enabled = enable;
            btnDelete.Enabled = enable;
            cboRepository.Enabled = !enable;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsVaild()) return;
                var proId = txtProductName.Tag;
                if (proId == null || ((int)proId) == 0)
                {
                    Utilities.ShowMessageError("Please choose product in list Product");
                    return;
                }

                var noteDetail = CreateNoteDetail();

                if (!UpdateNoteDetailSameRepository(noteDetail.ProductId, noteDetail.Repository))
                {
                    _note.ReceiptNoteDetails.Add(noteDetail);
                    AddItemInListReceiptNoteDetail(noteDetail);
                }

                ResetTextBoxNoteDetail();
                btnAdd.Enabled = false;
            }
            catch (Exception exception)
            {
                Utilities.ShowMessageError("Error: " + exception.Message);
            }

        }


        private bool UpdateNoteDetailSameRepository(int proId, byte repository)
        {
            var index = _note.ReceiptNoteDetails
                .FindIndex(d => d.ProductId == proId && d.Repository == repository);
            if (index < 0) return false;

            var current = _note.ReceiptNoteDetails[index];
            current.Unit += Convert.ToInt32(txtUnit.Text);
            current.CostPrice = Convert.ToDecimal(txtCostPrice.Text);

            UpdateNoteDetail(current, index);
            return true;
        }

        private ReceiptNoteDetail CreateNoteDetail()
        {
            return new ReceiptNoteDetail
                       {
                           //ReceiptNoteId = _note.Id,
                           ProductId = (int)txtProductName.Tag,
                           Unit = Convert.ToInt32(txtUnit.Text),
                           CostPrice = Convert.ToDecimal(txtCostPrice.Text),
                           Repository = (byte)cboRepository.SelectedValue
                       };
        }

        private void AddItemInListReceiptNoteDetail(ReceiptNoteDetail noteDetail, int index = -1)
        {
            var item = new ListViewItem(txtProductName.Text) { Tag = noteDetail.ProductId };
            item.SubItems.Add(string.Format("{0:0,0}", noteDetail.Unit));
            item.SubItems.Add(string.Format("{0:0,0}", noteDetail.CostPrice));
            item.SubItems.Add(cboRepository.Text);
            if (index > -1)
            {
                lstReceiptNoteDetail.Items.RemoveAt(index);
                lstReceiptNoteDetail.Items.Insert(index, item);

            }
            else
            {
                lstReceiptNoteDetail.Items.Add(item);
            }

        }

        private void txtCostPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            JustPressNumber(e);
        }

        private void txtUnit_KeyPress(object sender, KeyPressEventArgs e)
        {
            JustPressNumber(e);
        }

        private void JustPressNumber(KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void ResetTextBoxNoteDetail()
        {
            txtProductName.Text = string.Empty;
            txtProductName.Tag = null;
            txtUnit.Text = "0";
            txtCostPrice.Text = "0";
            lstvProduct.SelectedItems.Clear();
        }



        private void lstReceiptNoteDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstReceiptNoteDetail.SelectedItems.Count < 1) return;

            var item = lstReceiptNoteDetail.SelectedItems[0];

            if (item.Tag == null) return;
            var proId = (int)item.Tag;
            if (proId <= 0) return;

            txtProductName.Text = item.SubItems[0].Text;
            txtProductName.Tag = proId;
            txtUnit.Text = item.SubItems[1].Text.Replace(",", string.Empty);
            txtCostPrice.Text = item.SubItems[2].Text.Replace(",", string.Empty);
            cboRepository.Text = item.SubItems[3].Text;

            EnableAddButton(false);

        }

        private void txtKeyWord_TextChanged(object sender, EventArgs e)
        {
            if (txtKeyWord.Text.Length >= 1) return;
            cboCategory.SelectedIndex = 0;
            LoadListViewProductSearch();
        }

        private bool IsVaild()
        {
            if (txtUnit.Text.Trim().Length < 1 ||
                txtUnit.Text.Trim().Equals("0") ||
                txtCostPrice.Text.Trim().Length < 1 ||
                txtCostPrice.Text.Trim().Equals("0"))
            {
                Utilities.ShowMessageError("Something is wrong/invaild.\nPlease check and try again");
                return false;
            }
            return true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstReceiptNoteDetail.SelectedItems.Count < 1)
                {
                    Utilities.ShowMessageError("Please slected one row");
                    return;
                }

                if (!IsVaild()) return;
                var item = lstReceiptNoteDetail.SelectedItems[0];
                var index = item.Index;

                var noteDetails = CreateNoteDetail();
                if (item.SubItems[0].Tag != null)
                    noteDetails.Id = (int)item.SubItems[0].Tag;

                UpdateNoteDetail(noteDetails, index);

                EnableUpdateDelete(false);
                ResetTextBoxNoteDetail();
            }
            catch (Exception exception)
            {
                Utilities.ShowMessageError("Error: " + exception.Message);

            }
        }

        private void UpdateNoteDetail(ReceiptNoteDetail noteDetails, int index)
        {

            _note.ReceiptNoteDetails.RemoveAt(index);
            _note.ReceiptNoteDetails.Insert(index, noteDetails);
            AddItemInListReceiptNoteDetail(noteDetails, index);


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstReceiptNoteDetail.SelectedItems.Count < 1)
                {
                    Utilities.ShowMessageError("Please slected one row");
                    return;
                }

                if (Utilities.ShowQuestionMessage("Do you want to delete?") == DialogResult.No) return;

                foreach (var item in lstReceiptNoteDetail.SelectedItems.Cast<ListViewItem>())
                {
                    _note.ReceiptNoteDetails.RemoveAt(item.Index);
                    lstReceiptNoteDetail.Items.Remove(item);
                }
                EnableUpdateDelete(false);
                ResetTextBoxNoteDetail();
            }
            catch (Exception exception)
            {
                Utilities.ShowMessageError("Error: " + exception.Message);
            }


        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!_note.ReceiptNoteDetails.Any())
            {
                Utilities.ShowMessageError("Receipt note is empty");
                return;
            }
            try
            {

                var receiptNoteRepository = new ReceiptNoteRepository();
                if (_note.Id == 0)
                {
                    _note.Date = DateTime.Now;
                    _note.Status = true;
                    receiptNoteRepository.Add(_note);
                }
                else
                {
                    if (_note.ReceiptNoteDetails.Any(
                        noteDetail => noteDetail.Unit < 1 ||
                        noteDetail.CostPrice < 1))
                    {
                        Utilities.ShowMessageError("Receipt Note Details is invaild");
                        return;
                    }
                    receiptNoteRepository.Update(_note);
                }
                
                Utilities.ShowMessage("Save ok");
                _note = new ReceiptNote();
                lstReceiptNoteDetail.Items.Clear();
                ResetTextBoxNoteDetail();

            }
            catch (Exception exception)
            {
                Utilities.ShowMessageError("Error: " + exception.Message);
            }
        }
    }
}
