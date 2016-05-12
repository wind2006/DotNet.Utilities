namespace YanZhiwei.DotNet2.Utilities.WebForm
{
    using System;
    using System.Web;

    using YanZhiwei.DotNet2.Utilities.Common;

    /// <summary>
    /// 超链接帮助类
    /// </summary>
    /// 时间：2016-03-04 10:27
    /// 备注：
    public static class HyperlinkHelper
    {
        #region Properties

        /// <summary>
        /// 获取当前页面的Url
        /// </summary>
        public static string CurrentUrl
        {
            get
            {
                return HttpContext.Current.Request.Url.ToString();
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 添加超链接参数
        /// </summary>
        /// <param name="url">超链接</param>
        /// <param name="paramName">参数键</param>
        /// <param name="value">参数值</param>
        /// <returns>新的连接</returns>
        public static string AddParameter(this string url, string paramName, string value)
        {
            ValidateHelper.Begin().NotNullOrEmpty(paramName, "超链接参数名称").NotNullOrEmpty(value, "超链接参数名称数值");
            Uri _hyperlinkUri = new Uri(url);
            if (string.IsNullOrEmpty(_hyperlinkUri.Query))
            {
                string _urlEncode = HttpContext.Current.Server.UrlEncode(value);
                return string.Concat(url, "?" + paramName + "=" + _urlEncode);
            }
            else
            {
                string _urlEncode = HttpContext.Current.Server.UrlEncode(value);
                return string.Concat(url, "&" + paramName + "=" + _urlEncode);
            }
        }

        /// <summary>
        /// 获取Url后面的值
        /// </summary>
        /// <param name="name">参数</param>
        /// <returns>参数数值</returns>
        public static string Get(string name)
        {
            string _value = HttpContext.Current.Request.QueryString[name];
            return ((_value == null) ? string.Empty : _value.Trim());
        }

        /// <summary>
        /// 获取Url后面的值，如.....aspx?productid=2将获取到"2"
        /// </summary>
        /// <param name="name">参数</param>
        /// <returns>参数数值</returns>
        public static int GetQueryId(string name)
        {
            int _id = 0;
            int.TryParse(Get(name), out _id);
            return _id;
        }

        /// <summary>
        /// 更新超链接参数
        /// </summary>
        /// <param name="url">链接</param>
        /// <param name="paramName">参数键</param>
        /// <param name="value">参数值</param>
        /// <returns>更新后的链接</returns>
        public static string UpdateParameter(this string url, string paramName, string value)
        {
            ValidateHelper.Begin().NotNullOrEmpty(url, "超链接").IsURL(url, "超链接").NotNullOrEmpty(paramName, "超链接参数名称").NotNullOrEmpty(value, "超链接参数名称数值");
            string _keyWord = paramName + "=";
            int _index = url.IndexOf(_keyWord) + _keyWord.Length;
            int _index1 = url.IndexOf("&", _index);
            if (_index1 == -1)
            {
                url = url.Remove(_index, url.Length - _index);
                url = string.Concat(url, value);
                return url;
            }

            url = url.Remove(_index, _index1 - _index);
            url = url.Insert(_index, value);
            return url;
        }

        #endregion Methods
    }
}