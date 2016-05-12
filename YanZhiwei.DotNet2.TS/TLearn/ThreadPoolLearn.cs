using System;
using System.Threading;

namespace YanZhiwei.DotNet2.TS.TLearn
{
    public class ThreadPoolLearn
    {
        /*
         * 参考：
         * 1.线程很多的话，线程调度就越频繁，可能就会出现某个任务执行的时间比线程调度花费的时间短很多的尴尬局面。
         * 2.一个线程默认占用1M的堆栈空间，如果10230个线程将会占用差不多10G的内存空间。
         *
         * 3.缺点
         * 不能控制线程的优先级；
         * 不能将要执行的任务取消；
         *
         * 4.RegisterWaitForSingleObject
         * 提供了一些简单的线程间交互，因为该方法的第一个参数是WaitHandle，在VS对象浏览器中，我们发现EventWaitHandle继承了WaitHandle，
         * 而ManualResetEvent和AutoResetEvent都继承于EventWaitHandle，也就是说我们可以在RegisterWaitForSingleObject溶于信号量的概念。
         *
         * 5.那么当线程很多的时候并不是一件好事情，这会导致过度的使用系统资源而耗尽内存，那么自然就会引入“线程池”。
         *   线程池：是一个在后台执行多个任务的集合，他封装了我们对线程的基本操作，我们能做的就只要把“入口方法”丢给线程池就行了。
         *   特点：线程池有最大线程数限制，大小在不同的机器上是否区别的，当池中的线程都是繁忙状态，后入的方法就会排队，直至池中有空闲的线程来处理。
         *
         * 6.当创建一个线程，会用几十到几百毫秒的时间创建该线程的栈，而且，在默认情况下，每个线程需要1M的栈空间。『线程池做到了共享和重复使用线程，性能得以提高』。线程池还可以设置允许的最多线程数，一旦达到这个极限值，多余的线程需要排队，等待线程池中的线程执行结束再进入。
         */

        public static void Demo1()
        {
            int workerThreads, completePortsThreads;
            ThreadPool.GetMaxThreads(out workerThreads, out completePortsThreads);
            Console.WriteLine("线程池中最大的线程数{0},线程池中异步IO线程的最大数目{1}", workerThreads, completePortsThreads);
            ThreadPool.GetMinThreads(out workerThreads, out completePortsThreads);
            Console.WriteLine("线程池中最小的线程数{0},线程池中异步IO线程的最小数目{1}", workerThreads, completePortsThreads);
        }

        public static void Demo2()
        {
            ThreadPool.SetMaxThreads(100, 50);
            ThreadPool.SetMinThreads(20, 10);
            Demo1();
        }

        public static void Demo3()
        {
            AutoResetEvent _are = new AutoResetEvent(false);
            ThreadPool.RegisterWaitForSingleObject(_are, (arg, sign) =>
            {
                Console.WriteLine("当前时间:{0}  我是线程{1}", DateTime.Now, System.Threading.Thread.CurrentThread.ManagedThreadId);
            }, null, Timeout.Infinite, false);
            Console.WriteLine("时间:{0} 工作线程请注意，您需要等待5s才能执行。", DateTime.Now);
            System.Threading.Thread.Sleep(5000);
            _are.Set();
            Console.WriteLine("时间:{0} 工作线程已执行。", DateTime.Now);
            Console.Read();
        }

        public static void Demo4()
        {
            AutoResetEvent _are = new AutoResetEvent(false);
            ThreadPool.RegisterWaitForSingleObject(_are, (arg, sign) =>
            {
                Console.WriteLine("当前时间:{0}  我是线程{1}", DateTime.Now, System.Threading.Thread.CurrentThread.ManagedThreadId);
            }, null, 1000, false);//参数1000：其实就是WaitOne(1000)，采取超时机制
            Console.Read();
        }

        public static void Demo5()
        {
            AutoResetEvent _are = new AutoResetEvent(false);
            RegisteredWaitHandle handle = ThreadPool.RegisterWaitForSingleObject(_are, (arg, sign) =>
               {
                   Console.WriteLine("当前时间:{0}  我是线程{1}", DateTime.Now, System.Threading.Thread.CurrentThread.ManagedThreadId);
               }, null, 1000, false);//参数1000：其实就是WaitOne(1000)，采取超时机制
            Console.WriteLine("3S后停止");
            System.Threading.Thread.Sleep(3000);
            handle.Unregister(_are);
            Console.Read();
        }
    }
}