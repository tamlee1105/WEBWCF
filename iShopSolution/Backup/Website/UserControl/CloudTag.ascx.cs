using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Website.ShopService;

namespace Website.UserControl
{
    public partial class CloudTag : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            try
            {
                var service = new ShopServiceClient();
                var list = service.GetByCate(0);
                var tags = from p in list
                           select new
                           {
                               Product = p.Name,
                               Weight = 10 + p.Category.Id,
                               HrefUrl = "ProductDetails.aspx?id=" + p.Id
                           };
                WPCumulus1.DataSource = tags.ToList();
                WPCumulus1.DataBind();
            }
            catch
            {
            }

        }
    }

}