namespace YanZhiwei.DotNet2.Utilities.WebForm
{
    using Common;
    using System;
    using System.Text.RegularExpressions;
    using System.Web;

    /// <summary>
    /// URL 帮助类
    /// </summary>
    public static class FetchHelper
    {
        #region Methods


        /// <summary>
        /// 获取当前页面的主域，如www.GMS.com主域是GMS.com
        /// </summary>
        public static string ServerDomain
        {
            get
            {
                string _urlHost = HttpContext.Current.Request.Url.Host.ToLower();
                string[] _urlHostArray = _urlHost.Split(new char[] { '.' });
                if ((_urlHostArray.Length < 3) || CheckHelper.IsIp4Address(_urlHost))
                {
                    return _urlHost;
                }
                string _urlHost2 = _urlHost.Remove(0, _urlHost.IndexOf(".") + 1);
                if ((_urlHost2.StartsWith("com.") || _urlHost2.StartsWith("net.")) || (_urlHost2.StartsWith("org.") || _urlHost2.StartsWith("gov.")))
                {
                    return _urlHost;
                }
                return _urlHost2;
            }
        }

        /// <summary>
        /// 获取访问用户的IP
        /// </summary>
        public static string UserIp
        {
            get
            {
                string _result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                switch (_result)
                {
                    case null:
                    case "":
                        _result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                        break;
                }
                if (!CheckHelper.IsIp4Address(_result))
                {
                    return "Unknown";
                }
                return _result;
            }
        }

       

       

        /// <summary>
        /// 取得网站根目录的物理路径
        /// </summary>
        /// <returns>网站根目录的物理路径</returns>
        public static string GetRootPath()
        {
            string _appPath = string.Empty;
            HttpContext _httpCurrent = HttpContext.Current;
            if (_httpCurrent != null)
            {
                _appPath = _httpCurrent.Server.MapPath("~");
            }
            else
            {
                _appPath = AppDomain.CurrentDomain.BaseDirectory;
                if (Regex.Match(_appPath, @"\\$", RegexOptions.Compiled).Success)
                {
                    _appPath = _appPath.Substring(0, _appPath.Length - 1);
                }
            }

            return _appPath;
        }

        /// <summary>
        /// 取得网站的根目录的URL
        /// </summary>
        /// <returns>网站的根目录的URL</returns>
        public static string GetRootURI()
        {
            string _appPath = string.Empty;
            HttpContext _httpCurrent = HttpContext.Current;
            HttpRequest _req;
            if (_httpCurrent != null)
            {
                _req = _httpCurrent.Request;
                string _urlAuthority = _req.Url.GetLeftPart(UriPartial.Authority);
                if (_req.ApplicationPath == null || _req.ApplicationPath == "/")
                {
                    _appPath = _urlAuthority;
                }
                else
                {
                    _appPath = _urlAuthority + _req.ApplicationPath;
                }
            }

            return _appPath;
        }

        /// <summary>
        /// 取得网站的根目录的URL
        /// </summary>
        /// <param name="reguest">HttpRequest</param>
        /// <returns>网站的根目录的URL</returns>
        public static string GetRootURI(HttpRequest reguest)
        {
            string _appPath = string.Empty;
            if (reguest != null)
            {
                string _urlAuthority = reguest.Url.GetLeftPart(UriPartial.Authority);
                if (reguest.ApplicationPath == null || reguest.ApplicationPath == "/")
                {
                    _appPath = _urlAuthority;
                }
                else
                {
                    _appPath = _urlAuthority + reguest.ApplicationPath;
                }
            }

            return _appPath;
        }

        /// <summary>
        /// 获取表单Post过来的值
        /// </summary>
        /// <param name="name">参数</param>
        /// <returns>参数数值</returns>
        public static string Post(string name)
        {
            string _value = HttpContext.Current.Request.Form[name];
            return ((_value == null) ? string.Empty : _value.Trim());
        }

        
        #endregion Methods
    }
}