namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System.Collections.Generic;

    using YanZhiwei.DotNet2.Utilities.Core;

    /// <summary>
    /// Enumerable 帮助类
    /// </summary>
    public static class IEnumerableHelper
    {
        #region Methods

        /// <summary>
        /// 线程安全【上锁】
        ///<para> eg: foreach(var item in someList.AsLocked(someLock))</para>
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="source">IEnumerable</param>
        /// <param name="syncObject">lock对象</param>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<T> AsLocked<T>(this IEnumerable<T> source, object syncObject)
        {
            /*
            * 参考：
            * 1. http://www.codeproject.com/Articles/56575/Thread-safe-enumeration-in-C
            */
            return new ThreadSafeEnumerableHelper<T>(source, syncObject);
        }

        ///// <summary>
        /////获取总数
        ///// </summary>
        ///// <typeparam name="T">泛型</typeparam>
        ///// <param name="source">The source.</param>
        ///// <returns>总数</returns>
        //public static int Count<T>(this IEnumerable<T> source)
        //    where T : class
        //{
        //    int _count = 0;
        //    ICollection<T> _c = source as ICollection<T>;
        //    if (_c != null)
        //        _count = _c.Count;
        //    return _count;
        //}

        #endregion Methods
    }
}