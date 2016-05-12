namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System;
    using System.ComponentModel;
    using System.Reflection;

    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public static class EnumHelper
    {
        #region Methods

        /// <summary>
        /// 判断枚举是否包括枚举常数名称
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <param name="enumName">枚举常数名称</param>
        /// <returns>是否包括枚举常数名称</returns>
        public static bool CheckedContainEnumName<T>(string enumName)
            where T : struct, IConvertible
        {
            bool _result = false;
            if (typeof(T).IsEnum)
            {
                string[] _enumnName = Enum.GetNames(typeof(T));
                if (_enumnName != null)
                {
                    for (int i = 0; i < _enumnName.Length; i++)
                    {
                        if (string.Compare(_enumnName[i], enumName, true) == 0)
                        {
                            _result = true;
                            break;
                        }
                    }
                }
            }

            return _result;
        }

        /// <summary>
        /// 从枚举中获取Description
        /// </summary>
        /// <param name="targetEnum">需要获取枚举描述的枚举</param>
        /// <returns>描述内容</returns>
        public static string GetDescription(this Enum targetEnum)
        {
            string _description = string.Empty;
            FieldInfo _fieldInfo = targetEnum.GetType().GetField(targetEnum.ToString());
            DescriptionAttribute[] _attributes = _fieldInfo.GetDescriptAttr();
            if (_attributes != null && _attributes.Length > 0)
            {
                _description = _attributes[0].Description;
            }
            else
            {
                _description = targetEnum.ToString();
            }

            return _description;
        }

        /// <summary>
        /// 获取枚举常数名称
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <param name="targetEnum">The target enum.</param>
        /// <returns>常数名称</returns>
        public static string GetName<T>(this T targetEnum)
            where T : struct, IConvertible
        {
            return Enum.GetName(typeof(T), targetEnum);
        }

        /// <summary>
        /// 根据枚举数值获取枚举常数名称
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="enumNumber">枚举数值.</param>
        /// <returns>枚举常数名称</returns>
        public static string GetName<T>(int enumNumber)
            where T : struct, IConvertible
        {
            return Enum.GetName(typeof(T), enumNumber);
        }

        /// <summary>
        /// 将枚举数值转换成数组
        /// <code>
        /// int[] _actual = EnumHelper.GetValues'int'(typeof(AreaMode));
        /// int[] _expected = new int[] { 0, 1, 2, 4, 8, 16, 32, 59 };
        /// CollectionAssert.AreEqual(_actual, _expected);
        /// </code>
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="enumType">枚举类型</param>
        /// <returns>数组</returns>
        /// 日期：2015-09-16 14:00
        /// 备注：
        public static T[] GetValues<T>(this Type enumType)
        {
            if (enumType.IsEnum)
            {
                Array _array = Enum.GetValues(enumType);
                int _count = _array.Length;
                T[] _valueArray = new T[_count];
                for (int i = 0; i < _count; i++)
                {
                    _valueArray[i] = (T)_array.GetValue(i);
                }

                return _valueArray;
            }

            return null;
        }

        /// <summary>
        /// 检查枚举是否包含
        /// <para>
        /// Assert.IsTrue(AreaMode.CITY.In(AreaMode.CITYTOWN, AreaMode.CITY));
        /// </para>
        /// </summary>
        /// <param name="data">枚举</param>
        /// <param name="values">枚举</param>
        /// <returns>是否包含</returns>
        public static bool In(this Enum data, params Enum[] values)
        {
            return Array.IndexOf(values, data) != -1;
        }

        /// <summary>
        /// 检查枚举是否不包含
        /// <para>
        /// Assert.IsTrue(AreaMode.CITYTOWN.NotIn(AreaMode.ROAD));
        /// </para>
        /// </summary>
        /// <param name="data">枚举</param>
        /// <param name="values">枚举</param>
        /// <returns>是否未包含</returns>
        public static bool NotIn(this Enum data, params Enum[] values)
        {
            return Array.IndexOf(values, data) == -1;
        }

        /// <summary>
        /// 根据Description获取枚举
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="description">枚举描述</param>
        /// <param name="defaultValue">默认枚举</param>
        /// <returns>枚举</returns>
        public static T ParseEnumDescription<T>(this string description, T defaultValue)
            where T : struct, IConvertible
        {
            if (typeof(T).IsEnum)
            {
                Type _type = typeof(T);
                foreach (FieldInfo field in _type.GetFields())
                {
                    DescriptionAttribute[] _description = field.GetDescriptAttr();
                    if (_description != null && _description.Length > 0)
                    {
                        if (string.Compare(_description[0].Description, description, true) == 0)
                        {
                            defaultValue = (T)field.GetValue(null);
                            break;
                        }
                    }
                    else
                    {
                        if (string.Compare(field.Name, description, true) == 0)
                        {
                            defaultValue = (T)field.GetValue(null);
                            break;
                        }
                    }
                }
            }

            return defaultValue;
        }

        /// <summary>
        /// 将枚举常数名称转换成枚举
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <param name="enumName">枚举常数名称</param>
        /// <returns>枚举</returns>
        public static T ParseEnumName<T>(this string enumName)
            where T : struct, IConvertible
        {
            return (T)Enum.Parse(typeof(T), enumName, true);
        }

        /// <summary>
        /// Gets the Description attribute.
        /// </summary>
        /// <param name="fieldInfo">The field information.</param>
        /// <returns>DescriptionAttribute数组</returns>
        /// 日期：2015-09-16 14:00
        /// 备注：
        private static DescriptionAttribute[] GetDescriptAttr(this FieldInfo fieldInfo)
        {
            if (fieldInfo != null)
            {
                return (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            }

            return null;
        }

        #endregion Methods
    }
}