using System;
using System.Threading;
using System.Threading.Tasks;

namespace YanZhiwei.DotNet4.TS.TPLLearn
{
    public class TaskLearn
    {
        /*
         *参考：
         *1.任务是架构在线程之上的，也就是说任务最终还是要抛给线程去执行。
         *任务跟线程不是一对一的关系，比如开10个任务并不是说会开10个线程，这一点任务有点类似线程池，但是任务相比线程池有很小的开销和精确的控制。
         *
         *
         */

        public static void Demo1()
        {
            //第一种方式开启
            var task1 = new Task(() =>
            {
                Run1();
            });

            //第二种方式开启
            var task2 = Task.Factory.StartNew(() =>
            {
                Run2();
            });

            Console.WriteLine("调用start之前****************************\n");

            //调用start之前的“任务状态”
            Console.WriteLine("task1的状态:{0}", task1.Status);

            Console.WriteLine("task2的状态:{0}", task2.Status);

            task1.Start();

            Console.WriteLine("\n调用start之后****************************");

            //调用start之前的“任务状态”
            Console.WriteLine("\ntask1的状态:{0}", task1.Status);

            Console.WriteLine("task2的状态:{0}", task2.Status);

            //主线程等待任务执行完
            Task.WaitAll(task1, task2);

            Console.WriteLine("\n任务执行完后的状态****************************");

            //调用start之前的“任务状态”
            Console.WriteLine("\ntask1的状态:{0}", task1.Status);

            Console.WriteLine("task2的状态:{0}", task2.Status);

            Console.Read();
        }

        private static void Run1()
        {
            Thread.Sleep(1000);
            Console.WriteLine("\n我是任务1");
        }

        private static void Run2()
        {
            Thread.Sleep(2000);
            Console.WriteLine("我是任务2");
        }
    }
}