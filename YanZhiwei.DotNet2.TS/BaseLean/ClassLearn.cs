using System;
using System.Collections.Generic;

namespace YanZhiwei.DotNet2.TS.BaseLean
{
    public class SizeClass2
    {
        public int Length { get; set; }

        public int Width { get; set; }

        public SizeClass2(int length, int width)
        {
            Length = length;
            Width = width;
        }
    }

    public class ClassLearn
    {
        public static void Demo1()
        {
            /*
             * →栈上的temp指向托管堆上的一个集合实例
             * →当temp放到ChangeReferenceType(temp)方法中，本质是把temp指向的地址赋值给了变量list
             * →在ChangeReferenceType(List<string> list)方法内部，又把变量list的指向了另外一个集合实例地址
             * →但temp的指向地址一直没有改变
             */
            Console.WriteLine("--------Demo1-------------");
            List<string> temp = new List<string>() { "my", "god" };
            ChangeReferenceType(temp);
            temp.ForEach(t => Console.WriteLine(t + " "));
        }

        public static void Demo2()
        {
            /*
             * →栈上的temp指向托管堆上的一个集合实例
             * →当temp放到ChangeReferenceType(temp)方法中，本质是把temp指向的地址赋值给了变量list
             * →在ChangeReferenceType(List<string> list)方法内部，把temp和list共同指向的实例清空，又添加"hello"和"world"2个元素
             * →由于list和temp指向的实例是一样的，所以改变list指向的实例就等同于改变temp指向的实例
             * 以上，很好地说明了：引用类型参数传递的是地址。
             */
            Console.WriteLine("--------Demo2-------------");
            List<string> temp = new List<string>() { "my", "god" };
            ChangeReferenceType2(temp);
            temp.ForEach(t => Console.WriteLine(t + " "));
        }

        public static void Demo3()
        {
            Console.WriteLine("--------Demo3-------------");
            // SizeClass2 _sizeClass2 = new SizeClass2();
            Console.WriteLine("类不包含隐式无参构造函数");
        }

        public static void ChangeReferenceType(List<string> list)
        {
            list = new List<string>() { "hello", "world" };
        }

        public static void ChangeReferenceType2(List<string> list)
        {
            list.Clear();
            list.Add("hello");
            list.Add("world");
        }
    }
}