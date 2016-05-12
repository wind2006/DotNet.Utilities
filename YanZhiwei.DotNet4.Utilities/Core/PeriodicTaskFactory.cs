namespace YanZhiwei.DotNet4.Utilities.Core
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// 创建周期性Task
    /// <para>参考：http://stackoverflow.com/questions/4890915/is-there-a-task-based-replacement-for-system-threading-timer </para>
    /// </summary>
    public static class PeriodicTaskFactory
    {
        #region Methods

        /// <summary>
        /// 创建周期性任务
        /// </summary>
        /// <param name="action">委托</param>
        /// <param name="intervalInMilliseconds">以毫秒为单位的时间间隔。</param>
        /// <param name="delayInMilliseconds">以毫秒为单位，等待多长时间才能启动计时器。</param>
        /// <param name="duration">持续时间
        /// <example>如果持续时间设置为10秒，此任务可以运行的最长时间为10秒。</example></param>
        /// <param name="maxIterations">最大迭代次数</param>
        /// <param name="synchronous">如果设置为 true 以阻塞的方式执行每个时期和每次定期执行的任务包括在任务。</param>
        /// <param name="cancelToken">CancellationToken</param>
        /// <param name="periodicTaskCreationOptions">TaskCreationOptions</param>
        /// <returns>Task</returns>
        public static Task Start(Action action,
            int intervalInMilliseconds = Timeout.Infinite,
            int delayInMilliseconds = 0,
            int duration = Timeout.Infinite,
            int maxIterations = -1,
            bool synchronous = false,
            CancellationToken cancelToken = new CancellationToken(),
            TaskCreationOptions periodicTaskCreationOptions = TaskCreationOptions.None)
        {
            Stopwatch _stopWatch = new Stopwatch();
            Action _wrapperAction = () =>
            {
                CheckIfCancelled(cancelToken);
                action();
            };
            Action _mainAction = () =>
            {
                MainPeriodicTaskAction(intervalInMilliseconds, delayInMilliseconds, duration, maxIterations, cancelToken, _stopWatch, synchronous, _wrapperAction, periodicTaskCreationOptions);
            };
            /*
             * 如果开发人员知道一个Task要长时间运行，会长时间霸占一个底层线性资源，开发人员应该告诉线程池共享线程不会太快交还，
             * 这样一来，线程池更有可能为任务创建一个专用线程（而不是分配一个共享线程）。
             * 在StartNew的时候，使用TaskCreationOptions.LongRunning选项。
             */
            return Task.Factory.StartNew(_mainAction, cancelToken, TaskCreationOptions.LongRunning, TaskScheduler.Current);
        }

        /// <summary>
        /// 检查是否
        /// </summary>
        /// <param name="cancelToken">CancellationToken</param>
        private static void CheckIfCancelled(CancellationToken cancelToken)
        {
            if (cancelToken == null)
                throw new ArgumentNullException("cancellationToken");
            cancelToken.ThrowIfCancellationRequested();//如果已请求取消此标记，则引发 OperationCanceledException。
        }

        /// <summary>
        /// Mains the periodic task action.
        /// </summary>
        /// <param name="intervalInMilliseconds">以毫秒为单位的时间间隔.</param>
        /// <param name="delayInMilliseconds">以毫秒为单位，等待多长时间才能启动计时器。</param>
        /// <param name="duration">以毫秒为单位，持续时间</param>
        /// <param name="maxIterations">最大迭代次数</param>
        /// <param name="cancelToken">CancellationToken</param>
        /// <param name="stopWatch">The stop watch.</param>
        /// <param name="synchronous">如果设置为 true 以阻塞的方式执行每个时期和每次定期执行的任务包括在任务。</param>
        /// <param name="wrapperAction">委托</param>
        /// <param name="periodicTaskCreationOptions">TaskCreationOptions</param>
        private static void MainPeriodicTaskAction(int intervalInMilliseconds,
            int delayInMilliseconds,
            int duration,
            int maxIterations,
            CancellationToken cancelToken,
            Stopwatch stopWatch,
            bool synchronous,
            Action wrapperAction,
            TaskCreationOptions periodicTaskCreationOptions)
        {
            TaskCreationOptions _subTaskCreationOptions = TaskCreationOptions.AttachedToParent | periodicTaskCreationOptions;
            CheckIfCancelled(cancelToken);
            if (delayInMilliseconds > 0)
            {
                Thread.Sleep(delayInMilliseconds);
            }
            if (maxIterations == 0) { return; }
            int _iteration = 0;//迭代次数

            //ManualResetEventSlim：提供 ManualResetEvent 的简化版本。
            using (ManualResetEventSlim periodResetEvent = new ManualResetEventSlim(false))
            {
                while (true)
                {
                    CheckIfCancelled(cancelToken);
                    Task _subTask = Task.Factory.StartNew(wrapperAction, cancelToken, _subTaskCreationOptions, TaskScheduler.Current);
                    if (synchronous)
                    {
                        stopWatch.Start();
                        try
                        {
                            _subTask.Wait(cancelToken);
                        }
                        catch
                        {
                        }
                        stopWatch.Stop();
                    }

                    //时间间隔
                    if (intervalInMilliseconds == Timeout.Infinite)
                    {
                        break;
                    }
                    _iteration++;
                    //判断最大迭代次数
                    if (maxIterations > 0 && _iteration >= maxIterations)
                    {
                        break;
                    }
                    try
                    {
                        stopWatch.Start();
                        periodResetEvent.Wait(intervalInMilliseconds, cancelToken);
                        stopWatch.Stop();
                    }
                    finally
                    {
                        periodResetEvent.Reset();
                    }
                    CheckIfCancelled(cancelToken);
                    if (duration > 0 && stopWatch.ElapsedMilliseconds >= duration) { break; }
                }
            }
        }

        #endregion Methods
    }
}