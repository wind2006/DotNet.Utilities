

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace YanZhiwei.DotNet2.Utilities.Common.Tests
{
    [TestClass()]
    public class ASCIIHelperTests
    {
        [TestMethod()]
        public void ToASCIITest()
        {
            int _actual = ASCIIHelper.ToASCII('.');
            Assert.AreEqual(46, _actual);

            byte[] _actualArray = ASCIIHelper.ToASCII("Hello");
            CollectionAssert.AreEqual(new byte[5] { 72, 101, 108, 108, 111 }, _actualArray);
        }

        [TestMethod()]
        public void ParseASCIITest()
        {
            char _actual = ASCIIHelper.ParseASCII(46);
            Assert.AreEqual('.', _actual);
        }
    }
}