namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System;

    /// <summary>
    /// 十六进制帮助类
    /// </summary>
    public static class HexHelper
    {
        #region Methods

        /// <summary>
        /// 将十六进制字符串转换成十进制
        /// <para> eg:FF=>255</para>
        /// </summary>
        /// <param name="hexString">十六进制字符串</param>
        /// <returns>Decimal</returns>
        public static Int64 ParseHexString(this string hexString)
        {
            return Convert.ToInt64(hexString, 16);
        }

        /// <summary>
        ///将INT转换成十六进制字符串
        /// </summary>
        /// <param name="number">int</param>
        /// <returns>十六进制字符串</returns>
        public static string ToHexString(int number)
        {
            string _hex = string.Format("{0:X}", number);
            if (_hex.Length % 2 != 0)
                _hex = string.Format("0{0}", _hex);
            return _hex;
        }

        #endregion Methods
    }
}