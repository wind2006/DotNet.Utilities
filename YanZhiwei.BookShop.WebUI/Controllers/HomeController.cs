using System;
using System.Web.Mvc;

namespace YanZhiwei.BookShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult List()
        {
            return View();
        }

        //ChildActionOnly 特性保证了该 action 只能作为子action被调用（不是必须的）。
        [ChildActionOnly]
        public ActionResult Time()
        {
            /*
            Child action 和 Patial view 类似，也是在应用程序的不同地方可以重复利用相同的子内容。不同的是，它是通过调用 controller 中的 action 方法来呈现子内容的，并且一般包含了业务的处理。任何 action 都可以作为子 action 。
            */
            return PartialView(DateTime.Now);
        }
    }
}