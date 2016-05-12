namespace YanZhiwei.DotNet3._5.Utilities.WebForm
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web;

    /// <summary>
    /// URL帮助类
    /// </summary>
    public class UrlHelper
    {
        #region Methods

        /// <summary>
        /// 在url里添加参数，并返回添加后的url
        /// </summary>
        /// <param name="name">键</param>
        /// <param name="value">值</param>
        /// <returns>更新后链接</returns>
        public static string SetQueryString(string name, object value)
        {
            string[] _query = HttpContext.Current.Request.Url.Query.TrimStart('?').Split('&');
            string _newValue = name + '=' + value;

            if (_query.Any(i => i.IndexOf('=') > -1 && i.Substring(0, i.IndexOf('=')).Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                return Regex.Replace(HttpContext.Current.Request.RawUrl,
                    name + "=" + "[^&#]*",
                    _newValue, RegexOptions.IgnoreCase);
            }
            else
            {
                return HttpContext.Current.Request.RawUrl +
                    (HttpContext.Current.Request.RawUrl.IndexOf('?') > -1 ? '&' : '?') +
                    _newValue;
            }
        }

        #endregion Methods
    }
}