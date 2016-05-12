namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System.Collections.Generic;

    /// <summary>
    /// List帮助类
    /// </summary>
    public static class IListHelper
    {
        #region Methods

        /// <summary>
        /// 集合添加
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="self">本身集合</param>
        /// <param name="list">需要添加集合</param>
        public static void AddRange<T>(this IList<T> self, IEnumerable<T> list)
            where T : class
        {
            ((List<T>)self).AddRange(list);
        }

        /// <summary>
        /// 去重复集合添加
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="self">本身集合</param>
        /// <param name="items">需要集合</param>
        public static void AddUnique<T>(this IList<T> self, IEnumerable<T> items)
            where T : class
        {
            foreach (T item in items)
            {
                if (!self.Contains(item))
                {
                    self.Add(item);
                }
            }
        }

        /// <summary>
        /// 去重复集合添加
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="self">本身集合</param>
        /// <param name="items">需要添加集合</param>
        /// <param name="comparaer">IComparer</param>
        public static void AddUnique<T>(this List<T> self, IEnumerable<T> items, IComparer<T> comparaer)
            where T : class
        {
            self.Sort(comparaer);
            foreach (T item in items)
            {
                int _result = self.BinarySearch(item, comparaer);//搜索前需要排序
                if (_result < 0)
                    self.Add(item);
            }
        }

        /// <summary>
        /// 转换为List
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="self">需要转换的集合</param>
        /// <returns>List</returns>
        public static List<T> ToList<T>(IEnumerable<T> self)
        {
            List<T> _toList = new List<T>(self);
            return _toList;
        }

        #endregion Methods
    }
}