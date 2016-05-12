using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using YanZhiwei.DotNet2.Utilities.Common;

namespace YanZhiwei.DotNet2.Utilities.Common.Tests
{
    [Flags]
    public enum AreaMode
    {
        NONE = 0,
        CITY = 1,
        TOWN = 2,
        ROAD = 4,
        CITYTOWN = 8,
        TOWNROAD = 16,
        CITYROAD = 32,
        ALL = NONE | CITY | TOWN | CITYTOWN | TOWNROAD | CITYROAD
    }

    [TestClass()]
    public class EnumHelperTests
    {
        [TestMethod()]
        public void CheckedContainEnumNameTest()
        {
            Assert.IsTrue(EnumHelper.CheckedContainEnumName<AreaMode>("none"));
        }

        [TestMethod()]
        public void GetDescriptionTest()
        {
            Assert.AreEqual("NONE", AreaMode.NONE.GetDescription());
        }

        [TestMethod()]
        public void ParseEnumDescriptionTest()
        {
            Assert.AreEqual(AreaMode.NONE, EnumHelper.ParseEnumDescription<AreaMode>("NONE", AreaMode.CITYTOWN));
        }

        [TestMethod()]
        public void ParseEnumNameTest()
        {
            Assert.AreEqual(AreaMode.ALL, EnumHelper.ParseEnumName<AreaMode>("ALL"));
        }

        [TestMethod()]
        public void GetNameTest()
        {
            Assert.AreEqual("ALL", AreaMode.ALL.GetName());
            Assert.AreEqual("NONE", EnumHelper.GetName<AreaMode>(0));
            Assert.AreEqual("CITY", EnumHelper.GetName<AreaMode>(1));
            Assert.AreEqual("TOWN", EnumHelper.GetName<AreaMode>(2));
            Assert.AreEqual("ROAD", EnumHelper.GetName<AreaMode>(3));
            Assert.AreEqual("CITYTOWN", EnumHelper.GetName<AreaMode>(4));
        }

        [TestMethod()]
        public void InTest()
        {
            
            Assert.IsTrue(AreaMode.CITY.In(AreaMode.CITYTOWN, AreaMode.CITY));
        }

        [TestMethod()]
        public void NotInTest()
        {
            Assert.IsTrue(AreaMode.CITYTOWN.NotIn(AreaMode.ROAD));
        }

        [TestMethod()]
        public void GetValuesTest()
        {
            int[] _actual = EnumHelper.GetValues<int>(typeof(AreaMode));
            int[] _expected = new int[] { 0, 1, 2, 4, 8, 16, 32, 59 };
            CollectionAssert.AreEqual(_actual, _expected);
        }
    }
}