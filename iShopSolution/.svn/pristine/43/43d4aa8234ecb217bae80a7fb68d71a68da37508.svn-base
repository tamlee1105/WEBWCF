using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Website.ShopService;

namespace Website.UserControl
{
    public partial class CategoryMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadMenu();
        }

        private void LoadMenu()
        {
            try
            {
                var service = new ShopServiceClient();
                dListMenu.DataSource = service.GetAllCategories();
                dListMenu.DataBind();
            }
            catch
            {
                lblMsg.Text = "Could not connect to service";
            }
            
        }
    }
}