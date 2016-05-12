namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System;

    /// <summary>
    /// Decimal 帮助类
    /// </summary>
    public static class DecimalHelper
    {
        #region Methods

        /// <summary>
        /// 计算百分比(保持两位小数)
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="total">The total.</param>
        /// <returns>百分比</returns>
        public static decimal CalcPercentage(decimal value, decimal total)
        {
            return Math.Round((decimal)(100 * value) / total, 2);
        }

        /// <summary>
        /// 转换成钱表示形式（保持两位小数）
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>保持两位小数</returns>
        public static decimal ToMoney(this decimal data)
        {
            return Math.Round(data, 2);
        }

        #endregion Methods
    }
}