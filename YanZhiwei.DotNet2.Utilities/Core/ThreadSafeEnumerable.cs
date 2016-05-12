namespace YanZhiwei.DotNet2.Utilities.Core
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// IEnumerable 线程安全
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public class ThreadSafeEnumerableHelper<T> : IEnumerable<T>
    {
        #region Fields

        /*
         *参考：
         *1. http://www.codeproject.com/KB/cs/safe_enumerable.aspx
         */
        /// <summary>
        /// 泛型IEnumerable对象
        /// </summary>
        private readonly IEnumerable<T> innerEnumerable;

        /// <summary>
        /// 锁对象
        /// </summary>
        private readonly object syncObject;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadSafeEnumerableHelper{T}"/> class.
        /// </summary>
        /// <param name="inner">The inner.</param>
        /// <param name="syncObj">The synchronize object.</param>
        public ThreadSafeEnumerableHelper(IEnumerable<T> inner, object syncObj)
        {
            syncObject = syncObj;
            innerEnumerable = inner;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>IEnumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new ThreadSafeEnumerator<T>(innerEnumerable.GetEnumerator(), syncObject);
        }

        /// <summary>
        /// 返回一个循环访问集合的枚举数。
        /// </summary>
        /// <returns>
        /// 可用于循环访问集合的 <see cref="T:System.Collections.IEnumerator" /> 对象。
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion Methods
    }
}