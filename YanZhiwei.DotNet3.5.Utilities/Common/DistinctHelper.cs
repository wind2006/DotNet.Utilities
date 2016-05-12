namespace YanZhiwei.DotNet3._5.Utilities.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using YanZhiwei.DotNet3._5.Utilities.Core;

    /// <summary>
    /// Distinct/去重复项 帮助类
    /// </summary>
    /// 创建时间:2015-05-27 9:28
    /// 备注说明:<c>null</c>
    public static class DistinctHelper
    {
        #region Methods

        /// <summary>
        /// 去重复
        /// <para>
        /// eg: Person[] _finded = _personList.Distinct'Person, string'(p => p.Name, StringComparer.InvariantCultureIgnoreCase).ToArray();
        /// </para>
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <typeparam name="V">泛型</typeparam>
        /// <param name="source">集合</param>
        /// <param name="keySelector">选择器</param>
        /// <returns></returns>
        /// 创建时间:2015-05-27 9:28
        /// 备注说明:<c>null</c>
        public static IEnumerable<T> Distinct<T, V>(this IEnumerable<T> source, Func<T, V> keySelector)
        {
            return source.Distinct(new CommonEqualityComparer<T, V>(keySelector));
        }

        /// <summary>
        /// 去重复
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <typeparam name="V">泛型</typeparam>
        /// <param name="source">集合</param>
        /// <param name="keySelector">选择器</param>
        /// <param name="comparer">IEqualityComparer</param>
        /// <returns></returns>
        /// 创建时间:2015-05-27 9:28
        /// 备注说明:<c>null</c>
        public static IEnumerable<T> Distinct<T, V>(this IEnumerable<T> source, Func<T, V> keySelector, IEqualityComparer<V> comparer)
        {
            return source.Distinct(new CommonEqualityComparer<T, V>(keySelector, comparer));
        }

        #endregion Methods
    }
}