using System.Threading.Tasks;
using System.Web.Mvc;
using YanZhiwei.BookShop.WebUI.Models;

namespace YanZhiwei.BookShop.WebUI.Controllers
{
    public class RemoteDataController : AsyncController
    {
        /* 异步Control

       对于 ASP.NET 的工作平台 IIS，它维护了一个.NET线程池用来处理客户端请求。这个线程池称为工作线程池(worker thread pool)，其中的线程称为工作线程(worker threads)。当接收到一个客户端请求，一个工作线程从工作线程池中被唤醒并处理接收到的请求。当请求被处理完了后，工作线程又被这个线程池回收。这种线程程池的机制对ASP.NET应用程序有如下两个好处：
       1.通过线程的重复利用，避免了每次接收到一个新的请求就创建一个新的线程。
       2.线程池维护的线程数是固定的，这样线程不会被无限制地创建，减少了服务器崩溃的风险。

       一个请求是对应一个工作线程，如果MVC中的action对请求处理的时间很短暂，那么工作线程很快就会被线程池收回以备重用。但如果执行action的工作线程需要调用其他服务（如调用远程的服务，数据的导入导出），这个服务可能需要花很长时间来完成任务，那么这个工作线程将会一直等待下去，直到调用的服务返回才继续工作。这个工作线程在等待的过程中什么也没做，资源浪费了。设想一下，如果这样的action一多，所有的工作线程都处于等待状态，大家都没事做，而新的请求来了又没人理，这样就陷入了尴尬境地。

        解决这个问题需要使用异步(asynchronous) Controller，异步Controller允许工作线程在等待(await)的时候去处理别的请求，这样做减少了资源浪费，有效提高了服务器的性能。

     */

        // GET: RemoteData
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Data()
        {
            string data = await new RemoteService().GetRemoteDataAsync();
            Response.Write(data);

            return View("Result", new Result
            {
                ControllerName = "RemoteData",
                ActionName = "Data"
            });
        }
    }
}