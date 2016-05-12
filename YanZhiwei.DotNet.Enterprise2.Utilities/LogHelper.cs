using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using System.Configuration;

namespace YanZhiwei.DotNet.EntLib2.Utilities
{
    /// <summary>
    /// Enterprise Library for .NET Framework 2.0  日志工具类
    /// </summary>
    public static class LogHelper
    {
        #region 获取Listener的日志路径

        /// <summary>
        /// 获取Listener的日志路径
        /// </summary>
        /// <param name="listenersName">Listener名称</param>
        /// <returns>当获取不到的时候，返回空</returns>
        public static string GetTraceLogPath(string listenersName)
        {
            string _tracePath = string.Empty;
            if (!string.IsNullOrEmpty(listenersName))
            {
                Configuration _etlConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                LoggingSettings _loggingSettings = (LoggingSettings)_etlConfig.GetSection(LoggingSettings.SectionName);
                FlatFileTraceListenerData _listeners = _loggingSettings.TraceListeners.Get(listenersName) as FlatFileTraceListenerData;
                if (_listeners != null)
                {
                    _tracePath = _listeners.FileName;
                }
            }
            return _tracePath;
        }

        #endregion 获取Listener的日志路径

        #region 设置Listener的日志路径

        /// <summary>
        /// 设置Listener的日志路径
        /// </summary>
        /// <param name="listenersName">Listener名称</param>
        /// <param name="fileName">路径</param>
        public static void SetTraceLogPath(string listenersName, string fileName)
        {
            if (!string.IsNullOrEmpty(listenersName) && !string.IsNullOrEmpty(fileName))
            {
                Configuration _etlConfig = ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
                LoggingSettings _loggingSettings = (LoggingSettings)_etlConfig.GetSection(LoggingSettings.SectionName);
                FlatFileTraceListenerData _listeners = _loggingSettings.TraceListeners.Get(listenersName) as FlatFileTraceListenerData;
                if (_listeners != null)
                {
                    _listeners.FileName = fileName;
                    _etlConfig.Save();
                }
            }
        }

        #endregion 设置Listener的日志路径
    }
}