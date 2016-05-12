namespace YanZhiwei.DotNet2.Utilities.Core
{
    using System;
    using System.Diagnostics;
    using System.IO;

    using YanZhiwei.DotNet2.Utilities.Common;

    /// <summary>
    /// TraceListener实现
    /// </summary>
    public class ProjectTraceListener : TraceListener
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="filePath">日志路径</param>
        public ProjectTraceListener(string filePath)
        {
            this.FilePath = filePath;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 日志路径
        /// </summary>
        public string FilePath
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 在派生类中被重写时，向在该派生类中所创建的侦听器写入指定消息。
        /// </summary>
        /// <param name="message">要写入的消息。</param>
        public override void Write(string message)
        {
            File.AppendAllText(this.FilePath, message);
        }

        /// <summary>
        /// Writes the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="category">The category.</param>
        public override void Write(object obj, string category)
        {
            string _message = string.Empty;
            if (!string.IsNullOrEmpty(category))
            {
                _message = category + ":";
            }

            if (obj is Exception)
            {
                var _ex = (Exception)obj;
                _message += _ex.Message + Environment.NewLine;
                _message += _ex.StackTrace;
            }
            else if (null != obj)
            {
                _message += obj.ToString();
            }

            this.WriteLine(_message);
        }

        /// <summary>
        /// 在派生类中被重写时，向在该派生类中所创建的侦听器写入消息，后跟行结束符。
        /// </summary>
        /// <param name="message">要写入的消息。</param>
        public override void WriteLine(string message)
        {
            File.AppendAllText(this.FilePath, DateTime.Now.FormatDate(1) + "   " + message + Environment.NewLine);
        }

        #endregion Methods
    }
}