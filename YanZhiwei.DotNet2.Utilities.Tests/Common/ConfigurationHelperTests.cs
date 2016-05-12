using Microsoft.VisualStudio.TestTools.UnitTesting;
using YanZhiwei.DotNet2.Utilities.Enums;
using YanZhiwei.DotNet2.Utilities.Tests.Model;

namespace YanZhiwei.DotNet2.Utilities.Common.Tests
{
    [TestClass()]
    public class ConfigurationHelperTests
    {
        [TestMethod()]
        public void SaveSectionTest()
        {
            Person _person = new Person();
            _person.Name = "YanZhiwei";
            _person.Address = "China";
            _person.Age = 1;
            ConfigurationHelper _config = new ConfigurationHelper(ProgramMode.WebForm);
            _config.SaveSection<Person>(_person, "Test");
            Person _actual = _config.ReadSection<Person>("Test");
            Assert.AreEqual("YanZhiwei", _actual.Name);
        }
    }
}