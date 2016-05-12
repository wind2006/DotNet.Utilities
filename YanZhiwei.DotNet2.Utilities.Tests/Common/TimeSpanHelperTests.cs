using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace YanZhiwei.DotNet2.Utilities.Common.Tests
{
    [TestClass()]
    public class TimeSpanHelperTests
    {
        [TestMethod()]
        public void ParseTimeStringTest()
        {
            Assert.AreEqual(new TimeSpan(12, 12, 0), TimeSpanHelper.ParseTimeString("12:12:00"));
        }

        [TestMethod()]
        public void SetTodayTest()
        {
            TimeSpan _actual = TimeSpanHelper.ParseTimeString("12:12:00").SetToday();
            TimeSpan _expected = new TimeSpan(DateTime.Now.Day, 12, 12, 0);
            Assert.AreEqual(_expected, _actual);
        }

        [TestMethod()]
        public void FormatTimeTest()
        {
            Assert.AreEqual("14:01", TimeSpanHelper.FormatTime(new TimeSpan(14, 1, 2)));
        }
    }
}