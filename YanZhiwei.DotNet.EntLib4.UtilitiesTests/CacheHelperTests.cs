using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace YanZhiwei.DotNet.EntLib4.Utilities.Tests
{
    [TestClass()]
    public class CacheHelperTests
    {
        [TestMethod()]
        public void AddAbsoluteTimeTest()
        {
            CacheHelper _cacheHelper = new CacheHelper();
            _cacheHelper.AddAbsoluteTime("Name", "YanZhiwei", DateTime.Now.AddMinutes(1));
            Assert.AreEqual(_cacheHelper.GetData<string>("Name"), "YanZhiwei");
            Thread.Sleep(new TimeSpan(0, 1, 10));
            Assert.IsTrue(string.IsNullOrEmpty(_cacheHelper.GetData<string>("Name")));
        }
    }
}