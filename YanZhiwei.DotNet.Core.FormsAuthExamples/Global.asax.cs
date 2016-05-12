using System;
using YanZhiwei.DotNet.Core.FormsAuth;
using YanZhiwei.DotNet.Core.FormsAuthExamples.Model;

namespace YanZhiwei.DotNet.Core.FormsAuthExamples
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            FormsPrincipal<UserInfo>.AddPermission(new string[2] { "admin", "user" });
        }

        protected void Application_Error(object sender, EventArgs e)
        {
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}