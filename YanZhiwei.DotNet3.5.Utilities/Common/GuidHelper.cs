namespace YanZhiwei.DotNet3._5.Utilities.Common
{
    using System;

    /// <summary>
    /// Guid帮助类
    /// </summary>
    /// 时间：2016-01-11 9:20
    /// 备注：
    public static class GuidHelper
    {
        #region Methods

        /// <summary>
        /// 将Guid字符串转换为Guid
        /// </summary>
        /// <param name="guidString">字符串</param>
        /// <returns>Guid</returns>
        /// 时间：2016-01-11 9:20
        /// 备注：
        public static Guid? TryParse(this string guidString)
        {
            if (string.IsNullOrEmpty(guidString))
                return null;
            try
            {
                Guid _newGuid = new Guid(guidString);
                return _newGuid;
            }
            catch
            {
                return null;
            }
        }

        #endregion Methods
    }
}