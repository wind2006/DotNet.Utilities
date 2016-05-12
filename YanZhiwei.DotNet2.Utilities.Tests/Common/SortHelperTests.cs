using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace YanZhiwei.DotNet2.Utilities.Common.Tests
{
    [TestClass()]
    public class SortHelperTests
    {
        [TestMethod()]
        public void BubbleSortTest()
        {
            int[] _array = new int[5] { 8, 2, 3, 9, 5 };
            _array.BubbleSort();
            CollectionAssert.AreEqual(new int[5] { 2, 3, 5, 8, 9 }, _array);

            Student _student1 = new Student() { Name = "Yan", Age = 88 };
            Student _student2 = new Student() { Name = "Yan", Age = 19 };
            Student _student3 = new Student() { Name = "Yan", Age = 26 };
            Student _student4 = new Student() { Name = "Yan", Age = 14 };
            Student _student5 = new Student() { Name = "Yan", Age = 22 };
            Student[] _studentArray = new Student[5] { _student1, _student2, _student3, _student4, _student5 };
            _studentArray.BubbleSort();

            Student _studentEx1 = new Student() { Name = "Yan", Age = 14 };
            Student _studentEx2 = new Student() { Name = "Yan", Age = 19 };
            Student _studentEx3 = new Student() { Name = "Yan", Age = 22 };
            Student _studentEx4 = new Student() { Name = "Yan", Age = 26 };
            Student _studentEx5 = new Student() { Name = "Yan", Age = 88 };
            Student[] _studentExArray = new Student[5] { _studentEx1, _studentEx2, _studentEx3, _studentEx4, _studentEx5 };
            bool _expected = ArrayHelper.ReferenceTypeValueEqual<Student>(_studentArray, _studentExArray);
            Assert.IsTrue(_expected);
        }
    }

    public class Student : IComparable<Student>
    {
        public int Age { get; set; }

        public string Name { get; set; }

        public int CompareTo(Student other)
        {
            return this.Age.CompareTo(other.Age);
        }
    }
}