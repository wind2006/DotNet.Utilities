using System;
using YanZhiwei.DotNet.Core.FormsAuth;
using YanZhiwei.DotNet.Core.FormsAuthExamples.Model;

namespace YanZhiwei.DotNet.Core.FormsAuthExamples
{
    public partial class FormsPrincipal_Login_aspx : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            UserInfo userinfo = new UserInfo();
            userinfo.UserName = "yanzhiwei";
            userinfo.UserId = 1;
            userinfo.GroupId = 1;
            FormsPrincipal<UserInfo>.SignIn("yanzhiwei", userinfo, 1);
            FormsPrincipal<UserInfo>.RedirectDefaultPage("yanzhiwei");
        }
    }
}