
using YanZhiwei.DotNet.Core.Service;

namespace MvcSolution.Web
{
    public static class AppServiceHelper
    {
        public static ServiceHelper Instance = null;

        static AppServiceHelper()
        {
            Instance = new ServiceHelper(new RefServiceFactory());
        }
    }
}