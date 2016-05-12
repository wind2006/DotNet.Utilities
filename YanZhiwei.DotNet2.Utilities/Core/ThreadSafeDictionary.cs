namespace YanZhiwei.DotNet2.Utilities.Core
{
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// 对于Dictionary线程安全操作帮助类
    /// 说明
    /// 默认读锁超时1000毫秒
    /// 默认写锁超时1000毫秒
    /// .NET 4.0+ 可以使用ConcurrentDictionary来实现。
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public class ThreadSafeDictionary<T>
    {
        #region Fields

        /*知识
         * 1.
         * 使用Monitor或Mutex进行同步控制的问题：由于独占访问模型不允许任何形式的并发访问，这样的效率总是不太高。
         * 许多时候，应用程序在访问资源时是进行读操作，写操作相对较少。为解决这一问题，
         * C#提供了System.Threading.ReaderWriterLock类以适应多用户读/单用户写的场景。该类可实现以下功能：
         * 如果资源未被写操作锁定，那么任何线程都可对该资源进行读操作锁定，并且对读操作锁数量没有限制，即多个线程可同时对该资源进行读操作锁定，以读取数据。
         * 如果资源未被添加任何读或写操作锁，那么一个且仅有一个线程可对该资源添加写操作锁定，以写入数据。简单的讲就是：读操作锁是共享锁，允许多个线程同时读取数据；
         * 写操作锁是独占锁，同一时刻，仅允许一个线程进行写操作。
         * 引用链接：http://www.csharpwin.com/dotnetspace/12761r5814.shtml
         *
         * 2.
         * ReaderWriterLock 用于同步对资源的访问。在任一特定时刻，它允许多个线程同时进行读访问，或者允许单个线程进行写访问。
         * 在资源不经常发生更改的情况下，ReaderWriterLock 所提供的吞吐量比简单的一次只允许一个线程的锁（如 Monitor）更高。
         * 在多数访问为读访问，而写访问频率较低、持续时间也比较短的情况下，ReaderWriterLock 的性能最好。
         * 多个读线程与单个写线程交替进行操作，所以读线程和写线程都不会长时间阻止。
         * 大多数在 ReaderWriterLock 上获取锁的方法都采用超时值。使用超时可以避免应用程序中出现死锁。
         * 如果不使用超时，这两个线程将出现死锁。
         * 引用链接：http://msdn.microsoft.com/zh-cn/library/system.threading.readerwriterlock(v=vs.80).aspx
         *
         * 参考
         * 1. http://tinythreadsafecache.codeplex.com/SourceControl/latest#TinyThreadSafeCache.cs
         * 2. http://www.grumpydev.com/2010/02/25/thread-safe-dictionarytkeytvalue/
         * 3. http://stackoverflow.com/questions/157933/whats-the-best-way-of-implementing-a-thread-safe-dictionary
         * 4. http://stackoverflow.com/questions/15095817/adding-to-a-generic-dictionary-causes-indexoutofrangeexception
         */
        /// <summary>
        /// 默认读锁超时1000毫秒
        /// </summary>
        private static int readerTimeout = 1000;

        /// <summary>
        /// 默认写锁超时1000毫秒
        /// </summary>
        private static int writerTimeout = 1000;

        /// <summary>
        /// Dictionary 对象
        /// </summary>
        private Dictionary<string, T> dic = new Dictionary<string, T>();

        /// <summary>
        /// ReaderWriterLock对象
        /// .NET 3.5+ 推荐用ReaderWriterLockSlim
        /// </summary>
        private ReaderWriterLock rwlock = new ReaderWriterLock();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ThreadSafeDictionary()
        {
            readerTimeout = 1000;
            writerTimeout = 1000;
        }

        /// <summary>
        /// 带参构造函数
        /// </summary>
        /// <param name="readerTimeout">读锁超时设置【单位毫秒】</param>
        /// <param name="writerTimeout">写锁超时设置【单位毫秒】</param>
        public ThreadSafeDictionary(int readerTimeout, int writerTimeout)
        {
            ThreadSafeDictionary<T>.readerTimeout = readerTimeout;
            ThreadSafeDictionary<T>.writerTimeout = writerTimeout;
        }

        #endregion Constructors

        #region Indexers

        /// <summary>
        /// This【线程安全】
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public T this[string key]
        {
            get
            {
                rwlock.AcquireReaderLock(readerTimeout);
                try
                {
                    return dic[key];
                }
                finally
                {
                    rwlock.ReleaseReaderLock();
                }
            }

            set
            {
                rwlock.AcquireWriterLock(writerTimeout);
                try
                {
                    dic[key] = value;
                }
                finally
                {
                    rwlock.ReleaseWriterLock();
                }
            }
        }

        #endregion Indexers

        #region Methods

        /// <summary>
        /// Add【线程安全】 默认超时1000毫秒
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        public void Add(string key, T val)
        {
            Add(key, val, writerTimeout);
        }

        /// <summary>
        /// Add【线程安全】
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="timeout">超时设置【毫秒】</param>
        public void Add(string key, T val, int timeout)
        {
            rwlock.AcquireWriterLock(timeout);
            try
            {
                dic[key] = val;
            }
            finally
            {
                rwlock.ReleaseWriterLock();
            }
        }

        /// <summary>
        /// Clear【线程安全】 默认超时1000毫秒
        /// </summary>
        public void Clear()
        {
            Clear(writerTimeout);
        }

        /// <summary>
        /// Clear【线程安全】
        /// </summary>
        /// <param name="timeout">超时设置【毫秒】</param>
        public void Clear(int timeout)
        {
            rwlock.AcquireWriterLock(timeout);
            try
            {
                dic.Clear();
            }
            finally
            {
                rwlock.ReleaseWriterLock();
            }
        }

        /// <summary>
        /// ContainsKey【线程安全】 默认超时1000毫秒
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>是否包含</returns>
        public bool ContainsKey(string key)
        {
            return ContainsKey(key, readerTimeout);
        }

        /// <summary>
        /// ContainsKey【线程安全】
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="timeout">超时设置【毫秒】</param>
        /// <returns>是否包含</returns>
        public bool ContainsKey(string key, int timeout)
        {
            rwlock.AcquireReaderLock(timeout);
            try
            {
                return dic.ContainsKey(key);
            }
            finally
            {
                rwlock.ReleaseReaderLock();
            }
        }

        /// <summary>
        /// Count【线程安全】 默认超时1000毫秒
        /// </summary>
        /// <returns>数量</returns>
        public int Count()
        {
            return Count(readerTimeout);
        }

        /// <summary>
        /// Count【线程安全】
        /// </summary>
        /// <param name="timeout">超时设置【毫秒】</param>
        /// <returns>Count</returns>
        public int Count(int timeout)
        {
            rwlock.AcquireReaderLock(timeout);
            try
            {
                return dic.Count;
            }
            finally
            {
                rwlock.ReleaseReaderLock();
            }
        }

        /// <summary>
        /// Get【线程安全】 默认超时1000毫秒
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public T Get(string key)
        {
            return Get(key, readerTimeout);
        }

        /// <summary>
        /// Get【线程安全】
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="timeout">超时设置【毫秒】</param>
        /// <returns>值</returns>
        public T Get(string key, int timeout)
        {
            rwlock.AcquireReaderLock(timeout);
            try
            {
                T val;
                dic.TryGetValue(key, out val);
                return val;
            }
            finally
            {
                rwlock.ReleaseReaderLock();
            }
        }

        /// <summary>
        /// Remove【线程安全】 默认超时1000毫秒
        /// </summary>
        /// <param name="key">键</param>
        public void Remove(string key)
        {
            Remove(key, writerTimeout);
        }

        /// <summary>
        /// Remove【线程安全】
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="timeout">超时设置【毫秒】</param>
        public void Remove(string key, int timeout)
        {
            rwlock.AcquireWriterLock(timeout);
            try
            {
                dic.Remove(key);
            }
            finally
            {
                rwlock.ReleaseWriterLock();
            }
        }

        #endregion Methods
    }
}