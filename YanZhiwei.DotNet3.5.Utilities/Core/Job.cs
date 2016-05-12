namespace YanZhiwei.DotNet3._5.Utilities.Core
{
    using System;
    using System.Threading;

    using YanZhiwei.DotNet3._5.Interfaces;
    using YanZhiwei.DotNet3._5.Utilities.Core.SchedulerType;

    /// <summary>
    /// 任务帮助类
    /// </summary>
    public class Job : IDisposable
    {
        #region Fields

        /*
         * 参考：
         * 1. http://www.cnblogs.com/Googler/archive/2010/06/05/1752213.html
         */
        /// <summary>
        /// TimerCallback 对象
        /// </summary>
        private TimerCallback execTask;

        /// <summary>
        /// 上次执行时间
        /// </summary>
        private DateTime lastExecuteTime;

        /// <summary>
        /// 下次执行时间
        /// </summary>
        private DateTime nextExecuteTime;

        /// <summary>
        /// ISchedule 对象
        /// </summary>
        private ISchedule schedule;

        /// <summary>
        /// 任务名称
        /// </summary>
        private string taskName;

        /// <summary>
        /// Timer 对象
        /// </summary>
        private Timer timer;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="callback">一个TimerCallback 委托，表示要执行的方法.</param>
        /// <param name="schedule">为每个任务制定一个执行计划</param>
        /// <param name="taskName">任务名称</param>
        public Job(TimerCallback callback, ISchedule schedule, string taskName)
        {
            this.execTask = callback;
            this.schedule = schedule;
            this.taskName = taskName;
            execTask += new TimerCallback(Execute);
            JobScheduler.Register(this);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 该任务最后一次执行的时间
        /// </summary>
        public DateTime LastExecuteTime
        {
            get { return lastExecuteTime; }
        }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name
        {
            get
            {
                return taskName;
            }
        }

        /// <summary>
        /// 任务下执行时间
        /// </summary>
        public DateTime NextExecuteTime
        {
            get { return nextExecuteTime; }
        }

        /// <summary>
        /// 执行任务的计划
        /// </summary>
        public ISchedule Shedule
        {
            get { return schedule; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 销毁任务
        /// </summary>
        public void Close()
        {
            ((IDisposable)this).Dispose();
        }

        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务。
        /// </summary>
        public void Dispose()
        {
            if (execTask != null)
            {
                taskName = null;
                if (timer != null)
                {
                    timer.Dispose();
                    timer = null;
                }
                execTask = null;
                JobScheduler.DeRegister(this);
            }
        }

        /// <summary>
        /// 刷新任务计划
        /// </summary>
        public void RefreshSchedule()
        {
            if (timer != null)
            {
                timer.Change(schedule.DueTime, schedule.Period);
            }
        }

        /// <summary>
        /// 开始任务
        /// </summary>
        public void Start()
        {
            Start(null);
        }

        /// <summary>
        /// 开始任务
        /// </summary>
        /// <param name="execTaskState">回调参数</param>
        public void Start(object execTaskState)
        {
            if (timer == null)
            {
                timer = new Timer(execTask, execTaskState, schedule.DueTime, schedule.Period);
            }
            else
            {
                timer.Change(schedule.DueTime, schedule.Period);
            }
        }

        /// <summary>
        /// 停止任务
        /// </summary>
        public void Stop()
        {
            if (timer != null)
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite);
            }
        }

        /// <summary>
        /// ToString--返回任务名称
        /// </summary>
        /// <returns>任务名称</returns>
        public override string ToString()
        {
            return taskName;
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="state">任务函数参数</param>
        private void Execute(object state)
        {
            lastExecuteTime = DateTime.Now;
            if (schedule.Period == Timeout.Infinite)
            {
                nextExecuteTime = DateTime.MaxValue;
            }
            else
            {
                TimeSpan _period = new TimeSpan(schedule.Period * 10000);
                nextExecuteTime = lastExecuteTime + _period;
            }
            if (!(schedule is CycExecution))
            {
                this.Close();
            }
        }

        #endregion Methods
    }
}