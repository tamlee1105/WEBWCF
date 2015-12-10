using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Business.Entity;
using Business.Repository;

namespace App.MyUsrCtrl
{
    public partial class UsrCtrlStatistics : MyUserControl
    {
        public struct Repository
        {
            public string Name { get; set; }
            public int Unit { get; set; }
        }

        struct Statistic
        {
            public string ProductName { get; set; }
            public int Units { get; set; }
            public decimal AverUnitPrice { get; set; }
            private List<int> _stockUnits;
            public List<int> StockUnits
            {
                get { return _stockUnits ?? (_stockUnits = new List<int>()); }
            }

            public DateTime Date { get; set; }
        }
        public UsrCtrlStatistics()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            IsRemove = true;
        }

        private void UsrCtrlStatistics_Load(object sender, EventArgs e)
        {
            LoadCboCate();
            dtAt.Value = DateTime.Now;
        }

        private void LoadCboCate()
        {
            try
            {
                var repository = new CategoryRepository();
                var list = repository.GetAll().ToList();
                list.Insert(0, new Category { Id = 0, Name = "All" });
                cboCate.DataSource = list;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Load cbo cate: "+exception.Message);
            }
            
        }

        private void btnStatistics_Click(object sender, EventArgs e)
        {
            lstResult.Items.Clear();

            var productRepository = new ProductRepository();
            var cateId = cboCate.SelectedValue is int ? (int)cboCate.SelectedValue : 0;
            var productName = txtProductName.Text;
            var products = productRepository.Find(productName, cateId);

            var date = dtAt.Value;
            var averPriceReository = new WeightedAverageUnitPriceRepository();
            var repository = new MyRepository();

            var list = new List<Statistic>();
            foreach (var p in products.ToList())
            {
                var averPrice = averPriceReository.GetByDate(p.Id, date);
                if (averPrice == null) continue;
                var item = new Statistic
                               {
                                   ProductName = p.Name,
                                   AverUnitPrice = averPrice.AverageUnitPrice,
                                   Units = averPrice.Units,
                                   Date = averPrice.Date
                               };
                var repos = repository.GetRepoProduct(p.Id, date);
                foreach (var repo in repos)
                {
                    item.StockUnits.Add(repo.StockUnit);
                }
                list.Add(item);
            }

            LoadDataListView(list);


        }

        private int _stt;
        private void LoadDataListView(IEnumerable<Statistic> list)
        {
            _stt = 1;
            lstResult.Items.Clear();
            foreach (var sts in list)
            {
                var item = new ListViewItem("" + _stt++);
                item.SubItems.Add(sts.ProductName);

                item.SubItems.Add(String.Format("{0:0,0}", sts.Units));
                item.SubItems.Add(String.Format("{0:0,0 VND}", sts.AverUnitPrice));
                foreach (var unit in sts.StockUnits)
                {
                    item.SubItems.Add(String.Format("{0:0,0}", unit));
                }
                item.SubItems.Add(string.Format("{0:MM/dd/yyyy HH:mm}", sts.Date));
                lstResult.Items.Add(item);
            }
        }
    }
}
