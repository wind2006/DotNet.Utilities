using System;

namespace YanZhiwei.DotNet2.TS.BaseLean
{
    public class ParameterPassLearn
    {
        public void Demo1(int value)
        {
            /*
             *值类型参数被方法调用的时候，是对本身实例的拷贝和操作；
             */
            Console.WriteLine("传参之前的数值：" + value);
            value += 1;
        }

        public void Demo2(ref int Value)
        {
            /*
             *值类型，引用类型；在加入REF后就意味对参数地址进行操作；
             *即托管堆上实际地址的改变；
             */
            Value = 100;
        }
    }
}