using System;
using YanZhiwei.DotNet4.TS.TPLLearn;

namespace YanZhiwei.DotNet4.TS
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                // ConcurrentDictionaryLearn.Instance.Demo1();
                //ParallelLearn.Demo1();
                //ParallelLearn.Demo2();
                //ParallelLearn.Demo3();
                //ParallelLearn.Demo4();
                ParallelLearn.Demo5();
                //ParallelLearn.Demo6();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}