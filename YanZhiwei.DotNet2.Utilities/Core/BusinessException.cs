namespace YanZhiwei.DotNet2.Utilities.Core
{
    using System;

    /// <summary>
    /// 业务异常，用于在后端抛出到前端做相应处理
    /// </summary>
    [Serializable]
    public class BusinessException : Exception
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// 时间：2016-01-06 16:18
        /// 备注：
        public BusinessException()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="inner">内部异常</param>
        /// 时间：2016-02-24 16:35
        /// 备注：
        public BusinessException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误消息</param>
        /// 时间：2016-01-06 16:18
        /// 备注：
        public BusinessException(string message)
            : this("error", message)
        {
        }

        /// <summary>
        /// 构造消息
        /// </summary>
        /// <param name="name">错误名称</param>
        /// <param name="message">错误消息</param>
        /// 时间：2016-01-06 16:21
        /// 备注：
        public BusinessException(string name, string message)
            : base(message)
        {
            this.Name = name;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="errorCode">枚举</param>
        /// 时间：2016-01-06 16:21
        /// 备注：
        public BusinessException(string message, Enum errorCode)
            : this("error", message, errorCode)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">错误名称</param>
        /// <param name="message">错误消息</param>
        /// <param name="errorCode">枚举</param>
        /// 时间：2016-01-06 16:22
        /// 备注：
        public BusinessException(string name, string message, Enum errorCode)
            : base(message)
        {
            this.Name = name;
            this.ErrorCode = errorCode;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 错误代码枚举
        /// </summary>
        public Enum ErrorCode
        {
            get; set;
        }

        /// <summary>
        /// 错误名称
        /// </summary>
        public string Name
        {
            get; set;
        }

        #endregion Properties
    }
}