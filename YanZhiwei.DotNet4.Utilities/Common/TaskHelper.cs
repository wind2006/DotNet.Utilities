namespace YanZhiwei.DotNet4.Utilities.Common
{
    using System.Threading.Tasks;
    using System.Timers;

    /// <summary>
    /// Task 帮助类
    /// </summary>
    public static class TaskHelper
    {
        #region Methods

        /// <summary>
        /// Task延迟
        /// 参考：http://stackoverflow.com/questions/15341962/how-to-put-a-task-to-sleep-or-delay-in-c-sharp-4-0
        /// </summary>
        /// <param name="milliseconds">时间</param>
        /// <returns>Task</returns>
        public static Task Delay(double milliseconds)
        {
            var _tcs = new TaskCompletionSource<bool>();
            Timer _timer = new Timer();
            _timer.Elapsed += (obj, args) =>
            {
                _tcs.TrySetResult(true);
            };
            _timer.Interval = milliseconds;
            _timer.AutoReset = false;
            _timer.Start();
            return _tcs.Task;
        }

        #endregion Methods
    }
}