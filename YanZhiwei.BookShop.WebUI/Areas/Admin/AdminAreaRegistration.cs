using System.Web.Mvc;

namespace YanZhiwei.BookShop.WebUI.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        /*
         系统自动生成的 AdminAreaRegistration 类继承至抽象类 AreaRegistration，并重写了 AreaName 属性和 RegisterArea 方法。在 RegisterArea 方法中它为我们定义了一个默认路由，我们也可在这个方法中定义专属于Admin Area的的其他路由。但有一点要注意，在这如果要给路由起名字，一定要确保它和整个应用程序不一样。
        
         AreaRegistrationContext 类的 MapRoute 方法和 RouteCollection 类的 MapRoute 方法的使用是一样的，只是 AreaRegistrationContext 类限制了注册的路由只会去匹配当前 Area 的 controller，所以，如果你把在 Area 中添加的 controller 的默认命名空间改了，路由系统将找不到这个controller 。
         */
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}