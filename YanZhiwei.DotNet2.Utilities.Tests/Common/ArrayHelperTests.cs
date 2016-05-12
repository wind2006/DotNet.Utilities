using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using YanZhiwei.DotNet2.Utilities.Common;
using YanZhiwei.DotNet2.Utilities.Tests.Model;

namespace YanZhiwei.DotNet2.Utilities.Common.Tests
{
    [TestClass()]
    public class ArrayHelperTests
    {
        [TestMethod()]
        public void AddRangeTest()
        {
            CollectionAssert.AreEqual(new int[7] { 1, 2, 3, 4, 5, 6, 7 }, ArrayHelper.AddRange(new int[5] { 1, 2, 3, 4, 5 }, new int[2] { 6, 7 }));
        }

        [TestMethod()]
        public void AddTest()
        {
            CollectionAssert.AreEqual(new int[6] { 1, 2, 3, 4, 5, 6 }, ArrayHelper.Add(new int[5] { 1, 2, 3, 4, 5 }, 6));
        }

        [TestMethod()]
        public void ClearAllTest()
        {
            int[] _test = new int[5] { 1, 2, 3, 4, 5 };
            _test.ClearAll();
            CollectionAssert.AreEqual(new int[5] { 0, 0, 0, 0, 0 }, _test);
        }

        [TestMethod()]
        public void CopyTest()
        {
            CollectionAssert.AreEqual(new int[3] { 1, 2, 3 }, ArrayHelper.Copy(new int[5] { 1, 2, 3, 4, 5 }, 0, 3));
        }

        [TestMethod()]
        public void DynamicAddTest()
        {
            CollectionAssert.AreEqual(new int[6] { 1, 2, 3, 4, 5, 6 }, ArrayHelper.DynamicAdd(new int[5] { 1, 2, 3, 4, 5 }, 6));
        }

        [TestMethod()]
        public void EqualsTest()
        {
            Assert.IsTrue(ArrayHelper.EqualValue(new int[5] { 1, 2, 3, 4, 5 }, new int[5] { 1, 2, 3, 4, 5 }));
        }

        [TestMethod()]
        public void IsNullOrEmptyTest()
        {
            Assert.IsTrue(ArrayHelper.IsNullOrEmpty(new int[0]));
        }

        [TestMethod()]
        public void ResizeTest()
        {
            CollectionAssert.AreEqual(new int[5] { 1, 2, 3, 0, 0 }, ArrayHelper.Resize<int>(new int[3] { 1, 2, 3 }, 5));
        }

        [TestMethod()]
        public void ValueEqualTest()
        {
            Person[] _personAList = new Person[10];
            Person[] _personBList = new Person[10];
            for (int i = 0; i < 10; i++)
            {
                Person _tmp = new Person();
                _tmp.Age = 1;
                _tmp.Name = string.Format("YanZhiwei{0}", i);
                _tmp.Address = "shanghai";

                _personAList[i] = _tmp;
                _personBList[i] = _tmp;
            }
            Assert.IsTrue(ArrayHelper.ReferenceTypeValueEqual<Person>(_personAList, _personBList));
        }

        [TestMethod()]
        public void WithinIndexTest()
        {
            int[] _test = new int[5] { 1, 2, 3, 4, 5 };
            Assert.IsFalse(_test.WithinIndex(5));
            Assert.IsTrue(_test.WithinIndex(0));
            Assert.IsTrue(_test.WithinIndex(4));
        }
    }
}