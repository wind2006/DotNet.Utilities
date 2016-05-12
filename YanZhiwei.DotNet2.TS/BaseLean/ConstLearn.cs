using System;

namespace YanZhiwei.DotNet2.TS.BaseLean
{
    public class ConstLearn
    {
        public const int Age = 18;//必须初始化

        // public static const int Age_Man = 10;不能与static一起使用，因为const默认就是static；
        public ConstLearn(int _age)
        {
            //Age = _age;不能运行时赋值，const变量是编译期变量；
        }

        // public const Person Student=new Person();引用类型变量只能赋值为NULL
        public const Person Student = null;

        //  public const DateTime Now = new DateTime(2014, 10, 10);const只能用于数字和字符串
        public void ShowAge_Value()
        {
            Console.WriteLine(Age.ToString());
        }
    }

    public class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }
    }
}