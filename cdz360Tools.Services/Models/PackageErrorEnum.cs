using System.ComponentModel;

namespace cdz360Tools.Services.Models
{
    public enum PackageErrorEnum
    {
        /// <summary>
        /// 初始化为正常
        /// </summary>
        [Description("初始化为正常")]
        Normal,

        /// <summary>
        /// 同步字错误
        /// </summary>
        [Description("同步字错误")]
        SynWordError,

        /// <summary>
        /// 同步字匹配错误
        /// </summary>
        [Description("同步字匹配错误")]
        SynWordMatchError,

        /// <summary>
        /// 长度域错误
        /// </summary>
        [Description("长度域错误")]
        DataLengthError,

        /// <summary>
        /// 包定义的长度和实际收到长度不符合错误
        /// </summary
        [Description("包定义的长度和实际收到长度不符合错误")]
        PackageLengthError,

        /// <summary>
        /// CRC错误
        /// </summary>
        [Description("CRC错误")]
        CRCError,

        /// <summary>
        /// CTU通讯地址错误
        /// </summary>
        [Description("CTU通讯地址错误")]
        CTUADRRError,

        /// <summary>
        /// 包尾错误
        /// </summary>
        [Description("包尾错误")]
        EndTagError,

        /// <summary>
        /// 分析包时未知错误
        /// </summary>
        [Description("分析包时未知错误")]
        ExceptionError
    }
}