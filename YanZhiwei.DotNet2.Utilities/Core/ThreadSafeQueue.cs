namespace YanZhiwei.DotNet2.Utilities.Core
{
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// Queue线程安全实现的帮助类
    /// 说明
    /// 默认读锁超时1000毫秒
    /// 默认写锁超时1000毫秒
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public class ThreadSafeQueue<T>
    {
        #region Fields

        /// <summary>
        /// 泛型Queue对象
        /// </summary>
        private readonly Queue<T> queueTF;

        /// <summary>
        /// ReaderWriterLock 对象
        /// </summary>
        private readonly ReaderWriterLock rwlock = new ReaderWriterLock();

        /* 参考资料
         * 参考：
         * 1. http://www.codeproject.com/Articles/38908/Thread-Safe-Generic-Queue-Class
         * 2. http://stackoverflow.com/questions/13416889/thread-safe-queue-enqueue-dequeue
         * 3. http://blogs.msdn.com/b/jaredpar/archive/2009/02/16/a-more-usable-thread-safe-collection.aspx
         */
        /// <summary>
        /// 默认读锁超时1000毫秒
        /// </summary>
        private static int readerTimeout = 1000;

        /// <summary>
        /// 默认写锁超时1000毫秒
        /// </summary>
        private static int writerTimeout = 1000;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ThreadSafeQueue()
        {
            queueTF = new Queue<T>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="capacity">初始容量</param>
        public ThreadSafeQueue(int capacity)
        {
            queueTF = new Queue<T>(capacity);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="collection">IEnumerable</param>
        public ThreadSafeQueue(IEnumerable<T> collection)
        {
            queueTF = new Queue<T>(collection);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Count 【线程安全】
        /// </summary>
        /// <returns>queue 数量</returns>
        public int Count()
        {
            rwlock.AcquireReaderLock(readerTimeout);
            try
            {
                return queueTF.Count;
            }
            finally
            {
                rwlock.ReleaseReaderLock();
            }
        }

        /// <summary>
        /// Dequeue【线程安全】
        /// </summary>
        /// <returns>泛型</returns>
        public T Dequeue()
        {
            rwlock.AcquireReaderLock(readerTimeout);
            try
            {
                return queueTF.Dequeue();
            }
            finally
            {
                rwlock.ReleaseReaderLock();
            }
        }

        /// <summary>
        /// DequeueAll【线程安全】
        /// </summary>
        /// <returns>IList</returns>
        public IList<T> DequeueAll()
        {
            rwlock.AcquireReaderLock(readerTimeout);
            try
            {
                IList<T> _list = new List<T>();
                while (queueTF.Count > 0)
                {
                    _list.Add(queueTF.Dequeue());
                }

                return _list;
            }
            finally
            {
                rwlock.ReleaseReaderLock();
            }
        }

        /// <summary>
        /// Enqueue【线程安全】
        /// </summary>
        /// <param name="item">泛型</param>
        public void Enqueue(T item)
        {
            rwlock.UpgradeToWriterLock(writerTimeout);
            try
            {
                queueTF.Enqueue(item);
            }
            finally
            {
                rwlock.ReleaseWriterLock();
            }
        }

        /// <summary>
        /// EnqueueAll【线程安全】
        /// </summary>
        /// <param name="itemsToQueue">IEnumerable</param>
        public void EnqueueAll(IEnumerable<T> itemsToQueue)
        {
            rwlock.UpgradeToWriterLock(writerTimeout);
            try
            {
                foreach (T item in itemsToQueue)
                {
                    queueTF.Enqueue(item);
                }
            }
            finally
            {
                rwlock.ReleaseWriterLock();
            }
        }

        /// <summary>
        /// EnqueueAll【线程安全】
        /// </summary>
        /// <param name="itemsToQueue">IList</param>
        public void EnqueueAll(IList<T> itemsToQueue)
        {
            rwlock.UpgradeToWriterLock(writerTimeout);
            try
            {
                foreach (T item in itemsToQueue)
                {
                    queueTF.Enqueue(item);
                }
            }
            finally
            {
                rwlock.ReleaseWriterLock();
            }
        }

        /// <summary>
        /// GetEnumerator【线程安全】
        /// </summary>
        /// <returns>IEnumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            Queue<T> _tmpQueue;
            rwlock.AcquireReaderLock(readerTimeout);
            try
            {
                _tmpQueue = new Queue<T>(queueTF);
            }
            finally
            {
                rwlock.ReleaseReaderLock();
            }

            foreach (T item in _tmpQueue)
            {
                yield return item;
            }
        }

        #endregion Methods
    }
}