namespace YanZhiwei.DotNet2.Utilities.Core
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using YanZhiwei.DotNet2.Utilities.Models;

    /// <summary>
    /// 程序性能测试类
    /// </summary>
    public class PerformanceTest
    {
        #region Fields

        /// <summary>
        /// 开始时间
        /// </summary>
        private DateTime beginTime;

        /// <summary>
        /// 结束时间
        /// </summary>
        private DateTime endTime;

        /// <summary>
        /// 测试参数
        /// </summary>
        private PerformanceParams performance;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public PerformanceTest()
        {
            performance = new PerformanceParams()
            {
                RunCount = 1
            };
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// 执行函数
        /// </summary>
        /// <param name="testFactory">测试委托</param>
        /// <param name="resultFactory">结果委托</param>
        public void Execute(Action<int> testFactory, Action<string> resultFactory)
        {
            /*
            PerformanceTest p = new PerformanceTest();
            p.SetCount(10);//循环次数(默认:1)
            p.SetIsMultithread(true);//是否启动多线程测试 (默认:false)
            p.Execute(
            i =>{
            //需要测试的代码
            Response.Write(i+"<br>");
            System.Threading.Thread.Sleep(1000);
            },
            message =>
            {
            //输出总共运行时间
            Response.Write(message);   //总共执行时间：1.02206秒
            }
            );
            */

            List<Thread> _testThreads = new List<Thread>();
            beginTime = DateTime.Now;
            for (int i = 0; i < performance.RunCount; i++)
            {
                if (performance.IsMultithread)
                {
                    Thread _thread = new Thread(new ThreadStart(() =>
                    {
                        testFactory(i);
                    }));
                    _thread.Start();
                    _testThreads.Add(_thread);
                }
                else
                {
                    testFactory(i);
                }
            }
            if (performance.IsMultithread)
            {
                foreach (Thread t in _testThreads)
                {
                    while (t.IsAlive)
                    {
                        Thread.Sleep(10);
                    }
                }
            }
            resultFactory(GetResult());
        }

        /// <summary>
        ///设置执行次数(默认:1)
        /// </summary>
        public void SetCount(int count)
        {
            performance.RunCount = count;
        }

        /// <summary>
        /// 设置线程模式(默认:false)
        /// </summary>
        /// <param name="isMul">true为多线程</param>
        public void SetIsMultithread(bool isMul)
        {
            performance.IsMultithread = isMul;
        }

        /// <summary>
        /// 获取测试结果
        /// </summary>
        /// <returns>测试结果</returns>
        private string GetResult()
        {
            endTime = DateTime.Now;
            string totalTime = ((endTime - beginTime).TotalMilliseconds / 1000.0).ToString("n5");
            string reval = string.Format("总共执行时间：{0}秒", totalTime);
            Console.Write(reval);
            return reval;
        }

        #endregion Methods
    }
}