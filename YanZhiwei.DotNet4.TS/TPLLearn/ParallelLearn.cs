using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace YanZhiwei.DotNet4.TS.TPLLearn
{
    public class ParallelLearn
    {
        /*
         * 参考：
         * 1.Parallel.forEach:到之处就是可以将数据进行分区，每一个小区内实现串行计算，分区采用Partitioner.Create实现。
         *   Break():这个是通知并行计算尽快的退出循环，比如并行计算正在迭代100，那么break后程序还会迭代所有小于100的。
         *   Stop()：这个就不一样了，比如正在迭代100突然遇到stop，那它啥也不管了，直接退出。
         *   在调用 Stop 或 Break 后，循环中的其他线程可能会继续运行一段时间（这不受应用程序开发人员的控制），理解这一点很重要。
         *   可以使用 ParallelLoopState.IsStopped 属性检查是否已在另一个线程上停止该循环。
         *
         * 2.Parallel.ForEach
         * Partitioner.Create(0, 3000000)==>分区的范围是0-3000000。
         * 想知道系统给我们分了几个区? 很遗憾，这是系统内部协调的，无权告诉我们，当然系统也不反对我们自己指定分区个数，
         * 这里可以使用Partitioner.Create的第六个重载，比如这样：Partitioner.Create(0, 3000000, Environment.ProcessorCount)，
         * 因为 Environment.ProcessorCount能够获取到当前的硬件线程数，所以这里也就开了2个区。
         *
         * 3.Parallel.For<T>
         * localInit：每个线程的线程局部变量初始值的设置；
         * body：每次循环执行的方法，其中方法的最后一个参数就是线程局部变量；
         * localFinally：每个线程之后执行的方法。
         */

        public static void Demo1()
        {
            var _watch = Stopwatch.StartNew();
            _watch.Start();
            Demo1Run1();
            Demo1Run2();
            _watch.Stop();
            Console.WriteLine("串行任务花费时间:" + _watch.ElapsedMilliseconds);
            _watch.Reset();
            _watch.Start();
            Parallel.Invoke(Demo1Run1, Demo1Run2);
            _watch.Stop();
            Console.WriteLine("并行任务花费时间:" + _watch.ElapsedMilliseconds);
            Console.ReadLine();
        }

        private static void Demo1Run1()
        {
            Thread.Sleep(3000);
            Console.WriteLine("任务一：花费时间：3S");
        }

        private static void Demo1Run2()
        {
            Thread.Sleep(5000);
            Console.WriteLine("任务一：花费时间：5S");
        }

        public static void Demo2()
        {
            for (int k = 0; k < 10; k++)
            {
                GC.Collect();
                ConcurrentBag<int> _bagList = new ConcurrentBag<int>();
                var _watch = Stopwatch.StartNew();
                _watch.Start();
                for (int i = 0; i < 10000000; i++)
                {
                    _bagList.Add(i);
                }
                _watch.Stop();
                Console.WriteLine("第{0}次串行添加{1}数据，耗时:{2}ms.", (k + 1), _bagList.Count, _watch.ElapsedMilliseconds);
                GC.Collect();
                _bagList = new ConcurrentBag<int>();
                _watch = Stopwatch.StartNew();
                _watch.Start();
                Parallel.For(0, 10000000, i => { _bagList.Add(i); });
                _watch.Stop();
                Console.WriteLine("第{0}次并行添加{1}数据，耗时:{2}ms.\r\n", (k + 1), _bagList.Count, _watch.ElapsedMilliseconds);
            }
        }

        public static void Demo3()
        {
            for (int j = 1; j < 4; j++)
            {
                Console.WriteLine("\n第{0}次比较", j);
                ConcurrentBag<int> _bag = new ConcurrentBag<int>();
                var _watch = Stopwatch.StartNew();
                _watch.Start();
                for (int i = 0; i < 10000000; i++)
                {
                    _bag.Add(i);
                }
                Console.WriteLine("串行计算：集合有:{0},总共耗时：{1}", _bag.Count, _watch.ElapsedMilliseconds);
                GC.Collect();
                _bag = new ConcurrentBag<int>();
                _watch = Stopwatch.StartNew();
                _watch.Start();
                Parallel.ForEach(Partitioner.Create(0, 10000000), i =>
                {
                    int _min = i.Item1, _max = i.Item2;
                    for (int w = _min; w < _max; w++)
                    {
                        _bag.Add(w);
                    }
                });
                Console.WriteLine("并行计算：集合有:{0},总共耗时：{1}", _bag.Count, _watch.ElapsedMilliseconds);
                GC.Collect();
            }
        }

        public static void Demo4()
        {
            Parallel.For(0, 1000, (i, status) =>
            {
                if (i == 8)
                {
                    Console.WriteLine("Parallel For Break....");
                    status.Break();
                }
                Console.WriteLine(string.Format("output :{0}", i));
                Thread.Sleep(100);
            });
        }

        public static void Demo5()
        {
            long _total = 0;
            Parallel.For<long>(0,           // For循环的起点
                10,                 // For循环的终点
               () =>
               {
                   Console.WriteLine("init Value:0，ThreadId：" + Thread.CurrentThread.ManagedThreadId);
                   return 0;
               },                    // 初始化局部变量的方法(long)，既为下面的subtotal的初值
                (i, LoopState, subtotal) => // 为每个迭代调用一次的委托，i是当前索引，LoopState是循环状态，subtotal为局部变量名
                {
                    subtotal += i;    // 修改局部变量
                    Console.WriteLine("body Value:" + subtotal + ",CurValue:" + i + "，ThreadId:" + Thread.CurrentThread.ManagedThreadId);
                    return subtotal;        // 传递参数给下一个迭代
                },
                (finalResult) =>
                {
                    Interlocked.Add(ref _total, finalResult);
                    Console.WriteLine("finally Value:" + finalResult + "，ThreadId:" + Thread.CurrentThread.ManagedThreadId);
                } //对每个线程结果执行的最后操作，这里是将所有的结果相加
               );
            Console.WriteLine(_total);
        }

        public static void Demo6()
        {
            var _bagList = new ConcurrentBag<int>();
            ParallelOptions _options = new ParallelOptions();
            //指定使用的硬件线程数为1
            _options.MaxDegreeOfParallelism = 1;
            Parallel.For(0, 300000, _options, i =>
            {
                _bagList.Add(i);
            });
            Console.WriteLine("并行计算：集合有:{0}", _bagList.Count);
        }
    }
}