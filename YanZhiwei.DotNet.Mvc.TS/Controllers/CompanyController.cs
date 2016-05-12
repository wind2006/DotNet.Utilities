using System.Linq;
using System.Web.Mvc;
using YanZhiwei.DotNet.Mvc.Learn.DAL;
using YanZhiwei.DotNet4.Utilities.Common;

namespace YanZhiwei.DotNet.Mvc.Learn.Controllers
{
    public class CompanyController : Controller
    {
        private CompanyContext db = new CompanyContext();

        public ViewResult Index(string orderby = "FirstName", bool desc = false, string searchString = null)
        {
            var workerList = from w in db.Workers
                             select w;
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                workerList = workerList.Where(w => w.FirstName.Contains(searchString)
                    || w.LastName.Contains(searchString));
            }
            ViewBag.desc = !desc;
            var _result = workerList.OrderBy<>(orderby, desc);
            return View(_result);
        }
    }
}