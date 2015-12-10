using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Business.Entity;
using Business.IRepository;
using Business.Repository;

namespace App.MyUsrCtrl
{
    public partial class UsrCtrlDetailsProduct : MyUserControl
    {
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;
        private Product _product;

        public UsrCtrlDetailsProduct()
        {
            InitializeComponent();

            _categoryRepository = new CategoryRepository();
            _productRepository = new ProductRepository();
        }

        public UsrCtrlDetailsProduct(Product product)
            : this()
        {
            _product = product;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            IsRemove = true;
        }

        private void UsrCtrlDetailsProduct_Load(object sender, EventArgs e)
        {
            cboCate.DataSource = _categoryRepository.GetAll().ToList();

            LoadTextBox();
        }

        private void LoadTextBox()
        {
            if (_product == null)
            {
                AddUserControlDescription();
                //txtPrice.Text = "0";
                return;
            }

            pictureBoxProduct.ImageLocation = Path.Combine(Utilities.Path, _product.Image);
            txtImage.Text = Path.GetFileName(_product.Image);

            txtCode.Text = _product.Code;
            txtName.Text = _product.Name;
            cboCate.Text = _product.Category.Name;
            //txtPrice.Text = _product.Price.ToString("0,0");
            txtWarranty.Text = _product.Warranty;

            var description = _product.Description;
            description = Regex.Replace(description, @"<(table) class=""details"">|<(/table)>", string.Empty).Trim();
            var regex = new Regex("</tr>");
            var arrDescs = regex.Split(description);
            for (var i = 0; i < arrDescs.Count() - 1; i++)
            {
                AddUserControlDescription(arrDescs[i]);
            }


        }

        private const int x = 0;
        private int y;
        private const int t = 30;

        private void btnAddDesc_Click(object sender, EventArgs e)
        {
            AddUserControlDescription();
        }

        private void AddUserControlDescription(string value = null)
        {
            if (panelDescription.Controls.Count >= 10) return;

            var uc = new UsrCtrl
            {
                Location = new Point(x, y),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            if (!string.IsNullOrEmpty(value))
                uc.Value = value;
            y += t;
            panelDescription.Controls.Add(uc);
        }

        private void btnRemoveDesc_Click(object sender, EventArgs e)
        {
            if (panelDescription.Controls.Count < 1) return;
            panelDescription.Controls.RemoveAt(panelDescription.Controls.Count - 1);
            y -= t;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_product == null) _product = new Product();

            _product.Code = txtCode.Text;
            _product.Name = txtName.Text;
            _product.Category = (Category)cboCate.SelectedItem;
            _product.Description = Description();
            _product.Image = "Upload/shop/" + txtImage.Text;
            _product.Warranty = txtWarranty.Text;


            try
            {
                var success = _product.Id == 0 ? _productRepository.Add(_product) : _productRepository.Edit(_product);
                if (success)
                {
                    CopyImage();
                    _productRepository.Commit();
                    Utilities.ShowMessage("Save product to database success");
                    IsRemove = true;
                }
                else
                {
                    Utilities.ShowMessageError("Save product to database failure");
                    _productRepository.Refresh();
                }
            }
            catch (SqlException)
            {
                Utilities.ShowMessageError("Cannot insert duplicate product code in products. The statement has been terminated");
                _productRepository.Refresh();
            }
        }

        private string Description()
        {
            var str = panelDescription.Controls.OfType<UsrCtrl>().Aggregate(@"<table class='details'>",
                                                                            (current, uc) => current + uc.Value);
            return str + @"</table>";
        }

        private void CopyImage()
        {
            var fileSource = openFile.FileName;
            var fileDest = Path.Combine(Utilities.Path, "Upload/shop/" + txtImage.Text);
            if (!File.Exists(fileSource)) return;

            if (File.Exists(fileDest))
            {
                if (Utilities.ShowQuestionMessage(string.Format("This file {0} already exists, do you want to delete?", txtImage.Text))
                == DialogResult.Yes)
                    File.Delete(fileDest);
                else return;
            }

            try
            {
                File.Copy(fileSource, fileDest);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Copy exist file: " + ex.Message);
                Utilities.ShowMessageError("Error when copy image");
            }
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            if (openFile.ShowDialog() != DialogResult.OK) return;
            var fileName = Path.GetFileName(openFile.FileName);
            pictureBoxProduct.ImageLocation = openFile.FileName;
            txtImage.Text = fileName;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            y = 0;
            panelDescription.Controls.Clear();
            LoadTextBox();
        }

        //private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar) || e.KeyChar == ',') { }
        //    else
        //    {
        //        e.Handled = true;
        //    }
        //}
    }
}
