using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using App.Report;
using Business.Entity;
using Business.Repository;
using Microsoft.Reporting.WinForms;

namespace App.MyUsrCtrl
{
    public partial class UsrCtrlReport : MyUserControl
    {
        public UsrCtrlReport()
        {
            InitializeComponent();
        }


        private void UsrCtrlReport_Load(object sender, EventArgs e)
        {
            LoadCboCate();
            dtFrom.Value = DateTime.Now.AddYears(-1);
            dtTo.Value = DateTime.Now;
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
                Debug.WriteLine("Load cbo cate: " + exception.Message);
            }

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            IsRemove = true;
        }


        private void btnReport_Click(object sender, EventArgs e)
        {
            LoadReport();
            
        }

        private void LoadReport()
        {
            var cateName = cboCate.Text;
            var param = new List<ReportParameter> { new ReportParameter("Category", cateName) };
            reportViewer1.LocalReport.SetParameters(param);

            var cateId = cboCate.SelectedValue is int ? (int)cboCate.SelectedValue : 0;
            ReceiptDeliveryBindingSource.DataSource = ReportView.GetData(cateId, dtFrom.Value, dtTo.Value).ToList();
            reportViewer1.RefreshReport();
        }

       
    }
}
