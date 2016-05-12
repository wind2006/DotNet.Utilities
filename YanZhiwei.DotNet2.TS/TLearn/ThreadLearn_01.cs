using System;
using System.Threading;

namespace YanZhiwei.DotNet2.TS.TLearn
{
    public class ThreadLearn_01
    {
        public static void Demo1()
        {
            //线程A
            Thread ThreadA = new Thread(delegate()
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("A : " + i);
                    if (i == 9)
                    {
                        break;
                    }
                    Thread.Sleep(1000);
                }
            });
            //线程B
            Thread ThreadB = new Thread(delegate()
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("B : " + i);
                    if (i == 4)
                    {
                        break;
                    }
                    Thread.Sleep(1000);
                }
                ThreadA.Join();//在这里插入线程A
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("C : " + i);
                    if (i == 4)
                    {
                        break;
                    }
                    Thread.Sleep(1000);
                }
            });
            ThreadA.Start();
            ThreadB.Start();
        }
    }
}