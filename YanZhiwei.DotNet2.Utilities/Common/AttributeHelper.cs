namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System;

    /// <summary>
    /// Attribute 帮助类
    /// </summary>
    /// 时间：2016-01-12 15:17
    /// 备注：
    public static class AttributeHelper
    {
        #region Methods

        /// <summary>
        /// 获取自定义Attribute
        /// </summary>
        /// <typeparam name="TModel">泛型</typeparam>
        /// <typeparam name="TAttribute">泛型</typeparam>
        /// <returns>未获取到则返回NULL</returns>
        /// 时间：2016-01-12 15:22
        /// 备注：
        public static TAttribute GetCustom<TModel, TAttribute>()
            where TModel : class
            where TAttribute : Attribute
        {
            Type _type = typeof(TModel);

            object[] _customAttribute = _type.GetCustomAttributes(typeof(TAttribute), true);
            if (_customAttribute != null && _customAttribute.Length > 0)
            {
                TAttribute _tAttribute = _customAttribute[0] as TAttribute;
                return _tAttribute;
            }
            return null;
        }

        #endregion Methods
    }
}