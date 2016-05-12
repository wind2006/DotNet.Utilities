namespace YanZhiwei.DotNet3._5.Utilities.Common
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// AES加密
    /// </summary>
    public class AESEncryptHelper
    {
        #region Fields

        /*
         *知识：
         *1.DES一共就有4个参数参与运作：明文、密文、密钥、向量。为了初学者容易理解，可以把4个参数的关系写成：密文=明文+密钥+向量；明文=密文-密钥-向量。为什么要向量这个参数呢？因为如果有一篇文章，有几个词重复，那么这个词加上密钥形成的密文，仍然会重复，这给破解者有机可乘，破解者可以根据重复的内容，猜出是什么词，然而一旦猜对这个词，那么，他就能算出密钥，整篇文章就被破解了！加上向量这个参数以后，每块文字段都会依次加上一段值，这样，即使相同的文字，加密出来的密文，也是不一样的，算法的安全性大大提高！
         *2.Key是24位，IV则是8位
         */

        /// <summary>
        /// ASE对象
        /// </summary>
        private Aes aes = null;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">密钥.</param>
        /// <param name="iv">向量.</param>
        public AESEncryptHelper(byte[] key, byte[] iv)
        {
            this.aes = new AesCryptoServiceProvider();
            this.aes.Key = key;
            this.aes.IV = iv;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// 根据KEY生成DES
        /// </summary>
        /// <param name="key">密钥.</param>
        /// <returns>Aes</returns>
        public static Aes CreateAES(string key)
        {
            Aes _aes = new AesCryptoServiceProvider();
            AesCryptoServiceProvider _desCrypto = (AesCryptoServiceProvider)AesCryptoServiceProvider.Create();
            if (!string.IsNullOrEmpty(key))
            {
                MD5 _md5 = new MD5CryptoServiceProvider();
                _aes.Key = _md5.ComputeHash(Encoding.UTF8.GetBytes(key));
            }
            else
            {
                _aes.Key = _desCrypto.Key;
            }

            _aes.IV = _aes.IV;
            return _aes;
        }

        /// <summary>
        /// 创建ASE加密对象
        /// </summary>
        /// <returns>Aes</returns>
        public static Aes CreateAES()
        {
            return CreateAES(string.Empty);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="text">需要解密的字符串</param>
        /// <returns>解密后字符串</returns>
        public string DecryptString(string text)
        {
            byte[] _decryptedData = Convert.FromBase64String(text);

            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    CryptoStream _cryptoStream = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);

                    _cryptoStream.Write(_decryptedData, 0, _decryptedData.Length);

                    _cryptoStream.FlushFinalBlock();
                }
                catch
                {
                    return "N/A";
                }

                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="text">需要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public string EncryptString(string text)
        {
            byte[] _encryptedData = Encoding.UTF8.GetBytes(text);

            using (MemoryStream ms = new MemoryStream())
            {
                CryptoStream _cryptoStream = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);

                _cryptoStream.Write(_encryptedData, 0, _encryptedData.Length);
                _cryptoStream.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        #endregion Methods
    }
}