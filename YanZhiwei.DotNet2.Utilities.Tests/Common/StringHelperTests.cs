using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace YanZhiwei.DotNet2.Utilities.Common.Tests
{
    [TestClass()]
    public class StringHelperTests
    {
        [TestMethod()]
        public void ComplementRigthZeroTest()
        {
            string _actual = StringHelper.ComplementRigthZero("Yanzhiwei", 15);
            Assert.AreEqual("Yanzhiwei000000", _actual);
        }

        [TestMethod()]
        public void ComplementLeftZeroTest()
        {
            string _actual = StringHelper.ComplementLeftZero("Yanzhiwei", 15);
            Assert.AreEqual("000000Yanzhiwei", _actual);
        }

        [TestMethod()]
        public void ReverseTest()
        {
            string _actual = StringHelper.Reverse("YanZhiwei");
            Assert.AreEqual<string>("iewihZnaY", _actual);
        }

        [TestMethod()]
        public void ExceptBlanksTest()
        {
            string _actual = StringHelper.ClearBlanks(" 11 22 33 44  ");
            Assert.AreEqual<string>("11223344", _actual);
        }

        [TestMethod()]
        public void ReverseUsingArrayClassTest()
        {
            string _actual = StringHelper.ReverseUsingArrayClass("YanZhiwei");
            Assert.AreEqual<string>("iewihZnaY", _actual);
        }

        [TestMethod()]
        public void ReverseUsingCharacterBufferTest()
        {
            string _actual = StringHelper.ReverseUsingCharacterBuffer("YanZhiwei");
            Assert.AreEqual<string>("iewihZnaY", _actual);
        }

        [TestMethod()]
        public void ReverseUsingStringBuilderTest()
        {
            string _actual = StringHelper.ReverseUsingStringBuilder("YanZhiwei");
            Assert.AreEqual<string>("iewihZnaY", _actual);
        }

        [TestMethod()]
        public void ReverseUsingStackTest()
        {
            string _actual = StringHelper.ReverseUsingStack("YanZhiwei");
            Assert.AreEqual<string>("iewihZnaY", _actual);
        }

        [TestMethod()]
        public void ReverseUsingXORTest()
        {
            string _actual = StringHelper.ReverseUsingXOR("YanZhiwei");
            Assert.AreEqual<string>("iewihZnaY", _actual);
        }

        [TestMethod()]
        public void BuilderDelimiterTest()
        {
            string _actual = StringHelper.BuilderDelimiter("Yan", '-');
            Assert.AreEqual<string>("Y-a-n", _actual);
        }

        [TestMethod()]
        public void WrapTextTest()
        {
            string _actual = StringHelper.WrapText("YanZhiwei", 3);
            Assert.AreEqual<string>(@"Yan
Zhi
wei", _actual);
        }

        [TestMethod()]
        public void RemoveTest()
        {
            string _actual = StringHelper.SubString("Yan&Zhiwei", '&');
            Assert.AreEqual<string>("Yan", _actual);
        }

        [TestMethod()]
        public void RemoveLastTest()
        {
            string _actual = StringHelper.SubStringFromLast("YanZhiwei&", '&');
            Assert.AreEqual<string>("YanZhiwei", _actual);
        }

        [TestMethod()]
        public void ToMD5Test()
        {
            string _actual = StringHelper.ToMD5String("YanZhiwei");
            Assert.AreEqual("b07ec574a666d8e7582885ce334b4d00", _actual);
        }

        [TestMethod()]
        public void ParseThousandthStringTest()
        {
            int _actual = StringHelper.ParseThousandthString("111,222,333");
            Assert.AreEqual(111222333, _actual);
        }

        [TestMethod()]
        public void SubstringTest()
        {
            string _actual = StringHelper.GetFriendly("YanZhiwei", 3);
            Assert.AreEqual("Yan...", _actual);
        }

        [TestMethod()]
        public void UpperFirstCharTest()
        {
            Assert.AreEqual("YanZhiwei", StringHelper.UpperFirstChar("yanZhiwei"));
        }

        [TestMethod()]
        public void LowerFirstCharTest()
        {
            Assert.AreEqual("yanZhiwei", StringHelper.LowerFirstChar("YanZhiwei"));
        }
    }
}