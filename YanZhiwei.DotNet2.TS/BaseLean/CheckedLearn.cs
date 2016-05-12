using System;

namespace YanZhiwei.DotNet2.TS.BaseLean
{
    public class CheckedLearn
    {
        public static void Demo1()
        {
            try
            {
                int _numberA = int.MaxValue;
                int _numberB = int.MaxValue;
                int _result = checked(_numberA + _numberB);
                //_numberA + _numberB==-2，并没有抛出异常
                //?=-2:在运行时默认情况程序是不会检查算术运算是否溢出的，cpu只管算，对于它来讲按规则算就是了，结果对不对不是他的错。
                //checked关键字判断是否溢出；
                Console.WriteLine(_result.ToString());
            }
            catch (OverflowException ex)
            {
                Console.WriteLine(ex.Message.Trim());
            }
        }

        public static void Demo2()
        {
            //int _result = int.MaxValue * 2;
            //Console.WriteLine(_result);
        }
    }
}