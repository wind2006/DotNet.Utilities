using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using YanZhiwei.DotNet3._5.UtilitiesTests.Model;

namespace YanZhiwei.DotNet3._5.Utilities.Common.Tests
{
    [TestClass()]
    public class DistinctHelperTests
    {
        [TestMethod()]
        public void DistinctTest()
        {
            List<Person> _personList = new List<Person>()
            {
                 new Person(){ Age=1, Address="上海", Name="yanzhiwei"},
                 new Person(){ Age=2, Address="中国", Name="YANZHIWEI"},
                 new Person(){ Age=3, Address="株洲", Name="yanzhiwei"}
            };
            Person[] _finded = _personList.Distinct<Person, string>(p => p.Name, StringComparer.InvariantCultureIgnoreCase).ToArray();
            Assert.AreEqual(1, _finded.Count());
            _finded = _personList.Distinct<Person, string>(p => p.Name).ToArray();
            Assert.AreEqual(2, _finded.Count());
        }
    }
}