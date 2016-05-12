using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using YanZhiwei.DotNet2.Utilities.Enums;
using YanZhiwei.DotNet2.Utilities.Models;
namespace YanZhiwei.DotNet2.Utilities.Common.Tests
{
    [TestClass()]
    public class AppSettingsHelperTests
    {
        private AppSettingsHelper settingHelper,
                          settingCusHelper;

        [TestInitialize]
        public void Instance()
        {
            settingHelper = new AppSettingsHelper(ProgramMode.WinForm);
            settingCusHelper = new AppSettingsHelper(ProgramMode.WinForm, string.Format(@"{0}\App1.config", Environment.CurrentDirectory));
        }

        [TestMethod()]
        public void AddOrUpdateTest()
        {
            Assert.IsTrue(settingHelper.AddOrUpdate("Name", "YanZhiwei"));
            Assert.IsTrue(settingCusHelper.AddOrUpdate("Name", "YanZhiwei"));
        }

        [TestMethod()]
        public void ExistTest()
        {
            Assert.IsTrue(settingHelper.Exist());
            Assert.IsTrue(settingCusHelper.Exist());
        }

        [TestMethod()]
        public void GetValueTest()
        {
            Assert.IsTrue(settingHelper.AddOrUpdate("Name", "YanZhiwei"));
            Assert.IsTrue(settingCusHelper.AddOrUpdate("Name", "YanZhiwei"));

            Assert.AreEqual("YanZhiwei", settingHelper.GetValue("Name"));
            Assert.AreEqual("YanZhiwei", settingCusHelper.GetValue("Name"));
        }

        [TestMethod()]
        public void RemoveTest()
        {
            Assert.IsTrue(settingHelper.AddOrUpdate("Name", "YanZhiwei"));
            Assert.IsTrue(settingCusHelper.AddOrUpdate("Name", "YanZhiwei"));

            Assert.IsTrue(settingHelper.Remove("Name"));
            Assert.IsTrue(settingCusHelper.Remove("Name"));
        }
    }
}