using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

namespace YanZhiwei.DotNet2.Utilities.Common.Tests
{
    [TestClass()]
    public class UnixEpochHelperTests
    {
        [TestMethod()]
        public void DateTimeFromUnixTimestampMillisTest()
        {
            DateTime _actual = UnixEpochHelper.DateTimeFromUnixTimestampMillis(1422949956408);
            DateTime _expected = new DateTime(2015, 02, 03, 7, 52, 36);
            Assert.AreEqual(_expected.ToShortDateString(), _actual.ToShortDateString());
            Assert.AreEqual(_expected.ToShortTimeString(), _actual.ToShortTimeString());
            Assert.AreNotEqual(_expected, _actual);
        }
    }
}