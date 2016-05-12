namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System.Threading;

    /// <summary>
    ///  ThreadPool 帮助类
    /// </summary>
    public class ThreadPoolHelper
    {
        #region Properties

        /// <summary>
        /// 线程池中的活动工作线程数
        /// </summary>
        public static int ActiveThreadNumber
        {
            get
            {
                int _maxNumber, _availableNumber, _ioNumber;
                ThreadPool.GetMaxThreads(out _maxNumber, out _ioNumber);
                ThreadPool.GetAvailableThreads(out _availableNumber, out _ioNumber);
                return _maxNumber - _availableNumber;
            }
        }

        /// <summary>
        /// 线程池中的可用工作线程数
        /// </summary>
        public static int AvailableThreadNumber
        {
            get
            {
                int _availableNumber, _ioNumber;
                ThreadPool.GetAvailableThreads(out _availableNumber, out _ioNumber);
                return _availableNumber;
            }
        }

        /// <summary>
        /// 获取线程池中的最大工作线程数
        /// </summary>
        public static int MaxThreadNumber
        {
            get
            {
                int _maxNumber, _ioNumber;
                ThreadPool.GetMaxThreads(out _maxNumber, out _ioNumber);
                return _maxNumber;
            }
        }

        #endregion Properties
    }
}