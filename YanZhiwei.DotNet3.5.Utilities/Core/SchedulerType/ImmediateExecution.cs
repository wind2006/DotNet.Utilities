namespace YanZhiwei.DotNet3._5.Utilities.Core.SchedulerType
{
    using System;
    using System.Threading;

    using YanZhiwei.DotNet3._5.Interfaces;

    /// <summary>
    /// 计划立即执行任务
    /// </summary>
    public struct ImmediateExecution : ISchedule
    {
        #region Properties

        /// <summary>
        /// 该计划还有多久才能运行[毫秒]
        /// </summary>
        public long DueTime
        {
            get { return 0; }
        }

        /// <summary>
        /// 任务执行时间
        /// </summary>
        public DateTime ExecutionTime
        {
            get { return DateTime.Now; }
        }

        /// <summary>
        /// 周期[毫秒]
        /// </summary>
        public long Period
        {
            get { return Timeout.Infinite; }
        }

        #endregion Properties
    }
}