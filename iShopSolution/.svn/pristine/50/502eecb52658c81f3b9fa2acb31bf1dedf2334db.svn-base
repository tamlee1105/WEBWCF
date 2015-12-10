using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Website.Code;
using Website.ShopService;

namespace Website
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            LoadCategories();

            var keyword = Request.QueryString["p"];
            if (keyword != null)
            {
                txtKeySearch.Text = keyword;
                btnSumit_Click(sender, e);
            }

            Page.Title = "Tìm kiếm sản phẩm";

        }

        private void LoadCategories()
        {
            var list = new List<Category>();
            try
            {
                var service = new ShopServiceClient();
                list = service.GetAllCategories().ToList();
                list.Insert(0, new Category { Id = 0, Name = "---Tất cả loại sản phẩm---" });

            }
            catch (Exception)
            {
                list.Insert(0, new Category { Name = "Can not load service" });
            }
            ddlCatePros.DataSource = list;
            ddlCatePros.DataBind();

        }

        protected void btnSumit_Click(object sender, EventArgs e)
        {
            //ViewState["keyword"] = txtKeySearch.Text.Trim();
            //ViewState["cateId"] = Int32.Parse(ddlCatePros.SelectedValue);
            var key = txtKeySearch.Text.Trim();
            var cateId = Int16.Parse(ddlCatePros.SelectedValue);

            var service = new ShopServiceClient();
            //var result = service.Find(key, cateId).ToList();
            var result = Repository.Find(key, cateId).ToList();
            ViewState["result"] = result;
            ViewState["key"] = key;
            MultiView1.SetActiveView(ViewResultSearch);
        }

        protected void DataPager1_PreRender(object sender, EventArgs e)
        {
            var list = (List<Product>)ViewState["result"];
            
            var key = ViewState["key"];
            if (list == null)
            {
                lblResult.Text = "Không tìm thấy sản phẩm";
                return;
            }
            //list = list.ToList();
            lvResultSearch.DataSource = list;
            lvResultSearch.DataBind();
            if (string.IsNullOrEmpty(key.ToString()))
                lblResult.Text = "tìm thấy <span style='color:red'>" + list.Count + "</span> sản phẩm";
            else
                lblResult.Text = string.Format(" tìm với từ khóa <span style='color:red'>{0}</span> tìm thấy <span style='color:red'>{1}</span> sản phẩm", key, list.Count);

        }
    }
}