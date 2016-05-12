using System;
using System.Threading;

namespace YanZhiwei.DotNet2.TS.TLearn
{
    public class ManualResetEventLearn
    {
        private static ManualResetEventLearn instance = null;

        public static ManualResetEventLearn Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ManualResetEventLearn();
                }
                return instance;
            }
        }

        /*
         * 参考：
         * 1.ManualResetEvent 允许线程通过发信号互相通信。通常，此通信涉及一个线程在其他线程进行之前必须完成的任务。当一个线程开始一个活动（此活动必须完成后，其他线程才能
         * 开始）时，它调用 Reset 以将 ManualResetEvent 置于非终止状态，此线程可被视为控制 ManualResetEvent。调用 ManualResetEvent 上的 WaitOne 的线程将阻止，并等待信号。
         * 当控制线程完成活动时，它调用 Set 以发出等待线程可以继续进行的信号。并释放所有等待线程。一旦它被终止，ManualResetEvent 将保持终止状态（即对 WaitOne 的调用的线程
         * 将立即返回，并不阻塞），直到它被手动重置。可以通过将布尔值传递给构造函数来控制 ManualResetEvent 的初始状态，如果初始状态处于终止状态，为 true；否则为 false。
         * ManualResetEvent 用于线程同步，通知一个或多个线程某事件已经发生。通常用于一个线程执行的任务必须在其他线程的任务执行之前完成。注意：一旦它被终止，它将保持终止状态，直到它被手动重置。
         *
         * 2.线程是程序中的控制流程的封装。
         *
         * 3.Set()和Reset()方法返回一个布尔值，表示是否进行了成功的修改。
         * 为了把状态修改为有信号的（终止状态），必须调用Set()方法。 手动修改信号量为True，也就是恢复线程执行。
         * 为了把状态修改为无信号的（非终止状态），必须调用ReSet()方法。 手动修改信号量为False，暂停线程执行。
         * WaitOne()方法在无信号（非终止状态）状态下，可以使当前线程挂起；注意这里说的是当前线程；直到调用了Set()方法，该线程才被激活。
         * 该方法用于阻塞线程，默认是无限期的阻塞，有时我们并不想这样，而是采取超时阻塞的方法，如果超时就放弃阻塞，这样也就避免了无限期等待的尴尬。
         *
         * 4.ManualResetEvent状态分为两种：终止状态和非终止状态。当某一任务完成时，将ManualResetEvent设置为终止状态，这样其他等待的线程（一个或多个）将开始执行自己的任务。
         * ManualResetEvent signal = new ManualResetEvent(false);就像一个十字路口的信号灯，初始化参数为false时，主线程处于无信号状态，调用signal.WaitOne()，主线程阻塞，等待子线程的通知，知道signal回到有信号状态，主线程再继续往下执行。
         */

        public void Demo1()
        {
            ManualResetEvent _mre = new ManualResetEvent(false);//非终止状态
            System.Threading.Thread _thread = new System.Threading.Thread(() =>
            {
                _mre.WaitOne();//线程将阻止，并等待信号。
                Console.WriteLine("Demo1 Thread Run...");
            });
            _thread.Start();
            Console.WriteLine("Run...");
            System.Threading.Thread.Sleep(3000);
            _mre.Set();//发出等待线程可以继续进行的信号。并释放所有等待线程。
        }

        public void Demo2()
        {
            ManualResetEvent _mre = new ManualResetEvent(false);//非终止状态
            System.Threading.Thread _thread1 = new System.Threading.Thread(() =>
            {
                _mre.WaitOne(1000);//一秒后，自动运行
                for (int i = 1; i <= 5; i++)
                {
                    System.Threading.Thread.Sleep(500);
                    Console.WriteLine("线程一的数值:" + i);
                }
            });
            System.Threading.Thread _thread2 = new System.Threading.Thread(() =>
            {
                _mre.WaitOne();//线程将阻止，并等待信号。
                for (int i = 1; i <= 5; i++)
                {
                    System.Threading.Thread.Sleep(500);
                    Console.WriteLine("线程二的数值:" + i);
                }
            });
            _thread1.Start();
            _thread2.Start();
            for (int i = 1; i <= 5; i++)
            {
                System.Threading.Thread.Sleep(500);
                Console.WriteLine("主线程的数值:" + i);
                if (i == 3)
                    _mre.Set();//发出等待线程可以继续进行的信号。并释放所有等待线程。
            }
        }

        public void Demo3()
        {
            ManualResetEvent _mre = new ManualResetEvent(false);//非终止状态
            Console.WriteLine("当前时间：{0},我是主线程：{1}，请回来....", DateTime.Now, System.Threading.Thread.CurrentThread.GetHashCode());
            System.Threading.Thread _thread = new System.Threading.Thread(() =>
            {
                _mre.WaitOne();
                Console.WriteLine("当前时间：{0},收到收到，我是线程：{1}", DateTime.Now, System.Threading.Thread.CurrentThread.GetHashCode());
            });
            _thread.Start();
            System.Threading.Thread.Sleep(3000);
            _mre.Set();
        }
    }
}