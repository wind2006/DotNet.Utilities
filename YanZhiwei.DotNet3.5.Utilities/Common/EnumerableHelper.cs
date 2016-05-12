namespace YanZhiwei.DotNet3._5.Utilities.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Enumerable帮助类
    /// </summary>
    public static class EnumerableHelper
    {
        #region Methods

        /// <summary>
        /// 判断IEnumerable是否是空
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="items">The items.</param>
        /// <returns>是否为空</returns>
        public static bool IsEmpty<T>(this IEnumerable<T> items)
        {
            bool _result = true;
            if (items != null)
            {
                var _enumerator = items.GetEnumerator();
                _result = !_enumerator.MoveNext();
            }
            return _result;
        }

        /// <summary>
        /// 如果等于NULL则返回默认初始化集合
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="source">集合</param>
        /// <returns></returns>
        /// 时间：2015-12-15 14:39
        /// 备注：
        public static IEnumerable<T> NullToEmpty<T>(this IEnumerable<T> source)
        {
            if (source == null)
                return Enumerable.Empty<T>();

            return source;
        }

        /// <summary>
        /// 根据设定的大小分割集合
        /// <para>
        /// eg: IEnumerable'IEnumerable'Person'' _acutal = _persons.Split(5);
        /// </para>
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="source">集合</param>
        /// <param name="size">分割大小</param>
        /// <returns>分割后的集合</returns>
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, int size)
        {
            List<T> _partition = new List<T>(size);
            foreach (var item in source)
            {
                _partition.Add(item);
                if (_partition.Count == size)
                {
                    yield return _partition;
                    _partition = new List<T>(size);
                }
            }
            if (_partition.Count > 0)
                yield return _partition;
        }

        /// <summary>
        /// 转换为HashSet
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="source">集合</param>
        /// <returns>HashSet</returns>
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            HashSet<T> _result = new HashSet<T>();
            _result.UnionWith(source);
            return _result;
        }
        /// <summary>
        /// 递归扩展
        /// <para>
        /// eg:List _findedList = allZtree.Traverse(n => n.children).Where(c => c.type == "Road").ToList();
        /// </para>
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="source">集合</param>
        /// <param name="fnRecurse">委托</param>
        /// <returns>集合</returns>
        public static IEnumerable<T> Traverse<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> fnRecurse)
        {
            foreach (T current in source)
            {
                yield return current;
                IEnumerable<T> enumerable = fnRecurse(current);
                if (enumerable != null)
                {
                    foreach (T current2 in enumerable.Traverse(fnRecurse))
                    {
                        yield return current2;
                    }
                }
            }
            yield break;
        }

        #endregion Methods
    }
}