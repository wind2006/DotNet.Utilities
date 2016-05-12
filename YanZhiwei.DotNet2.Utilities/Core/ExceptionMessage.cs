namespace YanZhiwei.DotNet2.Utilities.Core
{
    using System;
    using System.Text;

    using YanZhiwei.DotNet2.Utilities.Common;

    /// <summary>
    /// 异常信息封装类
    /// </summary>
    public class ExceptionMessage
    {
        #region Constructors

        /// <summary>
        /// 以自定义用户信息和异常对象实例化一个异常信息对象
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="userMessage">自定义用户信息</param>
        /// <param name="isHideStackTrace">是否隐藏异常堆栈信息</param>
        public ExceptionMessage(Exception ex, string userMessage, bool isHideStackTrace)
        {
            UserMessage = string.IsNullOrEmpty(userMessage) ? ex.Message : userMessage;
            StringBuilder _builder = new StringBuilder();
            ExceptionInfo = string.Empty;
            int _count = 0;
            string _appString = string.Empty;
            while (ex != null)
            {
                if (_count > 0)
                {
                    _appString += "    ";
                }

                ExceptionInfo = ex.Message;
                _builder.AppendLine(_appString + "异常消息: " + ex.Message);
                _builder.AppendLine(_appString + "异常类型: " + ex.GetType().FullName);
                _builder.AppendLine(_appString + "异常方法: " + (ex.TargetSite == null ? null : ex.TargetSite.Name));
                _builder.AppendLine(_appString + "异常来源: " + ex.Source);
                if (!isHideStackTrace && ex.StackTrace != null)
                {
                    _builder.AppendLine(_appString + "异常堆栈: " + ex.StackTrace);
                }

                if (ex.InnerException != null)
                {
                    _builder.AppendLine(_appString + "内部异常: ");
                    _count++;
                }

                ex = ex.InnerException;
            }

            DetailMessage = _builder.ToString();
            _builder.Clear();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 异常输出的详细描述，包含异常消息，规模信息，异常类型，异常源，引发异常的方法及内部异常信息
        /// </summary>
        public string DetailMessage
        {
            get;
            private set;
        }

        /// <summary>
        /// 直接的Exception异常信息，即e.Message属性值
        /// </summary>
        public string ExceptionInfo
        {
            get;
            private set;
        }

        /// <summary>
        /// 用户信息，用于报告给用户的异常消息
        /// </summary>
        public string UserMessage
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return DetailMessage;
        }

        #endregion Methods
    }
}