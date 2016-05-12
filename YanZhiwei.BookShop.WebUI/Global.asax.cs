using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using YanZhiwei.BookShop.Domain.Abstract;
using YanZhiwei.BookShop.Domain.Concrete;
using YanZhiwei.DotNet.Ninject.Utilities;

namespace YanZhiwei.BookShop.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            NinjectControllerFactory _ninjectFacotry = new NinjectControllerFactory(ng =>
            {
                ng.Bind<IBookRepository>().To<BookRepository>();
            });
            /*
            调用 AreaRegistration.RegisterAllAreas 方法让MVC应用程序在启动后会寻找所有继承自 AreaRegistration 的类，并为每个这样的类调用它们的 RegisterArea 方法。
            
            注意：不要轻易改变 Application_Start 中注册方法的顺序，如果你把RouteConfig.RegisterRoutes方法放到AreaRegistration.RegisterAllAreas方法之前，Area 路由的注册将会在路由注册之后，路由系统是按顺序来匹配的，所以这样做会让请求 Area 的 Controller 匹配到错误的路由。
            */
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ControllerBuilder.Current.SetControllerFactory(_ninjectFacotry);
        }
    }
}