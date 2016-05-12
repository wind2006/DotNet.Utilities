using Microsoft.VisualStudio.TestTools.UnitTesting;
using YanZhiwei.DotNet2.Utilities.Models;

namespace YanZhiwei.DotNet2.Utilities.Common.Tests
{
    [TestClass()]
    public class MapHelperTests
    {
        [TestMethod()]
        public void GetQueryLocationTest()
        {
            GeoPoint _geoPointExpected = new GeoPoint(11521, 3412);
            GeoPoint _geoPointActual = MapHelper.GetQueryLocation(new LatLngPoint() { LonX = 115.21212, LatY = 34.121 });
            bool _expected = true;
            bool _actual = EntityHelper.ValueEqual(_geoPointExpected, _geoPointActual);
            Assert.AreEqual(_expected, _actual);
        }
    }
}