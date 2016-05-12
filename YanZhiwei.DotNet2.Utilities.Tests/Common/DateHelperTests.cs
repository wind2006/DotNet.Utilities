using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using YanZhiwei.DotNet2.Utilities.Enums;
using YanZhiwei.DotNet2.Utilities.Common;

namespace YanZhiwei.DotNet2.Utilities.Common.Tests
{
    [TestClass()]
    public class DateHelperTests
    {
        [TestMethod()]
        public void DateDiffTest()
        {
            DateTime _start = new DateTime(2014, 12, 12);
            DateTime _end = new DateTime(2014, 12, 24);
            int _actual = DateHelper.GetDateDiff(_start, _end, DatePart.day);
            Assert.AreEqual(12, _actual);
        }

        [TestMethod()]
        public void GetDaysTest()
        {
            DateTime _time = new DateTime(2014, 12, 24);
            int _actual = DateHelper.GetDays(_time);
            Assert.AreEqual(31, _actual);
        }

        [TestMethod()]
        public void GetFriendlyStringTest()
        {
            DateTime _time = new DateTime(2015, 3, 3, 9, 48, 0);
            string _friendlyString = _time.GetFriendlyString();
            Assert.Inconclusive(_friendlyString);
        }

        [TestMethod()]
        public void ParseExactTest()
        {
            DateTime _actual = DateHelper.ParseDateTimeString("12:24", "HH:mm");
             _actual = DateHelper.ParseDateTimeString("20040727", "yyyyMMdd");
            Assert.Inconclusive(_actual.ToString("yyyy-MM-dd HH:mm:ss"));
        }


    }
}