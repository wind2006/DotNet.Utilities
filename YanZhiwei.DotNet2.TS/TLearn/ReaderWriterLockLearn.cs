using System;
using System.Collections.Generic;
using System.Threading;

namespace YanZhiwei.DotNet2.TS.TLearn
{
    public class ReaderWriterLockLearn
    {
        private static ReaderWriterLockLearn instance = null;

        public static ReaderWriterLockLearn Instance
        {
            get
            {
                if (instance == null)
                    instance = new ReaderWriterLockLearn();
                return instance;
            }
        }

        /*
         * 参考：
         * 先前也知道，Monitor实现的是在读写两种情况的临界区中只可以让一个线程访问，那么如果业务中存在”读取密集型“操作，就
         * 好比数据库一样，读取的操作永远比写入的操作多。针对这种情况，我们使用Monitor的话很吃亏，不过没关系，ReadWriterLock因为实现了"写入串行"，"读取并行"。
         * AcquireWriterLock: 获取写入锁。
         * ReleaseWriterLock：释放写入锁。
         * AcquireReaderLock: 获取读锁。
         * ReleaseReaderLock：释放读锁。
         * UpgradeToWriterLock:将读锁转为写锁。
         * DowngradeFromWriterLock：将写锁还原为读锁。
         *
         */
        private static List<int> list = new List<int>();
        private static ReaderWriterLock rwLock = new System.Threading.ReaderWriterLock();

        public void Demo1()
        {
            System.Threading.Thread _threadInsert = new System.Threading.Thread(() =>
            {
                Timer _timer = new Timer(new TimerCallback(Insert), null, 0, 3000);
            });

            System.Threading.Thread _threadRead = new System.Threading.Thread(() =>
            {
                Timer timer1 = new Timer(new TimerCallback(Read), null, 0, 1000);
                Timer timer2 = new Timer(new TimerCallback(Read), null, 0, 1000);
                Timer timer3 = new Timer(new TimerCallback(Read), null, 0, 1000);
            });
            _threadInsert.Start();
            _threadRead.Start();
        }

        private void Insert(object obj)
        {
            int _number = new Random().Next(0, 100);
            rwLock.AcquireWriterLock(TimeSpan.FromSeconds(30));
            list.Add(_number);
            Console.WriteLine("当前线程：{0},插入数值：{1}.", System.Threading.Thread.CurrentThread.ManagedThreadId, _number);
            rwLock.ReleaseWriterLock();
        }

        private void Read(object obj)
        {
            rwLock.AcquireReaderLock(TimeSpan.FromSeconds(30));
            int _sum = list.Count == 0 ? 0 : list.Count - 1;
            Console.WriteLine("当前线程：{0},读取数值：{1}.", System.Threading.Thread.CurrentThread.ManagedThreadId, list[_sum]);
            rwLock.ReleaseReaderLock();
        }
    }
}