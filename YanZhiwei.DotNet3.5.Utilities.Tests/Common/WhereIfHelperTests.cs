using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using YanZhiwei.DotNet3._5.UtilitiesTests.Model;

namespace YanZhiwei.DotNet3._5.Utilities.Common.Tests
{
    [TestClass()]
    public class WhereIfHelperTests
    {
        private static List<Person> PersonList = null;

        [TestInitialize]
        public void InitData()
        {
            PersonList = new List<Person>()
            {
                 new Person(){ Age=1, Address="上海", Name="yanzhiwei"},
                 new Person(){ Age=2, Address="中国", Name="YANZHIWEI"},
                 new Person(){ Age=3, Address="株洲", Name="yanzhiwei"}
            };
        }

        [TestMethod()]
        public void WhereIfNullOrEmptyTest()
        {
            var _finded = PersonList.WhereIfNullOrEmpty<Person>(string.Empty, c => c.Name.Contains("yanzhiwei"));
            Assert.AreEqual(3, _finded.Count());
        }

        [TestMethod()]
        public void WhereIfTest()
        {
            string _name = string.Empty;
            var _finded = PersonList.WhereIf<Person>(!string.IsNullOrEmpty(_name), c => c.Name.Contains(_name));
            Assert.AreEqual(3, _finded.Count());
            _name = "yanzhiwei";
            _finded = PersonList.WhereIf<Person>(!string.IsNullOrEmpty(_name), c => c.Name.Contains(_name));
            Assert.AreEqual(2, _finded.Count());
        }
    }
}