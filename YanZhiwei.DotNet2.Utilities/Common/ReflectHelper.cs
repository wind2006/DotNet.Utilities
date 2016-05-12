namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;

    /// <summary>
    /// 反射帮助类
    /// </summary>
    public static class ReflectHelper
    {
        #region Fields

        /// <summary>
        /// 方法或属性搜索标志
        /// </summary>
        /// 日期：2015-10-10 9:08
        /// 备注：
        private static BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Public |
                          BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

        #endregion Fields

        #region Methods

        /// <summary>
        /// 利用反射来判断对象是否包含某个属性
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="instance">object</param>
        /// <param name="propertyName">需要判断的属性</param>
        /// <returns>是否包含</returns>
        public static bool ContainProperty<T>(this T instance, string propertyName)
            where T : class
        {
            if (instance != null && !string.IsNullOrEmpty(propertyName))
            {
                PropertyInfo _findedPropertyInfo = instance.GetType().GetProperty(propertyName);
                return _findedPropertyInfo != null;
            }

            return false;
        }

        /// <summary>
        /// 反射创建对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="fullName">类全名称</param>
        /// <param name="assemblyName">程序集全程</param>
        /// <returns>类型</returns>
        /// 时间：2016-01-07 14:47
        /// 备注：
        public static T CreateInstance<T>(string fullName, string assemblyName)
            where T : class, new()
        {
            string _path = fullName + "," + assemblyName;//命名空间.类型名,程序集
            Type _loadType = Type.GetType(_path);//加载类型
            object _getType = Activator.CreateInstance(_loadType, true);//根据类型创建实例
            return (T)_getType;//类型转换并返回
        }

        /// <summary>
        /// 获取实体类[DisplayName]以及本身Name
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <returns>IDictionary</returns>
        public static IDictionary<string, string> GetDisplayNames<T>()
            where T : class
        {
            IDictionary<string, string> _fields = new Dictionary<string, string>();
            PropertyInfo[] _properties = typeof(T).GetProperties();
            int _properityCnt = _properties.Length;
            foreach (PropertyInfo property in _properties)
            {
                object[] _attribute = property.GetCustomAttributes(typeof(DisplayNameAttribute), false);
                _fields.Add(property.Name, _attribute.Length == 0 ? property.Name : ((DisplayNameAttribute)_attribute[0]).DisplayName);
            }
            return _fields;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="name">要获取值的名称</param>
        /// <returns>反射获取到的值</returns>
        /// 日期：2015-10-10 9:07
        /// 备注：
        public static object GetFieldValue<T>(T obj, string name)
            where T : class
        {
            FieldInfo _fi = obj.GetType().GetField(name, bindingFlags);
            return _fi.GetValue(obj);
        }

        /// <summary>
        /// 反射调用方法
        /// </summary>
        /// <param name="obj">需反射类型</param>
        /// <param name="methodName">调用方法名称</param>
        /// <param name="args">参数</param>
        /// <returns>方法返回值</returns>
        public static object InvokeMethod(object obj, string methodName, object[] args)
        {
            object _objReturn = null;
            Type _type = obj.GetType();
            _objReturn = _type.InvokeMember(methodName, bindingFlags | BindingFlags.InvokeMethod, null, obj, args);
            return _objReturn;
        }

        /// <summary>
        /// 反射设置值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="obj">操作对象</param>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        public static void SetFieldValue<T>(T obj, string name, object value)
            where T : class
        {
            FieldInfo _fi = obj.GetType().GetField(name, bindingFlags);
            _fi.SetValue(obj, value);
        }

        /// <summary>
        /// 获取实体类[DisplayName]以及本身Name
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <returns>IDictionary</returns>
        public static IDictionary<string, string> ToDictionary<T>()
            where T : class
        {
            IDictionary<string, string> _fields = new Dictionary<string, string>();
            PropertyInfo[] _properties = typeof(T).GetProperties();
            int _properityCnt = _properties.Length;
            foreach (PropertyInfo property in _properties)
            {
                object[] _attribute = property.GetCustomAttributes(typeof(DisplayNameAttribute), false);
                _fields.Add(property.Name, _attribute.Length == 0 ? property.Name : ((DisplayNameAttribute)_attribute[0]).DisplayName);
            }

            return _fields;
        }

        #endregion Methods
    }
}