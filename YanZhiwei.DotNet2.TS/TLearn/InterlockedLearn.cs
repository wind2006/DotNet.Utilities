using System;
using System.Threading;

namespace YanZhiwei.DotNet2.TS.TLearn
{
    public class InterlockedLearn
    {
        /*
         * 参考：
         * ”原子操作”是个亮点，我们知道“原子”是不可再分的，深一点的意思就是说站在程序员的角度来看是不需要手工干预的，也就是所谓的“无锁编程”。
         * 实际应用中有时候我们可能只是对共享变量进行一些简单的操作，比如说“自增，自减，求和,赋值，比较"。
         */
        private static int count = 0;

        public static void Demo1()
        {
            for (int i = 0; i < 20; i++)
            {
                System.Threading.Thread _thread = new System.Threading.Thread(() =>
                {
                    Console.WriteLine("当前数字：{0}", Interlocked.Increment(ref count));
                });
                _thread.Start();
            }
        }
    }
}