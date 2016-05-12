namespace YanZhiwei.DotNet4.Utilities.Common
{
    using System;

    /// <summary>
    /// Guid帮助类
    /// </summary>
    /// 时间：2016-01-11 9:46
    /// 备注：
    public static class GuidHelper
    {
        #region Methods

        /// <summary>
        /// 转换guid字符串
        /// </summary>
        /// <param name="guidString">字符串</param>
        /// <returns>Guid</returns>
        /// 时间：2016-01-11 9:46
        /// 备注：
        public static Guid? TryParse(this string guidString)
        {
            Guid? _guid = null;
            if (!string.IsNullOrEmpty(guidString))
            {
                Guid _newGuid;
                if (Guid.TryParse(guidString, out _newGuid))
                {
                    return _newGuid;
                }
            }
            return _guid;
        }

        #endregion Methods
    }
}