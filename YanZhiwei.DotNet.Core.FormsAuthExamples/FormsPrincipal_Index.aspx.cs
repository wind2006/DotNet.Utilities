using System;

namespace YanZhiwei.DotNet.Core.FormsAuthExamples
{
    public partial class FormsPrincipal_Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                lblUserName.Text = Context.User.Identity.Name;
                lblRoles.Text = (Context.User.IsInRole("admin") == true ? "是管理员" : "不是管理员") +
                                 (Context.User.IsInRole("user") == true ? "；是普通用户" : "不是普通用户");
            }
        }
    }
}