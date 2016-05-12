namespace YanZhiwei.DotNet3._5.Utilities.Common
{
    using System;
    using System.Linq.Expressions;

    using YanZhiwei.DotNet3._5.Utilities.Core;

    /// <summary>
    /// INotifyPropertyChanged 帮助类
    /// </summary>
    public static class IPropertyChangedHelper
    {
        #region Methods

        /// <summary>
        /// 属性更改通知到客户端
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <typeparam name="TProperty">泛型</typeparam>
        /// <param name="source">类型，继承NotifyPropertyChanged</param>
        /// <param name="keySelector">选择器；eg:this.NotifyPropertyChanged(p => p.Name);  </param>
        public static void NotifyPropertyChanged<T, TProperty>(this T source, Expression<Func<T, TProperty>> keySelector)
            where T : NotifyPropertyChanged
        {
            /*
             public class Person : NotifyPropertyChanged
             {
             private string name;
             public string Name
             {
             get{
                return name;
                }
                set
                {
                if (name != value)
                {
                    name = value;
                    this.NotifyPropertyChanged(p => p.name);
                }
             }
            }
            }
            */

            MemberExpression _expression = keySelector.Body as MemberExpression;
            if (_expression != null)
            {
                string _propertyName = _expression.Member.Name;
                source.NotifyChanged(_propertyName);
            }
        }

        #endregion Methods
    }
}