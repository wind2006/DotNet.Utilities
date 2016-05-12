namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System.Diagnostics;

    /// <summary>
    /// 运行时间测试帮助类
    /// </summary>
    public class TestHelper
    {
        #region Fields

        /// <summary>
        /// 测试运行时间
        /// </summary>
        private readonly Stopwatch watch;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 初始化
        /// </summary>
        public TestHelper()
        {
            watch = new Stopwatch();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// 获取运行时间间隔,单位：秒
        /// </summary>
        /// <returns>运行时间间隔</returns>
        public double GetElapsed()
        {
            return watch.Elapsed.TotalSeconds;
        }

        /// <summary>
        /// 停止并获取运行时间间隔,单位：秒
        /// </summary>
        /// <returns>运行时间间隔</returns>
        public double GetElapsedAndStop()
        {
            Stop();
            return GetElapsed();
        }

        /// <summary>
        /// 重置计时器
        /// </summary>
        public void Reset()
        {
            watch.Reset();
        }

        /// <summary>
        /// 开始计时
        /// </summary>
        public void Start()
        {
            watch.Start();
        }

        /// <summary>
        /// 停止计时
        /// </summary>
        public void Stop()
        {
            watch.Stop();
        }

        #endregion Methods
    }
}