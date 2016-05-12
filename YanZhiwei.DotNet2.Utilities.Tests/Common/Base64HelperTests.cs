using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace YanZhiwei.DotNet2.Utilities.Common.Tests
{
    [TestClass()]
    public class Base64HelperTests
    {
        [TestMethod()]
        public void Base64DecodeTest()
        {
            string _actual = Base64Helper.ParseBase64String("6KiA5b+X5Lyf");
            Assert.AreEqual("言志伟", _actual);
        }

        [TestMethod()]
        public void Base64EncodeTest()
        {
            string _actual = Base64Helper.ToBase64String("言志伟");
            Assert.AreEqual("6KiA5b+X5Lyf", _actual);
        }
    }
}