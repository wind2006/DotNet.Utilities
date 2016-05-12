namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// MD5加密帮助类
    /// </summary>
    /// 时间：2015-11-06 9:22
    /// 备注：
    public static class MD5EncryptHelper
    {
        #region Methods

        /// <summary>
        /// 验证随机加密的MD5
        /// </summary>
        /// <param name="data">需要判断的字符串</param>
        /// <param name="rmd5">MD5 GUID</param>
        /// <returns>是否相等</returns>
        /// 时间：2015-11-06 9:24
        /// 备注：
        public static bool EqualsRandomMD5(this string data, Guid rmd5)
        {
            byte[] _array = rmd5.ToByteArray();
            byte _randomKey = _array[0];
            using (var md5Provider = new MD5CryptoServiceProvider())
            {
                data += _randomKey;
                byte[] _bytes = Encoding.UTF8.GetBytes(data);
                byte[] _hash = md5Provider.ComputeHash(_bytes);
                for (int i = 1; i < 16; i++)
                {
                    if (_hash[i] != _array[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// 生成随机加密的
        /// </summary>
        /// <param name="data">需要加密字符串</param>
        /// <returns>MD5加密Guid</returns>
        /// 时间：2015-11-06 9:20
        /// 备注：
        public static Guid ToRandomMD5(this string data)
        {
            using (MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider())

            {
                //生成256以内的随机数
                byte _randomKey = (byte)Math.Abs(new object().GetHashCode() % 256);
                data += _randomKey;
                byte[] _array = Encoding.UTF8.GetBytes(data);
                byte[] _hash = md5Provider.ComputeHash(_array);
                _hash[0] = _randomKey;
                return new Guid(_hash);
            }
        }

        #endregion Methods
    }
}