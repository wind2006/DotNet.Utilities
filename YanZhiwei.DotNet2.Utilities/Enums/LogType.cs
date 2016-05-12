namespace YanZhiwei.DotNet2.Utilities.Enums
{
    using System.ComponentModel;

    #region Enumerations

    /// <summary>
    /// 日志级别
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// 致命错误
        /// </summary>
        [Description("致命错误")]
        Fatal,

        /// <summary>
        /// 错误
        /// </summary>
        [Description("错误")]
        Error,

        /// <summary>
        /// 警告
        /// </summary>
        [Description("警告")]
        Warn,

        /// <summary>
        /// 信息
        /// </summary>
        [Description("信息")]
        Info,

        /// <summary>
        /// 调试
        /// </summary>
        [Description("调试")]
        Debug
    }

    #endregion Enumerations
}