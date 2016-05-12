using System;

namespace YanZhiwei.DotNet2.TS.BaseLean
{
    public struct SizeStruct
    {
        public int Width;
        public int Height;

        public int GetSize()
        {
            //return 0;
            return Height * Width;
        }
    }

    public struct SizeStruct2
    {
        //public int Width = 3;结构中不能有实例字段初始值设定项
        //--------------------------------------------
        //public int Height;
        //public SizeStruct2()
        //{
        //    Height = 10;
        //}
        //结构中不能包含显式无参数构造函数
    }

    public class SizeClass
    {
        public int Width;
        public int Height;
    }

    public class StrutLearn
    {
        /*
         *而作为struct类型，无论是赋值，作为参数类型传递，还是返回struct类型实例，是完全拷贝，会占用栈上的空间。根据Microsoft's Value Type Recommendations，在如下情况下，推荐使用struct：
         *1.小于16个字节
         *2.偏向于值，是简单数据，而不是偏向于"面向对象"
         *3.希望值不可变
         */

        public static void Demo1()
        {
            Console.WriteLine("----Demo1------------");
            Console.WriteLine("------Struct---------");
            SizeStruct _size = new SizeStruct() { Width = 1, Height = 1 };
            Console.WriteLine("复制后的：" + _size.Width + "-" + _size.Width);
            SizeStruct _size2 = _size;
            _size2.Width = 2;
            _size2.Height = 2;
            Console.WriteLine("复制后的：" + _size.Width + "-" + _size.Width);

            Console.WriteLine("------Class---------");
            SizeClass _size3 = new SizeClass() { Width = 1, Height = 1 };
            Console.WriteLine("复制后的：" + _size3.Width + "-" + _size3.Width);
            SizeClass _size4 = _size3;
            _size4.Width = 2;
            _size4.Height = 2;
            Console.WriteLine("复制后的：" + _size3.Width + "-" + _size3.Width);
        }

        public static void Demo2()
        {
            Console.WriteLine("----Demo2------------");
            SizeStruct _size = new SizeStruct();
            Console.WriteLine("结构包含隐式的无参构造函数:" + _size.Width + "--" + _size.Height);
            Console.ReadKey();
        }

        public static void Demo3()
        {
            //Console.WriteLine("----Demo2------------");
            //SizeStruct size;
            //size.Width = 5;
            //size.Height = 2;//如果注释掉，将报错
            //Console.WriteLine(size.GetSize());

            /*
             * 如果想调用结构实例的任何方法，需给结构所有字段赋值；
             */
        }

        public static void Demo4()
        {
            SizeStruct _size = new SizeStruct();
            Console.WriteLine("通过'SizeStruct _size = new SizeStruct()'方式创建示例，默认隐式构造函数已经给所有参数赋值；" + _size.GetSize());
        }
    }
}