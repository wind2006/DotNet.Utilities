namespace YanZhiwei.DotNet2.Utilities.Models
{
    using System;
    using System.Net;

    /// <summary>
    /// Socket 数据
    /// </summary>
    public class SocketData
    {
        #region Properties

        /// <summary>
        /// 异常枚举
        /// </summary>
        public SocketCode Code
        {
            get; set;
        }

        /// <summary>
        /// 缓存数据
        /// </summary>
        public byte[] DataBuffer
        {
            get; set;
        }

        /// <summary>
        /// 发生异常时不为null.
        /// </summary>
        public Exception Ex
        {
            get; set;
        }

        /// <summary>
        /// 当前IP地址,端口号
        /// </summary>
        public IPEndPoint Ip
        {
            get; set;
        }

        /// <summary>
        /// 附加数据
        /// </summary>
        public object Tag
        {
            get; set;
        }

        #endregion Properties
    }
}