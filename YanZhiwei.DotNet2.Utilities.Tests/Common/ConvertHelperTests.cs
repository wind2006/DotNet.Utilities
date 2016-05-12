using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using YanZhiwei.DotNet2.Utilities.Enums;

namespace YanZhiwei.DotNet2.Utilities.Common.Tests
{
    [TestClass()]
    public class ConvertHelperTests
    {
        [TestMethod()]
        public void ToHexBinDecOctTest()
        {
            #region 十进制转换成十六进制测试

            string _testString = "15";
            string _expected = "f";
            string _actual = ConvertHelper.ToHexBinDecOct(_testString, Conversion.Decimal, Conversion.Hexadecimal);
            Assert.AreEqual(_expected, _actual);

            #endregion 十进制转换成十六进制测试

            #region 十进制转换二进制测试

            string _testString2 = "3";
            string _expected2 = "00000011";
            string _actual2 = ConvertHelper.ToHexBinDecOct(_testString2, Conversion.Decimal, Conversion.Binary);
            Assert.AreEqual(_expected2, _actual2);

            #endregion 十进制转换二进制测试

            #region 十进制转换八进制

            string _testString3 = "22";
            string _expected3 = "26";
            string _actual3 = ConvertHelper.ToHexBinDecOct(_testString3, Conversion.Decimal, Conversion.Octal);
            Assert.AreEqual(_expected3, _actual3);

            #endregion 十进制转换八进制

            #region 十六进制转换成十进制测试

            string _testString4 = "f";
            string _expected4 = "15";
            string _actual4 = ConvertHelper.ToHexBinDecOct(_testString4, Conversion.Hexadecimal, Conversion.Decimal);
            Assert.AreEqual(_expected4, _actual4);

            #endregion 十六进制转换成十进制测试

            #region 十六进制转换成二进制测试

            string _testString5 = "f";
            string _expected5 = "00001111";
            string _actual5 = ConvertHelper.ToHexBinDecOct(_testString5, Conversion.Hexadecimal, Conversion.Binary);
            Assert.AreEqual(_expected5, _actual5);

            #endregion 十六进制转换成二进制测试

            #region 十六进制转换成八进制测试

            string _testString6 = "f";
            string _expected6 = "17";
            string _actual6 = ConvertHelper.ToHexBinDecOct(_testString6, Conversion.Hexadecimal, Conversion.Octal);
            Assert.AreEqual(_expected6, _actual6);

            #endregion 十六进制转换成八进制测试

            #region 二进制转换十进制

            string _testString7 = "1111";
            string _expected7 = "15";
            string _actual7 = ConvertHelper.ToHexBinDecOct(_testString7, Conversion.Binary, Conversion.Decimal);
            Assert.AreEqual(_expected7, _actual7);

            #endregion 二进制转换十进制

            #region 二进制转换十六进制

            string _testString8 = "1111";
            string _expected8 = "f";
            string _actual8 = ConvertHelper.ToHexBinDecOct(_testString8, Conversion.Binary, Conversion.Hexadecimal);
            Assert.AreEqual(_expected8, _actual8);

            #endregion 二进制转换十六进制

            #region 二进制转换八进制

            string _testString9 = "1111";
            string _expected9 = "17";
            string _actual9 = ConvertHelper.ToHexBinDecOct(_testString9, Conversion.Binary, Conversion.Octal);
            Assert.AreEqual(_expected9, _actual9);

            #endregion 二进制转换八进制
        }

        [TestMethod()]
        public void ToStringTest()
        {
            int[] _array = new int[3] { 1, 2, 3 };
            string _actual = ConvertHelper.ToString<int>(_array, "-");
            Assert.AreEqual("1-2-3", _actual);
        }

        [TestMethod()]
        public void ToDateTest()
        {
            Assert.AreEqual(new DateTime(2014, 10, 10), ConvertHelper.ToDateOrDefault("2014-10-10", new DateTime(2014, 10, 12)));
        }

        [TestMethod()]
        public void ToStringBaseTest()
        {
            Assert.AreEqual(123, ConvertHelper.ToStringBase<int>("123"));
        }

        [TestMethod()]
        public void ToStringTest1()
        {
            Assert.AreEqual("123", ConvertHelper.ToStringOrDefault(123, "456"));
        }

        [TestMethod()]
        public void ToDecimalTest()
        {
            Assert.AreEqual(123m, ConvertHelper.ToDecimalOrDefault(123, 456));
        }

        [TestMethod()]
        public void ToDoubleTest()
        {
            Assert.AreEqual(123d, ConvertHelper.ToDoubleOrDefault(123, 456));
        }

        [TestMethod()]
        public void ToInt32Test()
        {
            Assert.AreEqual(123, ConvertHelper.ToInt32OrDefault(123, 456));
        }

        [TestMethod()]
        public void ToInt64Test()
        {
            Assert.AreEqual(123, ConvertHelper.ToInt64OrDefault(123, 456));
        }

        [TestMethod()]
        public void ToIntTest()
        {
            Assert.AreEqual(123, ConvertHelper.ToIntOrDefault(123, 456));
        }

        [TestMethod()]
        public void ToInt16Test()
        {
            Assert.AreEqual(123, ConvertHelper.ToShortOrDefault(123, 456));
        }

        [TestMethod()]
        public void ToChineseMonthTest()
        {
            Assert.AreEqual("一", ConvertHelper.ToChineseMonth(1));
            Assert.AreEqual("二", ConvertHelper.ToChineseMonth(2));
            Assert.AreEqual("三", ConvertHelper.ToChineseMonth(3));
        }

        [TestMethod()]
        public void ToChineseDayTest()
        {
            Assert.AreEqual("一", ConvertHelper.ToChineseDay(1));
            Assert.AreEqual("二", ConvertHelper.ToChineseDay(2));
            Assert.AreEqual("三", ConvertHelper.ToChineseDay(3));
        }

        [TestMethod()]
        public void ToChineseDateTest()
        {
            DateTime _date = new DateTime(2014, 10, 10);
            Assert.AreEqual("甲午年九月十七", ConvertHelper.ToChineseDate(_date));
        }

        [TestMethod()]
        public void ToStringTest2()
        {
            Assert.AreEqual("2014-10-10", ConvertHelper.ToStringOrDefault(new DateTime(2014, 10, 10), "yyyy-MM-dd", "--"));
        }

        [TestMethod()]
        public void ToBooleanTest()
        {
            bool _actual = ConvertHelper.ToBooleanOrDefault("True", false);
            Assert.IsTrue(_actual);
        }

        [TestMethod()]
        public void ToByteTest()
        {
            byte _actual = ConvertHelper.ToByteOrDefault("1", 0);
            Assert.AreEqual(1, _actual);
        }
    }
}