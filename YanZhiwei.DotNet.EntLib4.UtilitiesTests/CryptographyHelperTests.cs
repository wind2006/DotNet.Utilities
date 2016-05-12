using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace YanZhiwei.DotNet.EntLib4.Utilities.Tests
{
    [TestClass()]
    public class CryptographyHelperTests
    {
        [TestMethod()]
        public void EncryptSymmetricTest()
        {
            string _actual = CryptographyHelper.EncryptSymmetric("TripleDESCryptoServiceProvider", "yanzhiwei");
            Assert.AreEqual(CryptographyHelper.DecryptSymmetric("TripleDESCryptoServiceProvider", _actual), "yanzhiwei");
        }

        [TestMethod()]
        public void CreateHashTest()
        {
            string _actual = CryptographyHelper.CreateHash("SHA1Managed", "YanZhiwei");
            bool _succesd = CryptographyHelper.CompareHash("SHA1Managed", "YanZhiwei", _actual);
            Assert.IsTrue(_succesd);
        }
    }
}