namespace YanZhiwei.DotNet4.Utilities.WinForm
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using YanZhiwei.DotNet4.Utilities.Models;

    /// <summary>
    /// BackgroundWorker 帮助类
    /// </summary>
    public class BWHelper
    {
        #region Fields

        private ValueMonitor<int> percentageProgress = new ValueMonitor<int>(0);
        private DateTime startTime;
        private ValueMonitor<TimeSpan> timeLeft = new ValueMonitor<TimeSpan>(TimeSpan.MaxValue);
        private IEnumerable<Action> toDo;
        private BackgroundWorker worker;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="aWorker">BackgroundWorker</param>
        public BWHelper(BackgroundWorker aWorker)
        {
            worker = aWorker;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            percentageProgress.ValueChanged += new ValueChangedDelegate<int>(percentageProgress_ValueChanged);

            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 设置是否是并行
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is parallel; otherwise, <c>false</c>.
        /// </value>
        public bool IsParallel
        {
            get; set;
        }

        /// <summary>
        /// 获取剩余时间
        /// </summary>
        /// <value>
        /// The time left.
        /// </value>
        public IValueMonitor<TimeSpan> TimeLeft
        {
            get { return timeLeft; }
        }

        /// <summary>
        /// 获取执行任务总数
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        public int Total
        {
            get
            {
                if (toDo == null) return 0;
                return toDo.Count();
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 设置需要执行的委托集合
        /// </summary>
        /// <param name="toDoActions">需要执行的委托集合</param>
        public void SetActionsTodo(IEnumerable<Action> toDoActions)
        {
            toDo = toDoActions;
        }

        private void percentageProgress_ValueChanged(int oldValue, int newValue)
        {
            worker.ReportProgress(newValue);
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int _total = toDo.Count();
            startTime = DateTime.Now;
            int _current = 0;
            if (!IsParallel)
            {
                foreach (var next in toDo)
                {
                    next();
                    _current++;
                    if (worker.CancellationPending == true) return;
                    percentageProgress.Value = (int)((double)_current / (double)_total * 100.0);
                    double passedMs = (DateTime.Now - startTime).TotalMilliseconds;
                    double oneUnitMs = passedMs / _current;
                    double leftMs = (_total - _current) * oneUnitMs;
                    timeLeft.Value = TimeSpan.FromMilliseconds(leftMs);
                }
            }
            else
            {
                Parallel.For(0, _total - 1,
                    (index, loopstate) =>
                    {
                        toDo.ElementAt(index)();
                        if (worker.CancellationPending == true) loopstate.Stop();
                        Interlocked.Increment(ref _current);

                        percentageProgress.Value = (int)((double)_current / (double)_total * 100.0);
                        double _passedMs = (DateTime.Now - startTime).TotalMilliseconds,
                               _oneUnitMs = _passedMs / _current,
                               _leftMs = (_total - _current) * _oneUnitMs;
                        timeLeft.Value = TimeSpan.FromMilliseconds(_leftMs);
                    }
                    );
            }
        }

        #endregion Methods
    }
}