using System.Web.Mvc;
using YanZhiwei.BookShop.Domain.Abstract;

namespace YanZhiwei.BookShop.WebUI.Controllers
{
    public class BookController : Controller
    {
        private IBookRepository repository;

        public BookController(IBookRepository bookRepository)
        {
            /*
            BookController类的创建（含初始化）主要经过下面这三个过程：
            1.在Application_Start中，ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());这段注册代码告诉MVC用NinjectControllerFactory工厂类来创建所有Controller对象。在NinjectControllerFactory类中包含了下面两个过程：绑定接口到接口的实现和创建Controller类对象。
            2.ninjectKernel.Bind<IBookRepository>().To<BookRepository>();这段绑定代码告诉ninjectKernel当被请求一个IBookRepository接口的实现时则返回一个BookRepository对象。
            3.请你阅读NinjectControllerFactory类中的GetControllerInstance方法，通过ninjectKernel.Get(controllerType)这句代码，ninject获取controller（如BookController）对象的信息并创建该controller的实例，这个过程会调用controller的构造函数，它会自动判断构造函数所需要的参数，如BookController类的构造函数需要一个IBookRepository接口参数，根据第2个过程ninject注册的绑定，ninject会给该构造函数传递BookRepository对象(IBookRepository接口的实现者)的引用。
            */
            repository = bookRepository;
        }

        /*
           正确使用Get和Post。Get一般用来从服务器获取只读的信息，当需要操作更改状态时使用Post。
        */

        // GET: Book
        public ActionResult Index()
        {
            /*
            1.从POST请求提交的表单中获取数据
            string oldProductName = Request.Form["OldName"];
            string newProductName = Request.Form["NewName"];
            2. Action 方法的参数不允许使用 ref 和 out 参数，这是没有意义的。
            3. MVC 框架通过检查上下文对象来为 Action 方法的参数提供值，它的名称是不区分大小写的，比如 Action 方法的 city 参数的值可以是通过 Request.Form["City"] 来获取的。
            */

            /*
            ViewBag、ViewData 和 TempData 都是 Controller 和 View 中能访问到的属性，都是用来存储小量的数据，他们的区别如下：
            ViewBag，是一个动态(dynamic)的弱类型，在程序运行的时候解析，是 MVC3 中新增的特性，只在当前View有效。
            ViewData，是一个字典集合，也是只在当前View有效，性能比 ViewBag 高，但是使用的时候需要类型转换。
            TempData，也是字典集合，一般用于两个请求之间临时缓存内容或页面间传递消息，保存在 Session 中，使用完以后则从 Session 中被清除。
            */
            return View(repository.Books);
        }
    }
}