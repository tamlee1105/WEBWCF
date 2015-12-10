using System;
using System.Collections.Generic;
using System.Linq;
using Website.Code;
using Website.ShopService;

namespace Website.UserControl
{
    public partial class Products : System.Web.UI.UserControl
    {

        public string Title { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var cateId = Request.QueryString["cateId"];
            if (IsPostBack || cateId == null) return;

            ViewState["cateId"] = cateId;

        }

        protected void DataPager1_PreRender(object sender, EventArgs e)
        {

            Product[] list = null;
            try
            {
                var cateId = ViewState["cateId"];
                if (cateId == null)
                    //list = service.GetProductsByCate(0);
                    list = Repository.GetProductsByCate(0);
                else
                {
                    var id = Convert.ToInt16(cateId);
                    //list = service.GetProductsByCate(id);
                    list = Repository.GetProductsByCate(id);
                    if (list.Any())
                    {
                        Page.Title = list[0].Category.Name + " " + Page.Title;
                        lblCatePro.InnerText = "Sản phẩm: " + list[0].Category.Name;
                    }
                    else
                        Page.Title = "Đang cập nhật";
                }
            }
            //catch (Exception)
            //{ }
            finally
            {
                lvProducts.DataSource = list;
                lvProducts.DataBind();
            }




        }
    }
}