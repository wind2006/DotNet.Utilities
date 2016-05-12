namespace YanZhiwei.DotNet4.Utilities.Common
{
    using System;

    /// <summary>
    ///  转换帮助类
    /// </summary>
    /// 时间：2016-01-14 12:21
    /// 备注：
    public static class ConvertHelper
    {
        #region Methods

        /// <summary>
        /// 字符串转GUID
        /// </summary>
        /// <param name="data">字符串</param>
        /// <returns>若转换失败，则返回Guid.Empty</returns>
        public static Guid ToGuid(this string data)
        {
            Guid _result = Guid.Empty;
            if (Guid.TryParse(data, out _result))
                return _result;
            else
                return Guid.Empty;
        }

        #endregion Methods
    }
}