using MvcSolution.Core.Cache;
using YanZhiwei.DotNet.Core.Cache;
using YanZhiwei.DotNet2.Utilities.WebForm;
using YanZhiwei.Framework.Mvc;

namespace MvcSolution.Web.UI.Common
{
    public class AdminCookieContext : CookieContext
    {
        public static AdminCookieContext Current
        {
            get
            {
                return CacheHelper.GetItem<AdminCookieContext>();
            }
        }

        public override string KeyPrefix
        {
            get
            {
                return FetchHelper.ServerDomain + "_AdminContext_";
            }
        }
    }
}