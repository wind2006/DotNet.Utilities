using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;

namespace YanZhiwei.DotNet2.Utilities.Common.Tests
{
    [TestClass()]
    public class DESEncryptHelperTests
    {
        private DESEncryptHelper desHelper = null;

        [TestInitialize]
        public void Init()
        {
            DES _newDes = DESEncryptHelper.CreateDES("yanzhiweizhuzhouhunanchina");
            _newDes.IV = new byte[8] { 0x01, 0x02, 0x03, 0x4, 0x05, 0x06, 0x07, 0x08 };
            desHelper = new DESEncryptHelper(_newDes.Key, _newDes.IV);
        }

      

        [TestMethod()]
        public void EncryptStringTest()
        {
            string _actual = desHelper.EncryptString("YanZhiwei");
            Assert.AreEqual("S928WewvXWKJ6pIoxT91qw==", _actual);
        }

        [TestMethod()]
        public void DecryptStringTest()
        {
            string _actual = desHelper.DecryptString("S928WewvXWKJ6pIoxT91qw==");
            Assert.AreEqual("YanZhiwei", _actual);
        }
    }
}