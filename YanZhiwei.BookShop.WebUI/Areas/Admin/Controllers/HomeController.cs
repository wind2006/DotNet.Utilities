using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YanZhiwei.BookShop.WebUI.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        /*
        Controller的歧义问题:
        */
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}