using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text.RegularExpressions;
using YanZhiwei.DotNet.Core.Cache;
using YanZhiwei.DotNet2.Utilities.Common;

namespace YanZhiwei.DotNet4.Core.CacheProvider
{
    /// <summary>
    /// RuntimeMemoryCache 辅助类
    /// </summary>
    /// 时间：2016-03-17 16:14
    /// 备注：
    public class RuntimeMemoryCacheProvider : ICacheProvider
    {
        private readonly ObjectCache objectCache;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// 时间：2016-03-17 16:04
        /// 备注：
        public RuntimeMemoryCacheProvider()
        {
            objectCache = MemoryCache.Default;
        }

        /// <summary>
        /// 清除缓存
        /// </summary>
        /// <param name="keyRegex">正则表达式</param>
        public virtual void Clear(string keyRegex)
        {
            List<string> _keys = new List<string>();
            List<string> _cacheKeys = objectCache.Select(m => m.Key).ToList();
            foreach (string key in _cacheKeys)
            {
                if (Regex.IsMatch(key, keyRegex, RegexOptions.IgnoreCase))
                    _keys.Add(key);
            }
            for (int i = 0; i < _keys.Count; i++)
            {
                objectCache.Remove(_keys[i]);
            }
        }

        /// <summary>
        /// 以键取值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>
        /// 值
        /// </returns>
        public virtual object Get(string key)
        {
            CheckedParamter(key);
            string _cacheKey = GetCacheKey(key);
            object _value = objectCache.Get(_cacheKey);
            if (_value == null)
            {
                return null;
            }
            DictionaryEntry _entry = (DictionaryEntry)_value;
            if (!key.Equals(_entry.Key))
            {
                return null;
            }
            return _entry.Value;
        }

        /// <summary>
        /// 从缓存中获取强类型数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <returns>获取的强类型数据</returns>
        public virtual T Get<T>(string key)
        {
            return (T)Get(key);
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">键</param>
        /// 时间：2015-12-31 13:32
        /// 备注：
        public virtual void Remove(string key)
        {
            CheckedParamter(key);
            string _cacheKey = GetCacheKey(key);
            objectCache.Remove(_cacheKey);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="minutes">分钟</param>
        /// <param name="isAbsoluteExpiration">是否绝对时间</param>
        /// <param name="onRemoveFacotry">委托</param>
        /// 时间：2015-12-31 13:12
        /// 备注：
        public virtual void Set(string key, object value, int minutes, bool isAbsoluteExpiration, Action<string, object, string> onRemoveFacotry)
        {
            CheckedParamter(key, value);
            string _cacheKey = GetCacheKey(key);
            DictionaryEntry _entry = new DictionaryEntry(key, value);
            CacheItemPolicy _policy = null;
            if (isAbsoluteExpiration)
            {
                _policy = new CacheItemPolicy() { AbsoluteExpiration = DateTime.Now.AddMinutes(minutes) };
            }
            else {
                _policy = new CacheItemPolicy() { SlidingExpiration = TimeSpan.FromMinutes(minutes) };
            }
            objectCache.Set(_cacheKey, _entry, _policy);
        }

        private void CheckedParamter(string key, object value)
        {
            ValidateHelper.Begin().NotNullOrEmpty(key, "缓存键").NotNull(value, "缓存数据");
        }

        private void CheckedParamter(string key)
        {
            ValidateHelper.Begin().NotNullOrEmpty(key, "缓存键");
        }

        private string GetCacheKey(string key)
        {
            return string.Concat(string.Empty, ":", key, "@", key.GetHashCode());
        }
    }
}