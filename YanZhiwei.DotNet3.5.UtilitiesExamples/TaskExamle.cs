using System;
using System.Threading;
using YanZhiwei.DotNet2.Utilities.Common;
using YanZhiwei.DotNet3._5.Interfaces;
using YanZhiwei.DotNet3._5.Utilities.Core;
using YanZhiwei.DotNet3._5.Utilities.Core.SchedulerType;

namespace YanZhiwei.DotNet3._5.Utilities.Examples
{
    public class TaskExamle
    {
        public static void Demo1()
        {
            try
            {
                ISchedule _schedule1 = new ScheduleExecutionOnce(DateTime.Now.AddSeconds(10));
                Console.WriteLine("最初计划执行时间：" + _schedule1.ExecutionTime);
                Console.WriteLine("初始化执行时间于现在时间的时间刻度差 ：" + _schedule1.DueTime);
                Console.WriteLine("循环的周期 ：" + _schedule1.Period);

                ISchedule _schedule2 = new CycExecution(new TimeSpan(0, 0, 20));
                Console.WriteLine("最初计划执行时间：" + _schedule2.ExecutionTime);
                Console.WriteLine("初始化执行时间于现在时间的时间刻度差 ：" + _schedule2.DueTime);
                Console.WriteLine("循环的周期 ：" + _schedule2.Period);

                ISchedule _schedule3 = new ImmediateExecution();
                Console.WriteLine("最初计划执行时间：" + _schedule2.ExecutionTime);
                Console.WriteLine("初始化执行时间于现在时间的时间刻度差 ：" + _schedule2.DueTime);
                Console.WriteLine("循环的周期 ：" + _schedule2.Period);
                Job _task1 = new Job((obj) =>
                {
                    Console.WriteLine("任务完成：" + DateTime.Now.FormatDate(1));
                    Console.WriteLine("---------------------------------------");
                },
                _schedule3,
                "YanZhiwei");

                _task1.Start(DateTime.Now.FormatDate(1));

                while (JobScheduler.Count > 0)
                {
                    Thread.Sleep(1000);
                    Job cc = JobScheduler.Find("YanZhiwei");
                    if (cc != null)
                        Console.WriteLine("NextExecuteTime:" + cc.NextExecuteTime);
                }
            }
            finally
            {
                // Console.WriteLine("当前任务数量：" + TaskScheduler.Count);
            }
        }
    }
}