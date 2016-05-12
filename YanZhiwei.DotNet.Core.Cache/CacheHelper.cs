namespace YanZhiwei.DotNet.Core.Cache
{
    using System;
    using System.Collections;
    using System.Text.RegularExpressions;
    using System.Web;

    using YanZhiwei.DotNet.Core.Cache.Models;

    /// <summary>
    /// 缓存帮助
    /// </summary>
    public class CacheHelper
    {
        #region Constructors

        ///// <summary>
        ///// 构造函数
        ///// </summary>
        ///// <param name="cacheConfig">CacheConfig</param>
        ///// 时间：2015-12-31 15:54
        ///// 备注：
        //public CacheHelper(CacheConfig cacheConfig)
        //{
        //    ValidateHelper.Begin().NotNull(cacheConfig, "缓存配置项");
        //    CacheConfigContext.CacheConfig = cacheConfig;
        //}

        #endregion Constructors

        #region Methods

        /// <summary>
        /// 根据正则表达式移除缓存
        /// </summary>
        /// <param name="keyRegex">缓存键的正则表达式</param>
        /// <param name="moduleRegex">模块正则表达式</param>
        /// 时间：2015-12-31 15:56
        /// 备注：
        public static void Clear(string keyRegex = ".*", string moduleRegex = ".*")
        {
            if (!Regex.IsMatch(CacheConfigContext.ModuleName, moduleRegex, RegexOptions.IgnoreCase))
                return;

            foreach (var cacheProviders in CacheConfigContext.CacheProviders.Values)
                cacheProviders.Clear(keyRegex);
        }

        /// <summary>
        /// 以键取值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        /// 时间：2015-12-31 15:55
        /// 备注：
        public static object Get(string key)
        {
            WrapCacheConfigItem _cacheConfig = CacheConfigContext.GetCurrentWrapCacheConfigItem(key);
            return _cacheConfig.CacheProvider.Get(key);
        }

        /// <summary>
        /// 如果缓存里没有，则取数据然后缓存起来
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="key">键</param>
        /// <param name="getRealDataFactory">委托</param>
        /// <returns></returns>
        /// 时间：2015-12-31 15:57
        /// 备注：
        public static T Get<T>(string key, Func<T> getRealDataFactory)
        {
            Func<T> _getDataFromCache = new Func<T>(() =>
            {
                T _data = default(T);
                object _cacheData = Get(key);
                if (_cacheData == null)
                {
                    _data = getRealDataFactory();

                    if (_data != null)
                        Set(key, _data);
                }
                else
                {
                    _data = (T)_cacheData;
                }

                return _data;
            });

            return GetItem<T>(key, _getDataFromCache);
        }

        /// <summary>
        /// 如果缓存里没有，则取数据然后缓存起来
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="key">Key.</param>
        /// <param name="id">分支Key</param>
        /// <param name="getRealDataFactory">委托</param>
        /// <returns>泛型</returns>
        /// 时间：2015-12-31 15:59
        /// 备注：
        public static T Get<T>(string key, int id, Func<int, T> getRealDataFactory)
        {
            return Get<T, int>(key, id, getRealDataFactory);
        }

        /// <summary>
        /// 如果缓存里没有，则取数据然后缓存起来
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="key">Key.</param>
        /// <param name="id">分支Key</param>
        /// <param name="getRealDataFactory">委托</param>
        /// <returns>泛型</returns>
        /// 时间：2015-12-31 15:59
        /// 备注：
        public static T Get<T>(string key, string id, Func<string, T> getRealDataFactory)
        {
            return Get<T, string>(key, id, getRealDataFactory);
        }

        /// <summary>
        /// 如果缓存里没有，则取数据然后缓存起来
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="key">Key.</param>
        /// <param name="branchKey">分支Key</param>
        /// <param name="getRealDataFactory">委托</param>
        /// <returns>泛型</returns>
        /// 时间：2015-12-31 15:59
        /// 备注：
        public static T Get<T>(string key, string branchKey, Func<T> getRealDataFactory)
        {
            return Get<T, string>(key, branchKey, id => getRealDataFactory());
        }

        /// <summary>
        /// 如果缓存里没有，则取数据然后缓存起来
        /// </summary>
        /// <typeparam name="F">泛型</typeparam>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="key">Key.</param>
        /// <param name="id">分支Key</param>
        /// <param name="getRealDataFactory">委托</param>
        /// <returns>泛型</returns>
        /// 时间：2015-12-31 15:59
        /// 备注：
        public static F Get<F, T>(string key, T id, Func<T, F> getRealDataFactory)
        {
            key = string.Format("{0}_{1}", key, id);

            Func<F> _getDataFromCache = new Func<F>(() =>
            {
                F _data = default(F);
                object _cacheData = Get(key);
                if (_cacheData == null)
                {
                    _data = getRealDataFactory(id);

                    if (_data != null)
                        Set(key, _data);
                }
                else
                {
                    _data = (F)_cacheData;
                }

                return _data;
            });

            return GetItem<F>(key, _getDataFromCache);
        }

        /// <summary>
        /// 如果上下文HttpContext.Current.Items里没有，则取数据然后加入Items，在页面生命周期内有效，
        /// 适合页面生命周期，页面载入后就被移除，而非HttpContext.Cache在整个应用程序都有效
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="key">Key</param>
        /// <param name="getRealDataFactory">委托</param>
        /// <returns>泛型</returns>
        /// 时间：2015-12-31 16:10
        /// 备注：
        public static T GetItem<T>(string key, Func<T> getRealDataFactory)
        {
            if (HttpContext.Current == null)
                return getRealDataFactory();

            IDictionary _httpContextItems = HttpContext.Current.Items;
            if (_httpContextItems.Contains(key))
            {
                return (T)_httpContextItems[key];
            }
            else
            {
                var data = getRealDataFactory();
                if (data != null)
                    _httpContextItems[key] = data;
                return data;
            }
        }

        /// <summary>
        /// 如果上下文HttpContext.Current.Items里没有，则取数据然后加入Items，在页面生命周期内有效，
        /// 适合页面生命周期，页面载入后就被移除，而非HttpContext.Cache在整个应用程序都有效
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>泛型</returns>
        /// 时间：2015-12-31 16:12
        /// 备注：
        public static T GetItem<T>()
            where T : new()
        {
            return GetItem<T>(typeof(T).ToString(), () => new T());
        }

        /// <summary>
        /// 如果上下文HttpContext.Current.Items里没有，则取数据然后加入Items，在页面生命周期内有效，
        /// 适合页面生命周期，页面载入后就被移除，而非HttpContext.Cache在整个应用程序都有效
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="getRealDataFactory">委托</param>
        /// <returns>泛型</returns>
        /// 时间：2015-12-31 16:12
        /// 备注：
        public static T GetItem<T>(Func<T> getRealDataFactory)
        {
            return GetItem<T>(typeof(T).ToString(), getRealDataFactory);
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key">键</param>
        /// 时间：2015-12-31 15:56
        /// 备注：
        public static void Remove(string key)
        {
            WrapCacheConfigItem _cacheConfig = CacheConfigContext.GetCurrentWrapCacheConfigItem(key);
            _cacheConfig.CacheProvider.Remove(key);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// 时间：2015-12-31 15:55
        /// 备注：
        public static void Set(string key, object value)
        {
            WrapCacheConfigItem _cacheConfig = CacheConfigContext.GetCurrentWrapCacheConfigItem(key);
            _cacheConfig.CacheProvider.Set(key, value, _cacheConfig.CacheConfigItem.Minitus, _cacheConfig.CacheConfigItem.IsAbsoluteExpiration, null);
        }

        #endregion Methods
    }
}