using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Website.Code.Entity;

namespace Website
{
    public partial class Manager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadGvlstCart();
        }

        private void LoadGvlstCart()
        {
            var list = Code.ShoppingCart.GetAllOrder();
            gvLstCart.DataSource = list;
            gvLstCart.DataBind();

            if (list == null) return;
            ViewState["list"] = list.ToList();

        }

        protected void gvLstCart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("details"))
            {
                var id = Convert.ToInt32(e.CommandArgument);
                var list = ViewState["list"] as List<Cart> ?? Code.ShoppingCart.GetAllOrder().ToList();
                var details = list.Find(c => c.Id == id);
                gvDetails.DataSource = details.OrdersDetails;
                gvDetails.DataBind();
            }
        }


        protected void btnSendRequest_Click(object sender, EventArgs e)
        {
            var list = ViewState["list"] as List<Cart>;
            if (list == null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('null');", true);
                return;
            }
            foreach (var row in gvLstCart.Rows.Cast<GridViewRow>())
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + list[row.RowIndex].Id + "');", true);

            }


        }
    }
}