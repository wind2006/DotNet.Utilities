using System;
using System.Net;
using System.Web;
using System.Web.SessionState;
using YanZhiwei.DotNet.Core.AdminPanel.Models;
using YanZhiwei.DotNet.Core.FormsAuth;
using YanZhiwei.DotNet2.Utilities.Common;
using YanZhiwei.DotNet3._5.Utilities.WebForm;

namespace YanZhiwei.DotNet.Core.AdminPanel.BackHandler
{
    /// <summary>
    /// Summary description for BaseHandler
    /// </summary>
    public class BaseHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string _action = context.Request["action"];

            HanlderLogin(context, _action);
        }

        /// <summary>
        /// 处理登陆
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <param name="action">请求动作</param>
        /// 时间：2016-05-10 17:28
        /// 备注：
        private void HanlderLogin(HttpContext context, string action)
        {
            if (string.Compare(action, "login", true) == 0)
            {
                string _userName = context.Request["userName"],
                       _userPassword = context.Request["userPassword"],
                       _verifyCode = context.Request["verifyCode"];
                if (CheckedVerifyCode(context, _verifyCode))
                {
                    try
                    {
                        Base_UserInfo userinfo = new Base_UserInfo();
                        userinfo.User_Name = _userName;
                        userinfo.User_Pwd = _userPassword;
                        FormsPrincipal<Base_UserInfo>.SignIn(_userName, userinfo);
                        context.CreateResponse("登陆成功.", HttpStatusCode.OK);
                    }
                    catch (Exception ex)
                    {
                        HanlderErrorRequest(context, ex, "登陆失败，请稍后重试。");
                    }
                }
            }
        }

        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <param name="ex">The ex.</param>
        /// 时间：2016-05-10 17:37
        /// 备注：
        private void HanlderErrorRequest(HttpContext context, Exception ex, string errorMsg)
        {
            context.CreateResponse(errorMsg, HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// 检查用户验证码
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <param name="verifyCode">验证码</param>
        /// <returns></returns>
        /// 时间：2016-05-04 17:02
        /// 备注：
        private bool CheckedVerifyCode(HttpContext context, string verifyCode)
        {
            if (string.Compare(verifyCode, context.Session["verifyCode"].ToStringOrDefault(""), true) == 0)
            {
                return true;
            }
            else {
                context.CreateResponse("验证码不正确.", HttpStatusCode.BadRequest, 1);
            }
            return false;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}