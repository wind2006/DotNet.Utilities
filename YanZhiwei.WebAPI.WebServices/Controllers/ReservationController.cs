using System.Collections.Generic;
using System.Web.Http;
using YanZhiwei.WebAPI.WebServices.Models;

namespace YanZhiwei.WebAPI.WebServices.Controllers
{
    public class ReservationController : ApiController
    {
        /*
        MVC WebAPI中的Controllers和普通MVC的Controllers类似，不过不再继承于Controller，而改为继承API的ApiController，一个Controller可以包含多个Action，这些Action响应请求的方法与Global中配置的路由规则有关，在后面结束Global时统一说明。

        再次强调Views对于WebAPI来说没有太大的用途，Models中的Model主要用于保存Service和Client交互的对象，这些对象默认情况下会被转换为Json格式的数据进行传输，Controllers中的Controller对应于WebService来说是一个Resource，用于提供服务。和普通的MVC一样，Global.asax用于配置路由规则

        当应用程序接收到一个和Web API 路由匹配的请求时，Action方法的调用将取决于发送HTTP请求的方式。当我们用 /api/reservation URL测试API Controller时，浏览器指定的是GET方式的请求。API Controller的基类 ApiController根据路由信息知道需要调用哪个Controller，并根据HTTP请求的方式寻找适合的Action方法。

        一般约定在Action方法前加上HTTP请求方式名作为前缀。这里前缀只是个约定，Web API能够匹配到任何包含了HTTP请求方式名的Action方法。也就是说，本文示例的GET请求将会匹配 GetAllReservations 和 GetReservation，也能够匹配 DoGetReservation 或 ThisIsTheGetAction。

        对于两个含有相同HTTP请求方式的Action方法，API Controlller会根据它们的参数和路由信息来寻找最佳的匹配。例如请求 /api/reservation URL，GetAllReservations 方法会被匹配，因为它没有参数；请求 /api/reservation/3 URL，GetReservation 方法会被匹配，因为该方法的参数名和URL的 /3 片段对应的片段变量名相同。我们还可以使用 POST、DELETE 和 PUT请求方式来指定ReservationController的其它Action方法。这就是前文提到的REST的风格。

        但有的时候为了用HTTP方式名来给Action方法命名会显得很不自然，比如 PutReservation，习惯上会用 UpdateReservation。不仅用PUT命名不自然，POST也是一样的。这时候就需要使用类似于MVC的Controller中使用的Action方法选择器了。在System.Web.Http 命名空间下同样包含了一系列用于指定HTTP请求方式的特性，如下所示：
        public class ReservationController : ApiController {
        [HttpPost]
        public Reservation CreateReservation(Reservation item) {
        return repo.Add(item);
        }
        [HttpPut]
        public bool UpdateReservation(Reservation item) {
        return repo.Update(item);
        } 
        }

       因为routeTemplate中有了{controller}
       所以针对api的请求可以自动映射到指定的controller类
       那么是怎么找到合适的Action的呢？
       系统根据请求的方式来判断
       如果是以GET的方式请求的
       那么将匹配controller中以“Get”开头的Action
       如果是以POST的方式请求的
       那么将匹配controller中以“Post”开头的Action
       如果是以PUT的方式请求的
       那么将匹配controller中以“Put”开头的Action
       如果是以DELETE的方式请求的
       那么将匹配controller中以“Delete”开头的Action
        */
        private IReservationRepository repo = ReservationRepository.getRepository();

        public IEnumerable<Reservation> GetAllReservations()
        {
            return repo.GetAll();
        }

        public Reservation GetReservation(int id)
        {
            return repo.Get(id);
        }

        public Reservation PostReservation(Reservation item)
        {
            return repo.Add(item);
        }

        public bool PutReservation(Reservation item)
        {
            return repo.Update(item);
        }

        public void DeleteReservation(int id)
        {
            repo.Remove(id);
        }
    }
}