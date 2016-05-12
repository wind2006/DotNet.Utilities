using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using YanZhiwei.DotNet2.Utilities.Common;

namespace YanZhiwei.DotNet2.Utilities.Common.Tests
{
    [TestClass()]
    public class BCDHelperTests
    {
        [TestMethod()]
        public void From8421BCDToBytesTest()
        {
            CollectionAssert.AreEqual(new byte[2] { 0x01, 0x10 }, BCDHelper.Parse8421BCDString("0110", false));
            CollectionAssert.AreEqual(new byte[2] { 0x10, 0x01 }, BCDHelper.Parse8421BCDString("0110", true));
            CollectionAssert.AreEqual(BCDHelper.Parse8421BCDString("01", true), BCDHelper.Parse8421BCDNumber(1, true));
        }

        [TestMethod()]
        public void To8421BCDStringTest()
        {
            Assert.AreEqual("1001", BCDHelper.To8421BCDString(new byte[2] { 0x01, 0x10 }, true));
            Assert.AreEqual("0110", BCDHelper.To8421BCDString(new byte[2] { 0x01, 0x10 }, false));
        }

        [TestMethod()]
        public void From8421BCDToByteTest()
        {
            byte _actualValue = BCDHelper.Parse8421BCDNumber(56);
            string _expected = "1010110", _actual = Convert.ToString(_actualValue, 2);
            Assert.AreEqual(_expected, _actual);
        }
    }
}