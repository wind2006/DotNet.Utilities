namespace YanZhiwei.DotNet3._5.Utilities.Common
{
    using YanZhiwei.DotNet2.Utilities.Common;
    using YanZhiwei.DotNet2.Utilities.Core;

    /// <summary>
    /// FrameworkException辅助类
    /// </summary>
    /// 时间：2016-02-26 13:44
    /// 备注：
    public static class FrameworkExceptionHelper
    {
        #region Methods

        /// <summary>
        /// 将InnerException.Data转换为Json字符串
        /// <para>不支持Data中Value是集合数组形式存储</para>
        /// </summary>
        /// <param name="frameworkException">FrameworkException</param>
        /// <returns>Json字符串</returns>
        /// 时间：2016-02-26 13:19
        /// 备注：
        public static string ParseInnerDataToJsonString(this FrameworkException frameworkException)
        {
            ValidateHelper.Begin().NotNull(frameworkException, "FrameworkException");

            return SerializationHelper.JsonSerialize(frameworkException.InnerException.Data);
        }

        #endregion Methods
    }
}