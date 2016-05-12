namespace YanZhiwei.DotNet3._5.Utilities.Core
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 定义方法以支持对象的相等比较
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// 创建时间:2015-05-27 9:23
    /// 备注说明:<c>null</c>
    public class CommonEqualityComparer<T, V> : IEqualityComparer<T>
    {
        #region Fields

        /*
         *参考:
         *1. http://www.cnblogs.com/ldp615/archive/2011/08/01/distinct-entension.html
         *2. https://msdn.microsoft.com/zh-cn/library/ms132151(v=vs.100).aspx
         *
         *知识：
         *1. IEqualityComparer<T> 接口：定义方法以支持对象的相等比较。
         */
        private IEqualityComparer<V> comparer;
        private Func<T, V> keySelector;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonEqualityComparer{T, V}"/> class.
        /// </summary>
        /// <param name="keySelector">The key selector.</param>
        /// <param name="comparer">The comparer.</param>
        /// 时间：2015-12-10 14:50
        /// 备注：
        public CommonEqualityComparer(Func<T, V> keySelector, IEqualityComparer<V> comparer)
        {
            this.keySelector = keySelector;
            this.comparer = comparer;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonEqualityComparer{T, V}"/> class.
        /// </summary>
        /// <param name="keySelector">The key selector.</param>
        /// 时间：2015-12-10 14:50
        /// 备注：
        public CommonEqualityComparer(Func<T, V> keySelector)
            : this(keySelector, EqualityComparer<V>.Default)
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Equalses the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        /// 时间：2015-12-10 14:50
        /// 备注：
        public bool Equals(T x, T y)
        {
            return comparer.Equals(keySelector(x), keySelector(y));
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        /// 时间：2015-12-10 14:50
        /// 备注：
        public int GetHashCode(T obj)
        {
            return comparer.GetHashCode(keySelector(obj));
        }

        #endregion Methods
    }
}