using System.Web;
using Website.Code.Entity;

namespace Website.Code
{
    public class Authentication
    {
        public User User { get; set; }
        public bool IsLogin { get; set; }
        public Authentication()
        {
            if (HttpContext.Current.Session["user"] == null)
            {
                IsLogin = false;
                //User = new User();
                //HttpContext.Current.Session["user"] = User;
            }
            else
            {
                User = (User)HttpContext.Current.Session["user"];
                if (!string.IsNullOrEmpty(User.Username) && !string.IsNullOrEmpty(User.Password))
                    IsLogin = true;
            }
        }

        public bool Login(User user)
        {
            var configPath = HttpContext.Current.Request.ApplicationPath;
            var root = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(configPath);
            if (root.AppSettings.Settings.Count > 0)
            {
                var username = root.AppSettings.Settings["user"];
                var password = root.AppSettings.Settings["pass"];
                if (username != null && password != null)
                {
                    if (username.Value.Equals(user.Username) &&
                        password.Value.Equals(user.Password.Encrypt()))
                    {
                        //User.Username = user.Username;
                        //User.Password = user.Password;
                        HttpContext.Current.Session["user"] = user;
                        IsLogin = true;
                        
                        return true;
                    }
                }
            }
            IsLogin = false;
            return false;
        }

        public void Logout()
        {
            IsLogin = false;
            HttpContext.Current.Session["user"] = null;
        }

    }
}