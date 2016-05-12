using System;

namespace YanZhiwei.DotNet2.TS.BaseLean
{
    public class BaseCircle
    {
        /*
         * 父类的构造函数总是在子类之前执行的。既先初始化静态构造函数，后初始化子类构造函数。
         */

        public BaseCircle()
        {
            Console.WriteLine("无参基类构造函数。");
        }

        public BaseCircle(double arg)
        {
            Console.WriteLine("有参基类构造函数。" + arg);
        }

        public BaseCircle(double arg, int arg2)
        {
            Console.WriteLine("有参基类构造函数。" + arg);
        }

        public BaseCircle(double arg, int arg2, int arg3)
        {
            Console.WriteLine("有参基类构造函数。" + arg);
        }
    }

    public class SubCircle : BaseCircle
    {
        public SubCircle()
            : base()
        {
            Console.WriteLine("无参子类构造函数。");
        }

        public SubCircle(double arg)
            : base(2)
        {
            Console.WriteLine("有参参子类构造函数。" + arg);
        }

        public SubCircle(int arg)
            : this(10, 100) //this只是调用本身，但是这样是需要调用一次基类没有参的构造函数，所以会多显示一条
        {
            Console.WriteLine("有参参子类构造函数。" + arg);
        }

        public SubCircle(int i, int j)
        {
            Console.WriteLine("有参参子类构造函数。" + i + " " + j);
        }
    }
}