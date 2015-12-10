using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Website
{
    public partial class ShoppingCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var qryId = Request.QueryString["ProductID"];
            if (qryId != null)
            {
                if (!IsPostBack)
                {
                    int id;
                    if (int.TryParse(qryId, out id))
                        AddOneProductToCart(id);
                }
            }
            if (!IsPostBack)
            {
                LoadGirdView();
                //Page.Title = "Giỏ hàng của bạn - iShop Bán hàng trực tuyến";
            }
        }

        private void LoadGirdView()
        {
            var shoppingCart = new Code.ShoppingCart();
            if (!shoppingCart.Cart.OrdersDetails.Any())
            {
                AlertNullCart();
                return;
            }
            gvCart.DataSource = shoppingCart.Cart.OrdersDetails;
            gvCart.DataBind();
            lblTotal.Text = string.Format("{0:0,0 VND}", shoppingCart.GetTotalPriceCart());
        }

        private void AddOneProductToCart(int proId)
        {
            var shoppingCart = new Code.ShoppingCart();
            shoppingCart.Add(proId, 1);
        }


        private void AlertNullCart()
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Giỏ hàng của bạn chưa có sản phẩm nào.');window.location='Products.aspx';", true);
        }


        protected void btnSumit_Click(object sender, EventArgs e)
        {
            try
            {
                var sex = ddlGioiTinh.SelectedItem.ToString();
                var customer = new Code.Entity.Customer
                                   {
                                       //Code = lblMaKhachHang.Text,
                                       CodeId = txtCMND.Text,
                                       Name = string.Format("{0} {1}", sex, txtHoTen.Text),
                                       Address = txtDiaChi.Text,
                                       Phone = txtSoDienThoai.Text,
                                       Email = txtEmail.Text
                                   };
                var shoppingCart = new Code.ShoppingCart {Cart = {Customer = customer}};
                shoppingCart.Add();
                MultiView1.SetActiveView(ViewDangKyThanhCong);

                ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                                                    "setTimeout(\"window.location='Default.aspx';\",10000);", true);
            }
            catch (Exception exception)
            {
                MultiView1.SetActiveView(ViewDangKyThatBai);
                Debug.WriteLine("submit shopping cart: "+exception.Message);
            }
        }

        protected void btnDelAll_Click(object sender, EventArgs e)
        {
            var shoppingCart = new Code.ShoppingCart();
            shoppingCart.Clear();
            LoadGirdView();
        }

        protected void btnUpdateCart_Click(object sender, EventArgs e)
        {
            var shoppingCart = new Code.ShoppingCart();
            foreach (var row in gvCart.Rows.Cast<GridViewRow>().Where(row => row.RowType == DataControlRowType.DataRow))
            {
                var unit = Convert.ToInt16(((TextBox)row.FindControl("txtQuantity")).Text);
                var key = gvCart.DataKeys[row.RowIndex];
                if (key == null) continue;
                var proId = Convert.ToInt32(key.Value);
                shoppingCart.SetUnit(proId, unit);
            }
            LoadGirdView();
        }

        protected void btnPayment_Click(object sender, EventArgs e)
        {
            btnUpdateCart_Click(sender, e);
            gvCart.Enabled = false;
            ddlGioiTinh.Items.Add("Ông");
            ddlGioiTinh.Items.Add("Bà");

            MultiView1.SetActiveView(ViewDangKyThongTin);
        }

        protected void gvCart_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var index = e.RowIndex;
            var shoppingCart = new Code.ShoppingCart();
            shoppingCart.Cart.OrdersDetails.RemoveAt(index);
            LoadGirdView();
        }
    }
}