using MvcSolution.Core.Config;
using YanZhiwei.DotNet.Core.Log;

namespace MvcSolution.Core.Log
{
    public static class AppLog4NetHelper
    {
        public static readonly Log4NetHelper Instance = null;

        static AppLog4NetHelper()
        {
            //初始化log4net配置
            var config = CachedConfigContext.Current.ConfigService.GetConfig("log4net");
            //重写log4net配置里的连接字符串
            config = config.Replace("{connectionString}", CachedConfigContext.Current.DaoConfig.Log);
            Instance = new Log4NetHelper(config);
        }
    }
}