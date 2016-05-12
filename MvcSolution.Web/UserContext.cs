using MvcSolution.Core.Cache;
using System;
using YanZhiwei.DotNet.Core.Cache;
using YanZhiwei.Framework.Mvc;
namespace MvcSolution.Web
{
    public class UserContext
    {
        protected IAuthCookie authCookie;

        public UserContext(IAuthCookie authCookie)
        {
            this.authCookie = authCookie;
        }

        public GMS.Contract.Models.LoginInfo LoginInfo
        {
            get
            {
                return CacheHelper.GetItem<GMS.Contract.Models.LoginInfo>("LoginInfo", () =>
                {
                    if (authCookie.UserToken == Guid.Empty)
                        return null;

                    var loginInfo = ServiceContext.Current.AccountService.GetLoginInfo(authCookie.UserToken);

                    if (loginInfo != null && loginInfo.UserID > 0 && loginInfo.UserID != this.authCookie.UserId)
                        throw new Exception("非法操作，试图通过网站修改Cookie取得用户信息！");

                    return loginInfo;
                });
            }
        }
    }
}