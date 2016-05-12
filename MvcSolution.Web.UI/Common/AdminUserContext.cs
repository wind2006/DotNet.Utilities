using MvcSolution.Core.Cache;
using YanZhiwei.DotNet.Core.Cache;
using YanZhiwei.Framework.Mvc;

namespace MvcSolution.Web.UI.Common
{
    public class AdminUserContext : UserContext
    {
        public AdminUserContext()
            : base(AdminCookieContext.Current)
        {
        }

        public AdminUserContext(IAuthCookie authCookie)
            : base(authCookie)
        {
        }

        public static AdminUserContext Current
        {
            get
            {
                return CacheHelper.GetItem<AdminUserContext>();
            }
        }
    }
}