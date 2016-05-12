using System;
using System.Threading;

namespace YanZhiwei.DotNet2.TS.TLearn
{
    public class MonitorLearn
    {
        private static MonitorLearn instance = null;

        public static MonitorLearn Instance
        {
            get
            {
                if (instance == null)
                    instance = new MonitorLearn();
                return instance;
            }
        }

        /*
         *参考：
         * 1.Monitor类：这个算是实现锁机制的纯正类，在锁定的临界区中只允许让一个线程访问，其他线程排队等待。
         * 如果我们要精细的控制，则必须使用原生类，这里要注意一个问题就是“锁住什么”的问题，一般情况下我们锁住的都是静态对象，我们知道静态对象
         * 属于类级别，当有很多线程共同访问的时候，那个静态对象对多个线程来说是一个，不像实例字段会被认为是多个。
         *
         * 2.Monitor.Wait和Monitor.Pulse
         * 首先这两个方法是成对出现，通常使用在Enter，Exit之间。
         * Wait： 暂时的释放资源锁，然后该线程进入"等待队列"中，那么自然别的线程就能获取到资源锁。
         * Pulse:  唤醒"等待队列"中的线程，那么当时被Wait的线程就重新获取到了锁。
         * 可能A线程进入到临界区后，需要B线程做一些初始化操作，然后A线程继续干剩下的事情。
         * 用上面的两个方法，我们可以实现线程间的彼此通信。
         *
         */

        private static readonly object SyncObject = new object();
        private static int count = 0;

        public static void Demo1()
        {
            for (int i = 0; i < 10; i++)
            {
                System.Threading.Thread _thread = new System.Threading.Thread(() =>
                {
                    System.Threading.Thread.Sleep(10);
                    Monitor.Enter(SyncObject);//在锁定的临界区中只允许让一个线程访问，其他线程排队等待。
                    Console.WriteLine(string.Format("当前数值：{0}", ++count));
                    Monitor.Exit(SyncObject);
                });
                _thread.Start();
            }
        }

        private static readonly object SyncObject2 = new object();

        public static void Demo2()
        {
            CookA _cook = new CookA(SyncObject2);
            Waiter _waiter = new Waiter(SyncObject2);
            System.Threading.Thread _thread1 = new System.Threading.Thread(new ThreadStart(_waiter.DoWork));
            System.Threading.Thread _thread2 = new System.Threading.Thread(new ThreadStart(_cook.DoWork));
            _thread1.Name = "服务员";
            _thread2.Name = "厨师";
            _thread1.Start();
            _thread2.Start();
        }
    }

    public class Person
    {
        protected object syncObject = null;

        public Person(object sync)
        {
            this.syncObject = sync;
        }

        public virtual void DoWork()
        {
        }
    }

    public class CookA : Person
    {
        public CookA(object sync)
            : base(sync)
        {
        }

        public override void DoWork()
        {
            Monitor.Enter(base.syncObject);
            Console.WriteLine("【{0}】:开始准备材料.....", System.Threading.Thread.CurrentThread.Name);
            Console.WriteLine("【{0}】:正在炒宫保鸡丁.....", System.Threading.Thread.CurrentThread.Name);
            Console.WriteLine("【{0}】:好啦，宫保鸡丁炒好了，可以端给客人.....", System.Threading.Thread.CurrentThread.Name);
            Monitor.Pulse(base.syncObject);
            Monitor.Wait(base.syncObject);
            Console.WriteLine("【{0}】:准备下个菜.....", System.Threading.Thread.CurrentThread.Name);
            Monitor.Exit(base.syncObject);
        }
    }

    public class Waiter : Person
    {
        public Waiter(object sync)
            : base(sync)
        {
        }

        public override void DoWork()
        {
            Monitor.Enter(base.syncObject);
            Console.WriteLine("【{0}】:客人点了一份宫保鸡丁.....", System.Threading.Thread.CurrentThread.Name);
            Console.WriteLine("【{0}】:厨师赶快准备.....", System.Threading.Thread.CurrentThread.Name);
            Monitor.Wait(base.syncObject);
            Console.WriteLine("【{0}】:好嘞，我来端菜.....", System.Threading.Thread.CurrentThread.Name);
            Monitor.Pulse(base.syncObject);
            Monitor.Exit(base.syncObject);
        }
    }
}