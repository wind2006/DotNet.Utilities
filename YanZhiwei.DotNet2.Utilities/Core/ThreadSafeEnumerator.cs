namespace YanZhiwei.DotNet2.Utilities.Core
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// IEnumerator 线程安全
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public class ThreadSafeEnumerator<T> : IEnumerator<T>
    {
        #region Fields

        /*
         * 参考：
         * 1.http://www.codeproject.com/KB/cs/safe_enumerable.aspx
         * 2.http://theburningmonk.com/2010/03/thread-safe-enumeration-in-csharp/
         */
        /// <summary>
        /// 泛型IEnumerator对象
        /// </summary>
        private readonly IEnumerator<T> innerEnumerator;

        /// <summary>
        /// 锁对象
        /// </summary>
        private readonly object syncObject;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="inner">IEnumerator</param>
        /// <param name="syncObj">object</param>
        public ThreadSafeEnumerator(IEnumerator<T> inner, object syncObj)
        {
            innerEnumerator = inner;
            syncObject = syncObj;
            Monitor.Enter(syncObject);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 返回当前类型
        /// </summary>
        public T Current
        {
            get { return innerEnumerator.Current; }
        }

        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <value>
        /// The current.
        /// </value>
        object IEnumerator.Current
        {
            get { return Current; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务。
        /// </summary>
        public void Dispose()
        {
            Monitor.Exit(syncObject);
        }

        /// <summary>
        /// 将枚举数推进到集合的下一个元素。
        /// </summary>
        /// <returns>
        /// 如果枚举数成功地推进到下一个元素，则为 true；如果枚举数越过集合的结尾，则为 false。
        /// </returns>
        public bool MoveNext()
        {
            return innerEnumerator.MoveNext();
        }

        /// <summary>
        /// 将枚举数设置为其初始位置，该位置位于集合中第一个元素之前。
        /// </summary>
        public void Reset()
        {
            innerEnumerator.Reset();
        }

        #endregion Methods
    }
}