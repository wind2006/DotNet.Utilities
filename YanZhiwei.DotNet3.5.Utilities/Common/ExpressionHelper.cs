namespace YanZhiwei.DotNet3._5.Utilities.Common
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Expression 帮助类
    /// </summary>
    /// 创建时间:2015-05-26 13:50
    /// 备注说明:<c>null</c>
    public static class ExpressionHelper
    {
        #region Methods

        /// <summary>
        /// 获取属性名称
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <typeparam name="TProperty">属性</typeparam>
        /// <param name="keySelector">选择器</param>
        /// <returns></returns>
        /// 创建时间:2015-05-26 13:50
        /// 备注说明:<c>null</c>
        public static string GetTPropertyName<T, TProperty>(this Expression<Func<T, TProperty>> keySelector)
            where T : class
        {
            string _propertyName = string.Empty;
            if (keySelector != null)
            {
                MemberExpression _memberExpression = keySelector.Body as MemberExpression;
                if (_memberExpression != null)
                {
                    _propertyName = _memberExpression.Member.Name;
                }
            }
            return _propertyName;
        }

        #endregion Methods
    }
}