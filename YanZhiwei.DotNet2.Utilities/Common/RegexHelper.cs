namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// 正则表达式帮助类
    /// </summary>
    public class RegexHelper
    {
        #region Methods

        /// <summary>
        /// 正则表达式匹配，匹配返回true
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <param name="pattern">模式字符串</param>
        /// <returns>是否匹配</returns>
        /// 日期：2015-10-10 9:10
        /// 备注：
        public static bool IsMatch(string input, string pattern)
        {
            return IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 正则表达式匹配，匹配返回true
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <param name="pattern">模式字符串</param>
        /// <param name="options">筛选条件</param>
        /// <returns>是否匹配</returns>
        public static bool IsMatch(string input, string pattern, RegexOptions options)
        {
            return Regex.IsMatch(input, pattern, options);
        }

        /// <summary>
        /// 正则表达式匹配，匹配返回true
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <param name="pattern">模式字符串</param>
        /// <param name="result">若匹配成功，则返回Match</param>
        /// <returns>匹配是否成功</returns>
        public static bool IsMatch(string input, string pattern, out Match result)
        {
            bool _checkResult = false;
            result = null;
            if (!string.IsNullOrEmpty(input))
            {
                Regex _regex = new Regex(pattern);
                result = _regex.Match(input);
                _checkResult = result.Success;
            }

            return _checkResult;
        }

        #endregion Methods
    }
}