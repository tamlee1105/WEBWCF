using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using Website.Code;
using Website.ShopService;

namespace Website
{
    public partial class ProductDetails : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            var proId = Request.QueryString["id"];
            if (proId == null) Response.Redirect("/Default.aspx");
            DisplayInfoProduct(Convert.ToInt16(proId));
        }

        private void DisplayInfoProduct(int id)
        {
            //var product = service.GetProduct(id);
            var product = Repository.GetProduct(id);
            if (product == null)
            {
                lblName.Text = "Không tìm thấy sản phẩm";
                return;
            }
            lblPic.ImageUrl = product.ImageHost;
            if (product.Inventory == 0)
            {
                //lblPrice.Text = "Hết hàng";
                btnBuy.Visible = false;
                txtSoLuong.Visible = false;
            }
            else
                lblPrice.Text = product.StrPrice;
            lblName.Text = product.Name;
            lblCateName.Text = product.Category.Name;
            lblDes.Text = product.Description;
            Page.Title = product.Name;
            lblWarranty.Text = product.Warranty;
            if (product.Inventory < 1)
                lblSoTon.Text = "Hết hàng";
            else
                lblSoTon.Text = product.Inventory + " sản phẩm";
            lblUpdate.Text = string.Format("{0:dd/MM/yyyy HH:mm:ss}", product.UpdateTime);

            ViewState["cateId"] = product.Category.Id;
            ViewState["proId"] = product.Id;
            //ViewState["SoTon"] = product.SoTon;
        }

        protected void DataPager1_PreRender(object sender, EventArgs e)
        {
            var vcateId = ViewState["cateId"];
            var vproId = ViewState["proId"];
            var list = new List<Product>();
            try
            {
                var cateId = Convert.ToInt16(vcateId);
                var proId = Convert.ToInt16(vproId);

                //list = service.GetByCate(cateId).ToList();
                list = Repository.GetProductsByCate(cateId).ToList();

                if (!list.Any()) return;
                var index = list.FindIndex(c => c.Id == proId);
                if (index < 0) return;
                list.RemoveAt(index);
            }
            //catch (Exception)
            //{
            //}
            finally
            {
                lvSamePros.DataSource = list;
                lvSamePros.DataBind();
            }

        }

        protected void btnBuy_Click(object sender, EventArgs e)
        {
            var unit = Convert.ToInt32(txtSoLuong.Text);
            if (ViewState["proId"] == null) return;
            var id = Convert.ToInt32(ViewState["proId"]);

            if (unit == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Số lượng đặt mua sản phẩm chưa có !!!');", true);
                txtSoLuong.Text = "1";
            }
            else
            {
                var shoppingCart = new Code.ShoppingCart();
                shoppingCart.Add(id, unit);
                Response.Redirect("~/ShoppingCart.aspx");
            }

        }
    }
}