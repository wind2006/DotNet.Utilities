using log4net;
using System;

namespace YanZhiwei.DotNet.Log4Net.Utilities
{
    /// <summary>
    /// Log4Net帮助类
    /// </summary>
    public class Log4NetHelper
    {
        #region 变量

        private static ILog log = null;

        private static string logger = "FileLogger";


        #endregion 变量

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static Log4NetHelper()
        {
            log = LogManager.GetLogger(logger);
        }

        /// <summary>
        ///设置日志记录Logger
        /// </summary>
        /// <param name="loggerName">Name of the logger.</param>
        public static void SetLogger(string loggerName)
        {
            logger = loggerName;
            log = LogManager.GetLogger(logger);
        }

        /// <summary>
        /// Writes the debug.
        /// </summary>
        /// <param name="debug">The debug.</param>
        /// <param name="ex">The ex.</param>
        public static void WriteDebug(string debug, Exception ex)
        {
            if (log.IsDebugEnabled)
            {
                log.Debug(debug, ex);
            }
        }

        /// <summary>
        /// Writes the debug.
        /// </summary>
        /// <param name="debug">The debug.</param>
        public static void WriteDebug(string debug)
        {
            if (log.IsDebugEnabled)
            {
                log.Debug(debug);
            }
        }

        /// <summary>
        /// Writes the error.
        /// </summary>
        /// <param name="error">The error.</param>
        public static void WriteError(string error)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(error);
            }
        }

        /// <summary>
        /// Writes the error.
        /// </summary>
        /// <param name="Error">The error.</param>
        /// <param name="ex">The ex.</param>
        public static void WriteError(string Error, Exception ex)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(Error, ex);
            }
        }

        /// <summary>
        /// Writes the fatal.
        /// </summary>
        /// <param name="fatal">The fatal.</param>
        public static void WriteFatal(string fatal)
        {
            if (log.IsFatalEnabled)
            {
                log.Fatal(fatal);
            }
        }

        /// <summary>
        /// Writes the fatal.
        /// </summary>
        /// <param name="fatal">The fatal.</param>
        /// <param name="ex">The ex.</param>
        public static void WriteFatal(string fatal, Exception ex)
        {
            if (log.IsFatalEnabled)
            {
                log.Fatal(fatal, ex);
            }
        }

        /// <summary>
        /// Writes the information.
        /// </summary>
        /// <param name="info">The information.</param>
        public static void WriteInfo(string info)
        {
            if (log.IsInfoEnabled)
            {
                log.Info(info);
            }
        }

        /// <summary>
        /// Writes the information.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="ex">The ex.</param>
        public static void WriteInfo(string info, Exception ex)
        {
            if (log.IsInfoEnabled)
            {
                log.Info(info, ex);
            }
        }

        /// <summary>
        /// Writes the warn.
        /// </summary>
        /// <param name="warn">The warn.</param>
        /// <param name="ex">The ex.</param>
        public static void WriteWarn(string warn, Exception ex)
        {
            if (log.IsWarnEnabled)
            {
                log.Warn(warn, ex);
            }
        }

        /// <summary>
        /// Writes the warn.
        /// </summary>
        /// <param name="warn">The warn.</param>
        public static void WriteWarn(string warn)
        {
            if (log.IsWarnEnabled)
            {
                log.Warn(warn);
            }
        }
    }
}