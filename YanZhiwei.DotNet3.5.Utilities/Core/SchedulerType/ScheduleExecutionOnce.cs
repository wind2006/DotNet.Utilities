namespace YanZhiwei.DotNet3._5.Utilities.Core.SchedulerType
{
    using System;
    using System.Threading;

    using YanZhiwei.DotNet3._5.Interfaces;

    /// <summary>
    /// 计划在某一未来的时间执行一个操作一次，如果这个时间比现在的时间小，就变成了立即执行的方式
    /// </summary>
    public struct ScheduleExecutionOnce : ISchedule
    {
        #region Fields

        private DateTime schedule;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="time">计划开始执行的时间</param>
        public ScheduleExecutionOnce(DateTime time)
        {
            schedule = time;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        ///  该计划还有多久才能运行[毫秒]
        /// </summary>
        public long DueTime
        {
            get
            {
                long _ms = (schedule.Ticks - DateTime.Now.Ticks) / 10000;
                if (_ms < 0)
                {
                    _ms = 0;
                }
                return _ms;
            }
        }

        /// <summary>
        /// 计划执行时间
        /// </summary>
        public DateTime ExecutionTime
        {
            get { return schedule; }
        }

        /// <summary>
        /// 计划周期[毫秒]
        /// </summary>
        public long Period
        {
            get { return Timeout.Infinite; }
        }

        #endregion Properties
    }
}