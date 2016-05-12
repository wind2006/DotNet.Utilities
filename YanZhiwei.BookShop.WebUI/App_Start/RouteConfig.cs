using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace YanZhiwei.BookShop.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Book", action = "Index", id = UrlParameter.Optional },
                /*
                出现这个问题是因为路由系统进行匹配的时候出现了Controller同名的歧义。
                当Area被注册的时候，Area中定义的路由被限制了只寻找 Area 中的Controller，所以我们请求 /Admin/Home/Index 时能正常得到 MvcApplication1.Areas.Admin.Controllers 命名空间的 HomeController。然而我们在RouteConfig.cs文件的RegisterRoutes方法中定义的路由并没有类似的限制。
                */
                namespaces: new[] { "YanZhiwei.BookShop.WebUI.Controllers" }
            );
        }
    }
}
