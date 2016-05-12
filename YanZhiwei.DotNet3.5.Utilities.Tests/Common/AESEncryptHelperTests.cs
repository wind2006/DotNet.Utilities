using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;
using YanZhiwei.DotNet3._5.Utilities.Common;

namespace YanZhiwei.DotNet3._5.UtilitiesTests.Common
{
    [TestClass()]
    public class AESEncryptHelperTests
    {
        private AESEncryptHelper aesHelper = null;

        [TestInitialize]
        public void Init()
        {
            Aes _newAES = AESEncryptHelper.CreateAES("yanzhiweizhuzhouhunanchina");
            _newAES.IV = new byte[16] { 0x01, 0x02, 0x03, 0x4, 0x05, 0x06, 0x07, 0x08, 0x01, 0x02, 0x03, 0x4, 0x05, 0x06, 0x07, 0x08 };
            aesHelper = new AESEncryptHelper(_newAES.Key, _newAES.IV);
        }

        [TestMethod()]
        public void EncryptStringTest()
        {
            string _actual = aesHelper.EncryptString("YanZhiwei");
            Assert.AreEqual("v4M1o7AhQ4EOVLxbs4ZIzQ==", _actual);
        }

        [TestMethod()]
        public void DecryptStringTest()
        {
            string _actual = aesHelper.DecryptString("v4M1o7AhQ4EOVLxbs4ZIzQ==");
            Assert.AreEqual("YanZhiwei", _actual);
        }
    }
}