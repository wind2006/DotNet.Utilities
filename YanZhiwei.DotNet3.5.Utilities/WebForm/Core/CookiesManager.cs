namespace YanZhiwei.DotNet3._5.Utilities.WebForm.Core
{
    using Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using YanZhiwei.DotNet3._5.Interfaces;

    /// <summary>
    /// cookie 辅助类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// 时间：2015-12-18 11:33
    /// 备注：
    public class CookiesManager<T> : IHttpStorageObject<T>
    {
        #region Fields

        /// <summary>
        /// 锁对象
        /// </summary>
        private static readonly object lockObj = new object();

        /// <summary>
        /// CookiesManager实例
        /// </summary>
        private static CookiesManager<T> instance = null;

        #endregion Fields

        #region Indexers

        /// <summary>
        /// 索引取值
        /// </summary>
        public override T this[string key]
        {
            get { return Get(key); }
        }

        #endregion Indexers

        #region Methods

        /// <summary>
        /// 获取实例 （单例模式）
        /// </summary>
        /// <returns>CookiesManager</returns>
        public static CookiesManager<T> GetInstance()
        {
            if (instance == null)
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new CookiesManager<T>();
                    }
                }
            }
            return instance;
        }

        /// <summary>
        /// 添加cookies ,注意value最大4K (默认1天)
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public override void Add(string key, T value)
        {
            Add(key, value, Day);
        }

        /// <summary>
        /// 添加cookies ,注意value最大4K
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="cookiesDurationInSeconds">有效时间单位秒</param>
        public void Add(string key, T value, int cookiesDurationInSeconds)
        {
            HttpResponse _response = HttpContext.Current.Response;
            if (_response != null)
            {
                HttpCookie _cookie = _response.Cookies[key];
                if (_cookie != null)
                {
                    if (typeof(T) == typeof(string))
                    {
                        string _setValue = value.ToString();
                        Add(key, cookiesDurationInSeconds, _cookie, _setValue, _response);
                    }
                    else
                    {
                        string _setValue = SerializationHelper.JsonSerialize<T>(value);
                        Add(key, cookiesDurationInSeconds, _cookie, _setValue, _response);
                    }
                }
            }
        }

        /// <summary>
        /// 是否包含该键
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>是否包含</returns>
        public override bool ContainsKey(string key)
        {
            return Get(key) != null;
        }

        /// <summary>
        /// 按键取值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>对象</returns>
        public override T Get(string key)
        {
            string _value = string.Empty;
            if (context.Request.Cookies[key] != null)
                _value = context.Request.Cookies[key].Value;
            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(_value, typeof(T));
            }
            else
            {
                return (T)SerializationHelper.JsonDeserialize(_value);
            }
        }

        /// <summary>
        /// 获取所有键集合
        /// </summary>
        /// <returns>集合</returns>
        public override IEnumerable<string> GetAllKey()
        {
            List<string> _allKeyList = context.Request.Cookies.AllKeys.ToList();
            foreach (var key in _allKeyList)
            {
                yield return key;
            }
        }

        /// <summary>
        /// 按键移除
        /// </summary>
        /// <param name="key">The key.</param>
        public override void Remove(string key)
        {
            HttpRequest _request = HttpContext.Current.Request;
            if (_request != null)
            {
                HttpCookie _cookie = _request.Cookies[key];
                _cookie.Expires = DateTime.Now.AddDays(-1);
                if (_cookie != null)
                {
                    if (!string.IsNullOrEmpty(key) && _cookie.HasKeys)
                        _cookie.Values.Remove(key);
                    else
                        _request.Cookies.Remove(key);
                }
            }
        }

        /// <summary>
        /// 移除所有
        /// </summary>
        public override void RemoveAll()
        {
            foreach (var key in GetAllKey())
            {
                Remove(key);
            }
        }

        /// <summary>
        /// 移除所有
        /// </summary>
        /// <param name="keySelector">选择器</param>
        public override void RemoveAll(Func<string, bool> keySelector)
        {
            List<string> _removeList = GetAllKey().Where(keySelector).ToList();
            foreach (var key in _removeList)
            {
                Remove(key);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="cookiesDurationInSeconds">过期时间，单位秒</param>
        /// <param name="cookie">HttpCookie</param>
        /// <param name="setValue">值</param>
        /// <param name="response">HttpResponse</param>
        private void Add(string key, int cookiesDurationInSeconds, HttpCookie cookie, string setValue, HttpResponse response)
        {
            if (!string.IsNullOrEmpty(key) && cookie.HasKeys)
                cookie.Values.Set(key, setValue);
            else
                if (!string.IsNullOrEmpty(setValue))
                cookie.Value = setValue;
            if (cookiesDurationInSeconds > 0)
                cookie.Expires = DateTime.Now.AddSeconds(cookiesDurationInSeconds);
            response.SetCookie(cookie);
        }

        #endregion Methods
    }
}