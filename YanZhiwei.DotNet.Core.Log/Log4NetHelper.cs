namespace YanZhiwei.DotNet.Core.Log
{
    using System;
    using System.IO;
    using System.Text;

    using log4net;
    using log4net.Config;

    using Newtonsoft.Json;

    /// <summary>
    /// Log4Net日志记录
    /// </summary>
    /// 时间：2016-01-04 11:13
    /// 备注：
    public class Log4NetHelper
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logNetConnectString">Log4Net数据库存储连接字符串</param>
        /// <param name="logNetXmlConfig">Log4Net XML配置</param>
        /// 时间：2016-01-04 11:13
        /// 备注：
        public Log4NetHelper(string logNetConnectString, string logNetXmlConfig)
        {
            if (string.IsNullOrEmpty(logNetXmlConfig))
                throw new ArgumentNullException("请初始化Log4Net XML配置内容！");
            if (!string.IsNullOrEmpty(logNetConnectString))
                logNetXmlConfig = logNetXmlConfig.Replace("{connectionString}", logNetConnectString);
            MemoryStream _ms = new MemoryStream(Encoding.UTF8.GetBytes(logNetXmlConfig));
            XmlConfigurator.Configure(_ms);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logNetXmlConfig">Log4Net XML配置</param>
        /// 时间：2016-01-04 11:16
        /// 备注：
        public Log4NetHelper(string logNetXmlConfig)
            : this(string.Empty, logNetXmlConfig)
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Debug记录
        /// </summary>
        /// <param name="loggerType">日志类型</param>
        /// <param name="message">日志内容</param>
        /// <param name="ex">Exception</param>
        /// 时间：2016-01-04 11:17
        /// 备注：
        public void Debug(LoggerType loggerType, object message, Exception ex = null)
        {
            ILog _logger = LogManager.GetLogger(loggerType.ToString());
            if (ex != null)
                _logger.Debug(SerializeObject(message), ex);
            else
                _logger.Debug(SerializeObject(message));
        }

        /// <summary>
        /// Error记录
        /// </summary>
        /// <param name="loggerType">日志类型</param>
        /// <param name="message">日志内容</param>
        /// <param name="ex">Exception</param>
        /// 时间：2016-01-04 11:17
        /// 备注：
        public void Error(LoggerType loggerType, object message, Exception ex = null)
        {
            var _logger = LogManager.GetLogger(loggerType.ToString());
            if (ex != null)
                _logger.Error(SerializeObject(message), ex);
            else
                _logger.Error(SerializeObject(message));
        }

        /// <summary>
        /// Fatal记录
        /// </summary>
        /// <param name="loggerType">日志类型</param>
        /// <param name="message">日志内容</param>
        /// <param name="ex">Exception</param>
        /// 时间：2016-01-04 11:17
        /// 备注：
        public void Fatal(LoggerType loggerType, object message, Exception ex = null)
        {
            var _logger = LogManager.GetLogger(loggerType.ToString());
            if (ex != null)
                _logger.Fatal(SerializeObject(message), ex);
            else
                _logger.Fatal(SerializeObject(message));
        }

        /// <summary>
        /// Info记录
        /// </summary>
        /// <param name="loggerType">日志类型</param>
        /// <param name="message">日志内容</param>
        /// <param name="ex">Exception</param>
        /// 时间：2016-01-04 11:17
        /// 备注：
        public void Info(LoggerType loggerType, object message, Exception ex = null)
        {
            var _logger = LogManager.GetLogger(loggerType.ToString());
            if (ex != null)
                _logger.Info(SerializeObject(message), ex);
            else
                _logger.Info(SerializeObject(message));
        }

        /// <summary>
        /// Warn记录
        /// </summary>
        /// <param name="loggerType">日志类型</param>
        /// <param name="message">日志内容</param>
        /// <param name="ex">Exception</param>
        /// 时间：2016-01-04 11:17
        /// 备注：
        public void Warn(LoggerType loggerType, object message, Exception ex = null)
        {
            var _logger = LogManager.GetLogger(loggerType.ToString());
            if (ex != null)
                _logger.Warn(SerializeObject(message), ex);
            else
                _logger.Warn(SerializeObject(message));
        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        /// 时间：2016-01-04 11:23
        /// 备注：
        private object SerializeObject(object message)
        {
            if (message is string || message == null)
                return message;
            else
                return JsonConvert.SerializeObject(message, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        #endregion Methods
    }
}