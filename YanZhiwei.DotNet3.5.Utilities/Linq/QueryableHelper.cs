using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace YanZhiwei.DotNet3._5.Utilities.Linq
{
    /// <summary>
    /// Queryable 帮助类
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public static class QueryableHelper
    {
        #region 私有变量
        private static Dictionary<string, LambdaExpression> cache = new Dictionary<string, LambdaExpression>();
        #endregion
        #region 排序
        /// 排序
        /// <para>eg: PersonList.AsQueryable().OrderBy(p => p.Name, true).ToList();</para>
        /// <para>ref:http://www.cnblogs.com/ldp615/archive/2012/01/15/orderby-extensions.html </para>
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <typeparam name="TProperty">泛型属性</typeparam>
        /// <param name="queryable">IQueryable</param>
        /// <param name="expression">依据排序的字段名称</param>
        /// <param name="desc">是否降序</param>
        /// <returns>IQueryable</returns>
        private static IQueryable<T> OrderByEx<T>(this IQueryable<T> queryable, string propertyName, bool desc = false)
        {
            var _keySelector = GetLambdaExpression<T>(propertyName);
            return desc ? queryable.OrderByDescending(_keySelector) : Queryable.OrderBy(queryable,);
        }
        private static LambdaExpression GetLambdaExpression<T>(string propertyName)
        {
            if (cache.ContainsKey(propertyName)) return cache[propertyName];
            Type _curType = typeof(T);
            var _param = Expression.Parameter(_curType, _curType.Name);
            var _body = Expression.Property(_param, propertyName);
            var _keySelector = Expression.Lambda(_body, _param);
            cache[propertyName] = _keySelector;
            return _keySelector;
        }
        #endregion
    }
}
