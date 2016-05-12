using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YanZhiwei.DotNet2.Utilities.AspNet;
namespace YanZhiwei.DotNet2.Utilities.WebForm.Examples
{
    public partial class FormsAuthen_Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            FormsAuthenticHelper _loginAuth = new FormsAuthenticHelper("YanZhiwei", string.Empty);
            _loginAuth.CreateCookie(1);
            _loginAuth.RedirectDefaultPage();
        }
    }
}