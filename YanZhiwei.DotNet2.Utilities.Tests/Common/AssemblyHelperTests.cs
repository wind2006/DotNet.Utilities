using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using YanZhiwei.DotNet2.Utilities.Common;

namespace YanZhiwei.DotNet2.Utilities.Common.Tests
{
    [TestClass()]
    public class AssemblyHelperTests
    {
        private AssemblyHelper assHelper = null;

        [TestInitialize]
        public void TestInit()
        {
            string _path = string.Format(@"{0}\TestSource\DotNet2.Interfaces.dll", AppDomain.CurrentDomain.BaseDirectory);
            assHelper = new AssemblyHelper(_path);
        }

        [TestMethod()]
        public void GetTitleTest()
        {
            Assert.AreEqual("YanZhiwei.DotNet2.Interfaces", assHelper.GetTitle());
        }

        [TestMethod()]
        public void GetProductNameTest()
        {
            Assert.AreEqual("YanZhiwei.DotNet2.Interfaces", assHelper.GetProductName());
        }

        [TestMethod()]
        public void GetVersionTest()
        {
            Assert.AreEqual("1.0.5483.24630", assHelper.GetVersion());
        }

        [TestMethod()]
        public void GetDescriptionTest()
        {
            Assert.AreEqual("", assHelper.GetDescription());
        }

        [TestMethod()]
        public void GetCopyrightTest()
        {
            Assert.AreEqual("Copyright © YanZhiwei 2015", assHelper.GetCopyright());
        }

        [TestMethod()]
        public void GetCompanyTest()
        {
            Assert.AreEqual("", assHelper.GetCompany());
        }

        [TestMethod()]
        public void GetAppFullNameTest()
        {
            Assert.AreEqual("DotNet2.Interfaces, Version=1.0.5483.24630, Culture=neutral, PublicKeyToken=null", assHelper.GetAppFullName());
        }

        [TestMethod()]
        public void GetBuildDateTimeByVersionTest()
        {
            Assert.AreEqual("2015-01-05 13:41:00", assHelper.GetBuildDateTimeByVersion().FormatDate(1));
        }

        [TestMethod()]
        public void GetBuildDateTimeTest()
        {
            Assert.AreEqual("2015-01-05 13:41:00", assHelper.GetBuildDateTime().FormatDate(1));
        }
    }
}