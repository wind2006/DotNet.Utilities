using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using YanZhiwei.DotNet2.Utilities.Tests.Model;

namespace YanZhiwei.DotNet3._5.Utilities.Common.Tests
{
    [TestClass()]
    public class EnumerableHelperTests
    {
        private static List<Person> PersonList = null;

        [TestInitialize]
        public void InitPersonList()
        {
            PersonList = new List<Person>();
            for (int i = 0; i < 10; i++)
            {
                PersonList.Add(new Person() { Name = "yanzhiwei", Age = 1, Address = "株洲" });
            }
        }

        [TestMethod()]
        public void IsEmptyTest()
        {
            Assert.IsFalse(PersonList.IsEmpty());
            PersonList = null;
            Assert.IsTrue(PersonList.IsEmpty());
            PersonList = new List<Person>();
            Assert.IsTrue(PersonList.IsEmpty());
        }
    }
}