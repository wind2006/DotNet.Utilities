using Microsoft.VisualStudio.TestTools.UnitTesting;
using YanZhiwei.DotNet2.Utilities.Models;

namespace YanZhiwei.DotNet2.Utilities.Common.Tests
{
    [TestClass()]
    public class WGSGCJLatLonHelperTests
    {
        /*
         * 测试接口：
         * 1.http://api.zdoz.net/interfaces.aspx#gps-link
         */

        [TestMethod()]
        public void GCJ02ToWGS84Test()
        {
            LatLngPoint _gcj02PointExpected = new LatLngPoint() { LatY = 34.122340014975919, LonX = 115.20642637776433 };
            LatLngPoint _gcj02PointActual = WGSGCJLatLonHelper.GCJ02ToWGS84(new LatLngPoint() { LonX = 115.21212, LatY = 34.121 });
            bool _expected = true;
            bool _actual = EntityHelper.ValueEqual(_gcj02PointExpected, _gcj02PointActual);
            Assert.AreEqual(_expected, _actual);
        }

        [TestMethod()]
        public void WGS84ToGCJ02Test()
        {
            LatLngPoint _gcj02PointExpected = new LatLngPoint() { LatY = 34.119651841940737, LonX = 115.21780492356538 };
            LatLngPoint _gcj02PointActual = WGSGCJLatLonHelper.WGS84ToGCJ02(new LatLngPoint() { LonX = 115.21212, LatY = 34.121 });
            bool _expected = true;
            bool _actual = EntityHelper.ValueEqual(_gcj02PointExpected, _gcj02PointActual);
            Assert.AreEqual(_expected, _actual);
        }
    }
}