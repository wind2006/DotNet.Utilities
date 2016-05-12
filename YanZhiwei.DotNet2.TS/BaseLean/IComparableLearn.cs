using System;
using System.Collections.Generic;

namespace YanZhiwei.DotNet2.TS.BaseLean
{
    public class IComparableLearn
    {
        public static void Demo1()
        {
            List<Student> _studentList = new List<Student>();
            _studentList.Add(new Student() { Age = 1, Name = "a1" });
            _studentList.Add(new Student() { Age = 5, Name = "g1" });
            _studentList.Add(new Student() { Age = 4, Name = "b1" });
            _studentList.Add(new Student() { Age = 2, Name = "f1" });

            _studentList.Sort();
            foreach (Student item in _studentList)
            {
                Console.WriteLine(item.Name + "----" + item.Age.ToString());
            }
        }
    }

    internal class Student : IComparable
    {
        public string Name { get; set; }

        public int Age { get; set; }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            Student student = obj as Student;
            if (Age > student.Age)
            {
                return 1;
            }
            else if (Age == student.Age)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        #endregion IComparable Members
    }
}