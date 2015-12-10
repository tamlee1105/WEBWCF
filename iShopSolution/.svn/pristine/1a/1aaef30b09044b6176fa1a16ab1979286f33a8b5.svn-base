using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Business.Entity;
using Business.IRepository;
using Business.Repository;

namespace App
{
    public partial class FormProductManager : Form
    {
        private List<Product> list;
        private IProductRepository _productRepository;

        public FormProductManager()
        {
            InitializeComponent();
            list = new List<Product>();
            _productRepository = new ProductRepository();
        }

        private void FormProductManager_Load(object sender, EventArgs e)
        {
            try
            {
                list = _productRepository.GetAll().ToList();
                LoadDataGridView();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Form product manager: " + ex.Message);
            }

        }

        private void LoadDataGridView()
        {
            dataGridViewProduct.DataSource = null;
            dataGridViewProduct.AutoGenerateColumns = false;
            dataGridViewProduct.DataSource = list;

            //statistics 
            var totalProducts = list.Count;
            var totalQuantities = list.Sum(p => p.Inventory);
            lblAlert.Text = string.Format("Have {0} product(s) with {1} unit(s) in repository", totalProducts,
                                          totalQuantities);
        }

        private void dataGridViewProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            //get a product selected
            //var item = list.ElementAt(e.RowIndex);
            var item = (Product) dataGridViewProduct.Rows[e.RowIndex].DataBoundItem;
            switch (e.ColumnIndex)
            {
                case 5:
                    // send to form details for editing
                    var frm = new FormDetailsProduct(item);
                    frm.ShowDialog();
                    list = _productRepository.GetAll().ToList();
                    LoadDataGridView();
                    break;
                case 6:
                    //confirm delete product
                    if (Utilities.ShowQuestionMessage("Do you want to delete " + item.Name + " ?") == DialogResult.Yes)
                    {
                        try
                        {
                            var success = _productRepository.Delete(item.Id);
                            if (success)
                            {
                                _productRepository.Commit();
                                Utilities.ShowMessage("Delete success");
                                list = _productRepository.GetAll().ToList();
                                LoadDataGridView();
                            }
                            else
                            {
                                Utilities.ShowMessageError("Delete failure");
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("Delete product: " + ex.Message);
                            Utilities.ShowMessageError("Error");
                        }
                    }
                    break;

            }

        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            list = _productRepository.GetAll().ToList();
            LoadDataGridView();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim().Length < 1)
                LoadDataGridView();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dataGridViewProduct.DataSource = null;
            dataGridViewProduct.DataSource = _productRepository.Find(txtSearch.Text.Trim()).ToList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var frm = new FormDetailsProduct();
            frm.ShowDialog();
            list = _productRepository.GetAll().ToList();
            LoadDataGridView();
        }


    }
}
