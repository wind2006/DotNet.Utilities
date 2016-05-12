namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// DES(Data Encryption Standard)
    /// DES使用的密钥key为8字节，初始向量IV也是8字节。
    /// </summary>
    public class DESEncryptHelper
    {
        #region Fields

        /// <summary>
        /// 默认加密Key
        /// </summary>
        private const string key = "DotNet2.Utilities";

        /// <summary>
        /// 默认向量
        /// </summary>
        private static byte[] iv = { 0x21, 0x45, 0x65, 0x87, 0x09, 0xBA, 0xDC, 0xEF };

        /// <summary>
        /// The DES
        /// </summary>
        private DES des = null;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="iv">向量</param>
        public DESEncryptHelper(byte[] key, byte[] iv)
        {
            des = new DESCryptoServiceProvider();
            des.Key = key;
            des.IV = iv;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="iv">向量</param>
        /// 时间：2016-01-14 13:23
        /// 备注：
        public DESEncryptHelper(string key, byte[] iv)
        {
            des = new DESCryptoServiceProvider();
            key = key.Substring(0, 8);
            key = key.PadRight(8, ' ');
            des.Key = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            des.IV = iv;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// 生成DES
        /// </summary>
        /// <returns>DES</returns>
        public static DES CreateDES()
        {
            return CreateDES(string.Empty);
        }

        /// <summary>
        /// 根据KEY生成DES
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>DES</returns>
        public static DES CreateDES(string key)
        {
            DES _des = new DESCryptoServiceProvider();
            DESCryptoServiceProvider _desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();
            if (!string.IsNullOrEmpty(key))
            {
                MD5 _md5 = new MD5CryptoServiceProvider();
                _des.Key = ArrayHelper.Copy<byte>(_md5.ComputeHash(Encoding.UTF8.GetBytes(key)), 0, 8);
            }
            else
            {
                _des.Key = _desCrypto.Key;
            }

            _des.IV = _des.IV;
            return _des;
        }

        /// <summary>
        /// 采用默认向量，Key解密
        /// </summary>
        /// <param name="text">需要解密的字符串.</param>
        /// <returns>解密后的字符串</returns>
        public static string Decrypt(string text)
        {
            DESEncryptHelper _helper = new DESEncryptHelper(key, iv);
            return _helper.DecryptString(text);
        }

        /// <summary>
        /// 采用默认向量,KEY加密
        /// </summary>
        /// <param name="text">需要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(string text)
        {
            DESEncryptHelper _helper = new DESEncryptHelper(key, iv);
            return _helper.EncryptString(text);
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="text">需要解密的字符串</param>
        /// <returns>解密后的字符串</returns>
        public string DecryptString(string text)
        {
            byte[] _decryptedData = Convert.FromBase64String(text);

            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    CryptoStream _cryptoStream = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);

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
        /// 加密字符串
        /// </summary>
        /// <param name="text">需要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public string EncryptString(string text)
        {
            byte[] _encryptedData = Encoding.UTF8.GetBytes(text);

            using (MemoryStream ms = new MemoryStream())
            {
                CryptoStream _cryptoStream = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

                _cryptoStream.Write(_encryptedData, 0, _encryptedData.Length);
                _cryptoStream.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        #endregion Methods
    }
}