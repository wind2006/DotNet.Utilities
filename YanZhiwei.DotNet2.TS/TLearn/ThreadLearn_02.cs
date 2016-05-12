using System;
using System.Diagnostics;
using System.Threading;

namespace YanZhiwei.DotNet2.TS.TLearn
{
    public class ThreadLearn_02
    {
        private static byte[] values = new byte[500000000];

        //分段统计的大小放该数组，比如分成4等份，[10000,10005,10008,10009]
        private static long[] partialSum;

        //把values数组长度均等分,比如长度1000，分成4粉，那partialSize就是250
        private static int partialSize;

        public static void Demo1()
        {
            //根据CPU的数量确定数组的长度
            partialSum = new long[Environment.ProcessorCount];
            //根据CPU的数量确定数组长度均等分
            partialSize = values.Length / Environment.ProcessorCount;
            GenerateByteArray();
            Console.WriteLine("正在统计字节数");
            Stopwatch watch = new Stopwatch();
            watch.Start();
            long total = 0;
            for (int i = 0; i < values.Length; i++)
            {
                total += values[i];
            }
            watch.Stop();
            Console.WriteLine("统计结果为：" + total);
            Console.WriteLine("计算时间为：" + watch.Elapsed);
            Console.WriteLine();
            watch.Reset();
            watch.Start();
            Thread[] threads = new Thread[Environment.ProcessorCount];
            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                threads[i] = new Thread(SumPartial);
                string _threadName = "线程" + i;
                threads[i].Name = _threadName;
                Console.WriteLine("START:" + _threadName);
                threads[i].Start(i);
                //  threads[i].Join();
            }
            //保证一个线程结束再执行下一个线程
            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                Console.WriteLine("JOIN：" + threads[i].Name);
                threads[i].Join();
            }
            //统计总大小
            long total2 = 0;
            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                total2 += partialSum[i];
            }
            watch.Stop();
            Console.WriteLine("使用分段线程统计的大小：" + total2);
            Console.WriteLine("计算时间为：" + watch.Elapsed);
        }

        /// <summary>
        /// 分段统计字节数组的大小
        /// </summary>
        /// <param name="partialNumber">比如有4个CPU，partialNumber可能的值是0， 1， 2， 3</param>
        private static void SumPartial(object partialNumber)
        {
            long sum = 0;
            int partialNumberAsInt = (int)partialNumber;
            int baseIndex = partialNumberAsInt * partialSize;
            for (int i = baseIndex; i < baseIndex + partialSize; i++)
            {
                sum += values[i];
            }
            Console.WriteLine(Thread.CurrentThread.Name + "计算完毕");
            partialSum[partialNumberAsInt] = sum;
        }

        /// <summary>
        /// 创建字节数组
        /// </summary>
        private static void GenerateByteArray()
        {
            var r = new Random(987);
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = (byte)r.Next(10);
            }
        }
    }
}