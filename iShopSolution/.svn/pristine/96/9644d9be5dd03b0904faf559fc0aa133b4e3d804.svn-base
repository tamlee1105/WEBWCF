using System;
using System.Linq;

namespace Website.UserControl
{
    public partial class Basket : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cart"] != null)
            {
                var shoppingCart = new Code.ShoppingCart();
                var orderDetails = shoppingCart.Cart.OrdersDetails;
                if (orderDetails.Any())
                {
                    var totalUnit = orderDetails.Sum(d => d.Unit);
                    lblSanPham.Text = string.Format("có <b>{0}</b> sản phẩm", totalUnit);
                    lblTongTien.Text = string.Format("Tổng tiền: <b>{0:0,0 VND}</b>", shoppingCart.GetTotalPriceCart());
                    return;
                }
            }
            lblTongTien.Text = "<b>Chưa có sản phẩm</b>"; 
        }
    }
}