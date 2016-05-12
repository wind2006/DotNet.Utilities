using MvcSolution.Core.Cache;
using YanZhiwei.DotNet.Core.Cache;
using YanZhiwei.DotNet.Core.Service;
using YanZhiwei.DotNet3._5.Utilities.Common;

namespace MvcSolution.Web
{
    /// <summary>
    /// 直接引用提供服务
    /// </summary>
    public class RefServiceFactory : ServiceFactory
    {
        public override T CreateService<T>()
        {
            //第一次通过反射创建服务实例，然后缓存住
            string _interfaceName = typeof(T).Name;

            return CacheHelper.Get<T>(string.Format("Service_{0}", _interfaceName), () =>
            {
                return AssemblyHelper.FindTypeByInterface<T>();
            });
        }
    }
}