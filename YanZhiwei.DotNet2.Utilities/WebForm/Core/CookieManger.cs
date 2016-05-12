namespace YanZhiwei.DotNet2.Utilities.WebForm.Core
{
    using System;
    using System.Web;

    /// <summary>
    /// cookie 帮助类
    /// </summary>
    public class CookieManger
    {
        /// <summary>
        /// 取Cookie
        /// </summary>
        /// <param name="name">键</param>
        /// <returns>值</returns>
        public static HttpCookie Get(string name)
        {
            return HttpContext.Current.Request.Cookies[name];
        }

        /// <summary>
        /// 取Cookie值
        /// </summary>
        /// <param name="name">键</param>
        /// <returns>值</returns>
        public static string GetValue(string name)
        {
            var httpCookie = Get(name);
            if (httpCookie != null)
                return httpCookie.Value;
            else
                return string.Empty;
        }

        /// <summary>
        /// 移除Cookie
        /// </summary>
        /// <param name="name">cookie键</param>
        public static void Remove(string name)
        {
            Remove(CookieManger.Get(name));
        }

        /// <summary>
        /// Removes the specified cookie.
        /// </summary>
        /// <param name="cookie">HttpCookie</param>
        public static void Remove(HttpCookie cookie)
        {
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now;
                Save(cookie, 0);
            }
        }

        /// <summary>
        /// 保存Cookie
        /// </summary>
        /// <param name="name">键</param>
        /// <param name="value">值</param>
        /// <param name="expiresHours">小时</param>
        public static void Save(string name, string value, int expiresHours = 0)
        {
            var httpCookie = Get(name);
            if (httpCookie == null)
                httpCookie = Set(name);

            httpCookie.Value = value;
            Save(httpCookie, expiresHours);
        }

        /// <summary>
        /// 保存Cookie
        /// </summary>
        /// <param name="cookie">HttpCookie</param>
        /// <param name="expiresHours">小时</param>
        public static void Save(HttpCookie cookie, int expiresHours)
        {
            string domain = FetchHelper.ServerDomain;
            string urlHost = HttpContext.Current.Request.Url.Host.ToLower();
            if (domain != urlHost)
                cookie.Domain = domain;

            if (expiresHours > 0)
                cookie.Expires = DateTime.Now.AddHours(expiresHours);

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        private static HttpCookie Set(string name)
        {
            return new HttpCookie(name);
        }
    }
}