using System;
using System.Threading;

namespace YanZhiwei.DotNet2.TS.TLearn
{
    public class MutexLearn
    {
        private static MutexLearn instance = null;

        public static MutexLearn Instance
        {
            get
            {
                if (instance == null)
                    instance = new MutexLearn();
                return MutexLearn.instance;
            }
        }

        /*
         * 参考：
         * Metux中提供了WatiOne和ReleaseMutex来确保只有一个线程来访问共享资源。
         *
         * mutex 与监视器类似；它防止多个线程在某一时间同时执行某个代码块。  事实上，名称“mutex”是术语“互相排斥 (mutually exclusive)”的简写形式。
         * 然而与监视器不同的是，mutex 可以用来使跨进程的线程同步。 mutex 由 Mutex 类表示。
         *
         * 当用于进程间同步时，mutex 称为“命名 mutex”，因为它将用于另一个应用程序，因此它不能通过全局变量或静态变量共享。
         * 必须给它指定一个名称，才能使两个应用程序访问同一个 mutex 对象。
         *
         * 1.Mutex是一个令牌，当一个线程拿到这个令牌时运行，另外想拿到令牌的线程就必须等待，直到拿到令牌的线程释放令牌。没有所有权的线程是无法释放令牌的。
         * 2.Mutex（false，”string”）中的string是令牌的关键，或者可以叫令牌名，因为Mutex是跨进程的，整个系统中只会有唯一的令牌存在所以，也就是说你在一个应用程序中的一个线程中得到了Mutex的所有权，那在另外一个线程中的另外的线程想得到他就必须要等待。
         */
        private static int count = 0;
        private static Mutex mutex = new Mutex();

        public static void Demo1()
        {
            for (int i = 0; i < 20; i++)
            {
                System.Threading.Thread _thread = new System.Threading.Thread(Run);
                _thread.Start();
            }
            Console.ReadLine();
        }

        private static Mutex mutex2 = new Mutex(false, "YanZhiwei");

        public static void Demo2()
        {
            System.Threading.Thread _thread = new System.Threading.Thread(() =>
            {
                mutex2.WaitOne();
                Console.WriteLine("当前时间：{0}我是线程:{1}，我已经进去临界区", DateTime.Now, System.Threading.Thread.CurrentThread.GetHashCode());
                System.Threading.Thread.Sleep(10000);
                Console.WriteLine("\n当前时间:{0}我是线程:{1}，我准备退出临界区", DateTime.Now, System.Threading.Thread.CurrentThread.GetHashCode());
                mutex2.ReleaseMutex();
            });
            _thread.Start();
        }

        private static void Run()
        {
            System.Threading.Thread.Sleep(100);
            mutex.WaitOne();
            Console.WriteLine("当前数字：{0}", ++count);
            mutex.ReleaseMutex();
        }
    }
}