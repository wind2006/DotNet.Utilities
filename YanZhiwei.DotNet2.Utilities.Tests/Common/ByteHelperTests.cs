using Microsoft.VisualStudio.TestTools.UnitTesting;
using YanZhiwei.DotNet2.Utilities.Enums;

namespace YanZhiwei.DotNet2.Utilities.Common.Tests
{
    [TestClass()]
    public class ByteHelperTests
    {
        [TestMethod()]
        public void GetHighTest()
        {
            Assert.AreEqual(0x0A, ByteHelper.GetHigh(0xAD));
        }

        [TestMethod()]
        public void GetLowTest()
        {
            Assert.AreEqual(0x0D, ByteHelper.GetLow(0xAD));
        }

        [TestMethod()]
        public void ParseBinaryStringTest()
        {
            Assert.AreEqual(0xFF, ByteHelper.ParseBinaryString("11111111"));
        }

        [TestMethod()]
        public void ParseBinaryStringToBytesTest()
        {
            CollectionAssert.AreEqual(new byte[2] { 0xFF, 0xFF }, ByteHelper.ParseBinaryStringToBytes("1111111111111111"));
        }

        [TestMethod()]
        public void ParseHexStringTest()
        {
            CollectionAssert.AreEqual(new byte[2] { 0xFF, 0xFE }, ByteHelper.ParseHexString("FFFE"));
        }

        [TestMethod()]
        public void ParseHexStringWithDelimiterTest()
        {
            CollectionAssert.AreEqual(new byte[2] { 0xFF, 0xFE }, ByteHelper.ParseHexStringWithDelimiter("FF-FE", "-"));
        }

        [TestMethod()]
        public void ParsePercentTest()
        {
            Assert.AreEqual(128, ByteHelper.ParsePercent(50));
        }

        [TestMethod()]
        public void ToBinaryStringTest()
        {
            Assert.AreEqual("1111111111111111", ByteHelper.ToBinaryString(new byte[2] { 0xFF, 0xFF }));
            Assert.AreEqual("11111111", ByteHelper.ToBinaryString(0xFF));
        }

        [TestMethod()]
        public void ToBytesTest()
        {
            CollectionAssert.AreEqual(new byte[2] { 0x01, 0x02 }, ByteHelper.ToBytes(258));
            CollectionAssert.AreEqual(new byte[2] { 0x08, 0x00 }, ByteHelper.ToBytes(8, 2));
        }

        [TestMethod()]
        public void ToHexStringTest()
        {
            Assert.AreEqual("FFFE", ByteHelper.ToHexString(new byte[2] { 255, 254 }, ToHexadecimal.ConvertAll));
        }

        [TestMethod()]
        public void ToHexStringWithBlankTest()
        {
            Assert.AreEqual("FF FE", ByteHelper.ToHexStringWithBlank(new byte[2] { 255, 254 }));
        }

        [TestMethod()]
        public void ToHexStringWithDelimiterTest()
        {
            Assert.AreEqual("FF-FE", ByteHelper.ToHexStringWithDelimiter(new byte[2] { 255, 254 }, "-"));
        }

        [TestMethod()]
        public void CalcPercentageTest()
        {
            Assert.AreEqual(50.20m, ByteHelper.CalcPercentage(128));
        }
    }
}