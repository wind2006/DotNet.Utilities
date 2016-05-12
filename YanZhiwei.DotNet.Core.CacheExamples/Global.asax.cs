using System;
using YanZhiwei.DotNet.Core.Cache;
using YanZhiwei.DotNet.Core.ConfigExamples;

namespace YanZhiwei.DotNet.Core.CacheExamples
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            CacheConfigContext.SetCacheConfig(CachedConfigContext.Current.CacheConfig);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
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