namespace YanZhiwei.DotNet3._5.Utilities.Core.SchedulerType
{
    using System;

    using YanZhiwei.DotNet3._5.Interfaces;

    /// <summary>
    /// 周期性的执行计划
    /// </summary>
    public struct CycExecution : ISchedule
    {
        #region Fields

        private TimeSpan period;
        private DateTime schedule;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数,马上开始运行
        /// </summary>
        /// <param name="period">周期时间</param>
        public CycExecution(TimeSpan period)
        {
            this.schedule = DateTime.Now;
            this.period = period;
        }

        /// <summary>
        /// 构造函数，在一个将来时间开始运行
        /// </summary>
        /// <param name="shedule">计划执行的时间</param>
        /// <param name="period">周期时间</param>
        public CycExecution(DateTime shedule, TimeSpan period)
        {
            this.schedule = shedule;
            this.period = period;
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
                long ms = (schedule.Ticks - DateTime.Now.Ticks) / 10000;
                if (ms < 0)
                {
                    ms = 0;
                }
                return ms;
            }
        }

        /// <summary>
        /// 计划执行时间
        /// </summary>
        public DateTime ExecutionTime
        {
            get { return schedule; }
            set { schedule = value; }
        }

        /// <summary>
        /// 周期[毫秒]
        /// </summary>
        public long Period
        {
            get { return period.Ticks / 10000; }
        }

        #endregion Properties
    }
}