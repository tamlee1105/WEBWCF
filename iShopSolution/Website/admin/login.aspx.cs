using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Website.Code;
using Website.Code.Entity;

namespace Website.admin
{
    public partial class login1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var author = new Authentication();
            if(author.IsLogin)
                Response.Redirect("~/admin/Default.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var author = new Authentication();
            var user = new User {Username = txtName.Text, Password = txtPass.Text};
            if(author.Login(user))
            {
                Response.Redirect("~/admin/Default.aspx");
            }
            else
            {
                lblMsg.Text = "Bạn có phải là admin không ???";
            }
        }
    }
}