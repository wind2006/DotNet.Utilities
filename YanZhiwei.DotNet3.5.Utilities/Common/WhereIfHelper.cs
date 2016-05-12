namespace YanZhiwei.DotNet3._5.Utilities.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using YanZhiwei.DotNet2.Utilities.Common;

    /// <summary>
    /// WhereIf 帮助类
    /// </summary>
    /// 创建时间:2015-05-27 10:05
    /// 备注说明:<c>null</c>
    public static class WhereIfHelper
    {
        #region Methods

        /// <summary>
        /// Wheres if.
        /// <para>
        /// eg: var _result = _students.WhereIf(!string.IsNullOrEmpty(_name), c => c.Name == _name)
        ///                            .WhereIf(_age != 100, c => c.Age == _age).FirstOrDefault();
        /// </para>
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="source">集合</param>
        /// <param name="condition">成立条件</param>
        /// <param name="keySelector">选择器</param>
        /// <returns>集合</returns>
        /// 时间：2015-12-10 14:47
        /// 备注：
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> keySelector)
        {
            return condition ? source.Where(keySelector) : source;
        }

        /// <summary>
        /// 当等于NULL或空的时候，执行选择器
        /// <para>
        /// eg: var _finded = PersonList.WhereIfNullOrEmpty(string.Empty, c => c.Name.Contains("yanzhiwei"));
        /// </para>
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="source">集合</param>
        /// <param name="condition">成立条件</param>
        /// <param name="keySelector">成立条件</param>
        /// <returns>集合</returns>
        /// 创建时间:2015-05-27 10:19
        /// 备注说明:<c>null</c>
        public static IEnumerable<T> WhereIfNullOrEmpty<T>(this IEnumerable<T> source, string condition, Func<T, bool> keySelector)
        {
            return condition.IsNullOrEmpty() == false ? source.Where(keySelector) : source;
        }

        #endregion Methods
    }
}