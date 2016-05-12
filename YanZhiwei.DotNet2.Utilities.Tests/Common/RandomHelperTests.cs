using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace YanZhiwei.DotNet2.Utilities.Common.Tests
{
    [TestClass()]
    public class RandomHelperTests
    {
        [TestMethod()]
        public void NextMacAddressTest()
        {
            string _newMac = RandomHelper.NextMacAddress();
            Assert.Inconclusive(_newMac);
            Assert.AreEqual(17, _newMac.Length);
        }

        [TestMethod()]
        public void NextDoubleTest()
        {
            double _newDouble = RandomHelper.NextDouble(1.5, 1.7);
            Assert.Inconclusive(_newDouble.ToString());
            bool _result = (_newDouble > 1.5) && (_newDouble < 1.7);
            Assert.AreEqual(true, _result);
        }

        [TestMethod()]
        public void NetxtStringTest()
        {
            string _newString = RandomHelper.NetxtString(4, false);
            Assert.Inconclusive(_newString);
            Assert.AreEqual(4, _newString);
        }

        [TestMethod()]
        public void NetxtStringTest1()
        {
            string _newString = RandomHelper.NetxtString(RandomHelper.RandomString09AZ, 4, false);
            Assert.Inconclusive(_newString);
            Assert.AreEqual(4, _newString);
        }

        [TestMethod()]
        public void NextDateTimeTest()
        {
            DateTime _newTime = RandomHelper.NextTime();
            Assert.Inconclusive(_newTime.ToString());
        }
    }
}