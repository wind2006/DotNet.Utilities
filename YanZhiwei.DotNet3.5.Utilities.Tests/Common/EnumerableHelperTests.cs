using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using YanZhiwei.DotNet3._5.UtilitiesTests.Model;

namespace YanZhiwei.DotNet3._5.Utilities.Common.Tests
{
    [TestClass()]
    public class EnumerableHelperTests
    {
        [TestMethod()]
        public void SplitTest()
        {
            List<Person> _persons = new List<Person>();
            for (int i = 0; i < 10; i++)
            {
                _persons.Add(new Person()
                {
                    Age = 10,
                    Name = "YanZhiwei",
                    Birth = DateTime.Now,
                    Address = "Zhuzhou"
                });
            }

            IEnumerable<IEnumerable<Person>> _acutal = _persons.Split(5);
            Assert.AreEqual(2, _acutal.Count());
        }
    }
}