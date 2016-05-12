using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using YanZhiwei.DotNet2.Utilities.Models;

namespace YanZhiwei.DotNet4.Utilities.Models
{
    /// <summary>
    /// 支持泛型的列表字段排序条件
    /// </summary>
    /// <typeparam name="T">列表元素类型</typeparam>
    public class SortCondition<T> : SortCondition
    {
        /// <summary>
        /// 使用排序字段 初始化一个<see cref="SortCondition"/>类型的新实例
        /// </summary>
        public SortCondition(Expression<Func<T, object>> keySelector)
            : this(keySelector, ListSortDirection.Ascending)
        { }

        /// <summary>
        /// 使用排序字段与排序方式 初始化一个<see cref="SortCondition"/>类型的新实例
        /// </summary>
        public SortCondition(Expression<Func<T, object>> keySelector, ListSortDirection listSortDirection)
            : base(GetPropertyName(keySelector), listSortDirection)
        { }

        /// <summary>
        /// 从泛型委托获取属性名
        /// </summary>
        private static string GetPropertyName(Expression<Func<T, object>> keySelector)
        {
            string param = keySelector.Parameters.First().Name;
            string operand = (((dynamic)keySelector.Body).Operand).ToString();
            operand = operand.Substring(param.Length + 1, operand.Length - param.Length - 1);
            return operand;
        }
    }
}