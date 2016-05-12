namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;

    using YanZhiwei.DotNet2.Utilities.Core;
    using YanZhiwei.DotNet2.Utilities.Models;

    /// <summary>
    /// 参数验证帮助类
    /// </summary>
    public static class ValidateHelper
    {
        #region Methods

        /// <summary>
        /// 验证初始化
        /// </summary>
        /// <returns>Validation</returns>
        public static Validation Begin()
        {
            return null;
        }

        /// <summary>
        /// 需要验证的正则表达式
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="checkFactory">委托</param>
        /// <param name="pattern">正则表达式</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation</returns>
        public static Validation Check(this Validation validation, Func<bool> checkFactory, string pattern, string argumentName)
        {
            return Check<ArgumentException>(validation, checkFactory, string.Format(Resource.ParameterCheck_Match2, argumentName));
        }

        /// <summary>
        /// 检查指定路径的文件夹必须存在，否则抛出<see cref="DirectoryNotFoundException"/>异常。
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">判断数据</param>
        /// <param name="paramName">参数名称。</param>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        /// <exception cref="DirectoryNotFoundException">DirectoryNotFoundException</exception>
        /// <returns>Validation</returns>
        public static Validation CheckDirectoryExists(this Validation validation, string data, string paramName)
        {
            return Check<DirectoryNotFoundException>(validation, () => Directory.Exists(data), string.Format(Resource.ParameterCheck_DirectoryNotExists, data));
        }

        /// <summary>
        /// 检查指定路径的文件必须存在，否则抛出<see cref="FileNotFoundException"/>异常。
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">参数</param>
        /// <param name="paramName">参数名称。</param>
        /// <exception cref="ArgumentNullException">当文件路径为null时</exception>
        /// <exception cref="FileNotFoundException">当文件路径不存在时</exception>
        /// <returns>Validation</returns>
        public static Validation CheckFileExists(this Validation validation, string data, string paramName)
        {
            return Check<FileNotFoundException>(validation, () => File.Exists(data), string.Format(Resource.ParameterCheck_FileNotExists, data));
        }

        /// <summary>
        /// 检查参数必须大于[或可等于，参数canEqual]指定值，否则抛出<see cref="ArgumentOutOfRangeException"/>异常。
        /// </summary>
        /// <typeparam name="T">参数类型。</typeparam>
        /// <param name="validation">Validation</param>
        /// <param name="value">判断数据</param>
        /// <param name="paramName">参数名称。</param>
        /// <param name="target">要比较的值。</param>
        /// <param name="canEqual">是否可等于。</param>
        /// <exception cref="ArgumentOutOfRangeException">ArgumentOutOfRangeException</exception>
        /// <returns>Validation</returns>
        public static Validation CheckGreaterThan<T>(this Validation validation, T value, string paramName, T target, bool canEqual)
            where T : IComparable<T>
        {
            // bool flag = canEqual ? value.CompareTo(target) >= 0 : value.CompareTo(target) > 0;
            string _format = canEqual ? Resource.ParameterCheck_NotGreaterThanOrEqual : Resource.ParameterCheck_NotGreaterThan;
            return Check<ArgumentOutOfRangeException>(validation, () => canEqual ? value.CompareTo(target) >= 0 : value.CompareTo(target) > 0, string.Format(_format, paramName, target));
        }

        /// <summary>
        /// 检查参数必须小于[或可等于，参数canEqual]指定值，否则抛出<see cref="ArgumentOutOfRangeException"/>异常。
        /// </summary>
        /// <typeparam name="T">参数类型。</typeparam>
        /// <param name="validation">Validation</param>
        /// <param name="value">判断数据</param>
        /// <param name="paramName">参数名称。</param>
        /// <param name="target">要比较的值。</param>
        /// <param name="canEqual">是否可等于。</param>
        /// <exception cref="ArgumentOutOfRangeException">ArgumentOutOfRangeException</exception>
        /// <returns>Validation</returns>
        public static Validation CheckLessThan<T>(this Validation validation, T value, string paramName, T target, bool canEqual)
            where T : IComparable<T>
        {
            string _format = canEqual ? Resource.ParameterCheck_NotLessThanOrEqual : Resource.ParameterCheck_NotLessThan;
            return Check<ArgumentOutOfRangeException>(validation, () => canEqual ? value.CompareTo(target) <= 0 : value.CompareTo(target) < 0, string.Format(_format, paramName, target));
        }

        /// <summary>
        /// 验证是否在范围内
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">输入项</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation</returns>
        public static Validation InRange(this Validation validation, string data, int min, int max, string argumentName)
        {
            return Check<ArgumentOutOfRangeException>(validation, () => CheckHelper.InRange(data, min, max), string.Format(Resource.ParameterCheck_Between, min, max));
        }

        /// <summary>
        /// 是否是中文
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">中文</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation</returns>
        public static Validation IsChinses(this Validation validation, string data, string argumentName)
        {
            return Check(validation, () => CheckHelper.IsChinses(data), RegexPattern.ChineseCheck, argumentName);
        }

        /// <summary>
        /// 是否是电子邮箱
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="email">需要验证的邮箱</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation</returns>
        public static Validation IsEmail(this Validation validation, string email, string argumentName)
        {
            return Check(validation, () => CheckHelper.IsEmail(email), RegexPattern.EmailCheck, argumentName);
        }

        /// <summary>
        /// 是否是文件路径
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">路径</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation</returns>
        public static Validation IsFilePath(this Validation validation, string data, string argumentName)
        {
            return Check(validation, () => CheckHelper.IsFilePath(data), RegexPattern.URLCheck, argumentName);
        }

        /// <summary>
        /// 是否是十六进制字符串
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">验证数据</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation</returns>
        public static Validation IsHexString(this Validation validation, string data, string argumentName)
        {
            return Check(validation, () => CheckHelper.IsHexString(data), RegexPattern.HexStringCheck, argumentName);
        }

        /// <summary>
        /// 是否是身份证号码
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">验证数据</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation</returns>
        public static Validation IsIdCard(this Validation validation, string data, string argumentName)
        {
            return Check(validation, () => CheckHelper.IsIdCard(data), RegexPattern.IdCardCheck, argumentName);
        }

        /// <summary>
        /// 是否是整数
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">需要检测的字符串</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation</returns>
        public static Validation IsInt(this Validation validation, string data, string argumentName)
        {
            return Check(validation, () => CheckHelper.IsInt(data), RegexPattern.IntCheck, argumentName);
        }

        /// <summary>
        /// 是否是IP
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">需要检测到IP</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation</returns>
        public static Validation IsIp(this Validation validation, string data, string argumentName)
        {
            return Check(validation, () => CheckHelper.IsIp4Address(data), RegexPattern.IpCheck, argumentName);
        }

        /// <summary>
        /// 是否是数字
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">需要检测的字符串</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation</returns>
        public static Validation IsNumber(this Validation validation, string data, string argumentName)
        {
            return Check(validation, () => CheckHelper.IsNumber(data), RegexPattern.NumberCheck, argumentName);
        }

        /// <summary>
        /// 是否是合法端口
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">参数值</param>
        /// <param name="paramName">参数名称</param>
        /// <returns>Validation</returns>
        public static Validation IsPort(this Validation validation, string data, string paramName)
        {
            return Check<ArgumentException>(validation, () => CheckHelper.IsValidPort(data), string.Format(Resource.ParameterCheck_Port, paramName));
        }

        /// <summary>
        /// 是否是邮政编码
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">邮政编码</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation</returns>
        public static Validation IsPoseCode(this Validation validation, string data, string argumentName)
        {
            return Check(validation, () => CheckHelper.IsPoseCode(data), RegexPattern.PostCodeCheck, argumentName);
        }

        /// <summary>
        /// 判断字符串是否是要求的长度
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="input">验证的字符串</param>
        /// <param name="requireLength">要求的长度</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation</returns>
        public static Validation IsRequireLen(this Validation validation, string input, int requireLength, string argumentName)
        {
            return Check<ArgumentException>(
               validation,
               () => input.Length == requireLength,
               string.Format(Resource.ParameterCheck_StringLength, argumentName, requireLength));
        }

        /// <summary>
        /// 判断类型是否能序列化
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">输入项</param>
        /// <returns>Validation</returns>
        /// 时间：2016-01-14 9:57
        /// 备注：
        public static Validation IsSerializable(this Validation validation, object data)
        {
            return Check<ArgumentException>(validation, () => data.GetType().IsSerializable, string.Format("该参数类型{0}不能序列化！", data.GetType().FullName));
        }

        /// <summary>
        /// 是否是URL
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">url</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation</returns>
        public static Validation IsURL(this Validation validation, string data, string argumentName)
        {
            return Check(validation, () => CheckHelper.IsURL(data), RegexPattern.URLCheck, argumentName);
        }

        /// <summary>
        /// 验证参数不能等于某个值
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">输入项</param>
        /// <param name="equalObj">比较项</param>
        /// <param name="paramName">参数名称</param>
        /// <returns>Validation</returns>
        public static Validation NotEqual(this Validation validation, object data, object equalObj, string paramName)
        {
            return Check<ArgumentException>(validation, () => data != equalObj, string.Format(Resource.ParameterCheck_NotEqual, paramName, data));
        }
        /// <summary>
        /// 验证非空
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">输入项</param>
        /// <param name="paramName">参数名称</param>
        /// <returns>Validation</returns>
        public static Validation NotNull(this Validation validation, object data, string paramName)
        {
            return Check<ArgumentNullException>(validation, () => CheckHelper.NotNull(data), string.Format(Resource.ParameterCheck_NotNull, paramName));
        }

        /// <summary>
        /// 不能为空或者NULL验证
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="input">输入项</param>
        /// <param name="paramName">参数名称</param>
        /// <returns>Validation</returns>
        public static Validation NotNullOrEmpty(this Validation validation, string input, string paramName)
        {
            return Check<ArgumentNullException>(validation, () => !string.IsNullOrEmpty(input), string.Format(Resource.ParameterCheck_NotNullOrEmpty_String, paramName));
        }

        /// <summary>
        /// 需要验证的正则表达式
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="input">需要匹配的输入项</param>
        /// <param name="pattern">正则表达式</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation</returns>
        public static Validation RegexMatch(this Validation validation, string input, string pattern, string argumentName)
        {
            return Check<ArgumentException>(validation, () => Regex.IsMatch(input, pattern), string.Format(Resource.ParameterCheck_Match, input, argumentName));
        }

        /// <summary>
        /// Checks the specified filter method.
        /// </summary>
        /// <typeparam name="TException">The type of the exception.</typeparam>
        /// <param name="validation">The validation.</param>
        /// <param name="filterMethod">The filter method.</param>
        /// <param name="message">The message.</param>
        /// <returns>Validation</returns>
        private static Validation Check<TException>(this Validation validation, Func<bool> filterMethod, string message)
            where TException : Exception
        {
            if (filterMethod())
            {
                return validation ?? new Validation() { IsValid = true };
            }
            else
            {
                TException _exception = (TException)Activator.CreateInstance(typeof(TException), message);
                throw _exception;
            }
        }

        #endregion Methods
    }
}