namespace YanZhiwei.DotNet2.Utilities.WinForm
{
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    /// <summary>
    ///  WebBrowser 帮助类
    /// </summary>
    public static class WebBrowserHelper
    {
        #region Methods

        /// <summary>
        /// 从WebBrowser中获取CookieContainer
        /// </summary>
        /// <param name="webBrowser">WebBrowser对象</param>
        /// <returns>CookieContainer</returns>
        public static CookieContainer GetCookieContainer(this WebBrowser webBrowser)
        {
            CookieContainer _cookieContainer = new CookieContainer();

            string _cookieString = webBrowser.Document.Cookie;
            if (string.IsNullOrEmpty(_cookieString))
            {
                return _cookieContainer;
            }

            string[] _cookies = _cookieString.Split(';');
            if (_cookies == null)
            {
                return _cookieContainer;
            }

            foreach (string cookieString in _cookies)
            {
                string[] _cookieNameValue = cookieString.Split('=');
                if (_cookieNameValue.Length != 2)
                {
                    continue;
                }

                Cookie _cookie = new Cookie(_cookieNameValue[0].Trim().ToString(), _cookieNameValue[1].Trim().ToString());
                _cookieContainer.Add(_cookie);
            }

            return _cookieContainer;
        }

        /// <summary>
        /// WebBrowser添加cookie
        /// </summary>
        /// <param name="webBrowser">WebBrowser</param>
        /// <param name="url">url</param>
        /// <param name="cookie">cookie</param>
        public static void InsertCookie(this WebBrowser webBrowser, string url, Cookie cookie)
        {
            InternetSetCookie(url, cookie.Name, cookie.Value);
        }

        /// <summary>
        /// Internets the set cookie.
        /// </summary>
        /// <param name="urlName">Name of the URL.</param>
        /// <param name="cookieName">Name of the cookie.</param>
        /// <param name="cookieData">The cookie data.</param>
        /// <returns>设置是否成功</returns>
        /// 日期：2015-10-13 13:44
        /// 备注：
        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetCookie(string urlName, string cookieName, string cookieData);

        #endregion Methods
    }
}