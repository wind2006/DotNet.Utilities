using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace YanZhiwei.DotNet.International.Utilities.Tests
{
    [TestClass()]
    public class PinYinHelperTests
    {
        [TestMethod()]
        public void GetSimplePinYinTest()
        {
            Assert.AreEqual("YZW", PinYinHelper.GetSimplePinYin("言志伟"));
            Assert.AreEqual("XTLTEST1", PinYinHelper.GetSimplePinYin("斜土路Test1"));
        }

        [TestMethod()]
        public void GetAllPinYinTest()
        {
            Assert.AreEqual("YANZHIWEI", PinYinHelper.GetAllPinYin("言志伟"));
        }
    }
}