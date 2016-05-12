namespace YanZhiwei.DotNet3._5.Utilities.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Timers;

    /// <summary>
    /// 对象锁辅助类
    /// </summary>
    /// 时间：2016-02-25 15:03
    /// 备注：
    public class Locker
    {
        #region Fields

        private const int ExpireMinutes = 10;

        private static readonly Dictionary<string, LockObj> dict = new Dictionary<string, LockObj>();
        private static readonly Timer timer;

        #endregion Fields

        #region Constructors

        static Locker()
        {
            timer = new Timer(60000);
            timer.Elapsed += (s, e) =>
            {
                RemovedExired();
            };
            timer.Start();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// 移除所有锁
        /// </summary>
        /// 时间：2016-02-25 15:04
        /// 备注：
        public static void RemovedExired()
        {
            lock (dict)
            {
                List<string> keys = dict.Where(x => x.Value.IsExpired()).Select(x => x.Key).ToList();
                foreach (var key in keys)
                {
                    dict.Remove(key);
                }
            }
        }

        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="action">委托</param>
        /// 时间：2016-02-25 15:03
        /// 备注：
        public static void Run(string key, Action action)
        {
            LockObj _lockObj = null;
            lock (dict)
            {
                if (!dict.ContainsKey(key))
                {
                    dict[key] = new LockObj();
                }
                _lockObj = dict[key];
                _lockObj.Time = DateTime.Now;
            }
            lock (_lockObj)
            {
                action();
            }
        }

        #endregion Methods

        #region Nested Types

        private class LockObj
        {
            #region Properties

            public DateTime Time
            {
                private get; set;
            }

            #endregion Properties

            #region Methods

            public bool IsExpired()
            {
                return this.Time < DateTime.Now.AddMinutes(-ExpireMinutes);
            }

            #endregion Methods
        }

        #endregion Nested Types
    }
}