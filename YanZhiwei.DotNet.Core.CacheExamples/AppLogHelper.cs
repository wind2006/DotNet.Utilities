using YanZhiwei.DotNet.Core.ConfigExamples;
using YanZhiwei.DotNet.Core.Log;

namespace YanZhiwei.DotNet.Core.CacheExamples
{
    public static class AppLogHelper
    {
        public static Log4NetHelper Instance = null;

        static AppLogHelper()
        {
            Instance = new Log4NetHelper(CachedConfigContext.Current.DaoConfig.Log, CachedConfigContext.Current.ConfigService.GetConfig("Log4net"));
        }

        public static void Info(object message)
        {
            Instance.Info(LoggerType.WinExceptionLog, message);
        }
    }
}