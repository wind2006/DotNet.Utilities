namespace YanZhiwei.DotNet.Core.Cache
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Caching;

    using YanZhiwei.DotNet2.Utilities.Common;

    /// <summary>
    /// 运行时内存缓存
    /// </summary>
    public class RuntimeMemoryCache : CacheBase
    {
        #region Fields

        private readonly string cacheRegion;
        private readonly ObjectCache objectCache;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 初始化一个<see cref="RuntimeMemoryCache"/>类型的新实例
        /// </summary>
        public RuntimeMemoryCache(string region)
        {
            cacheRegion = region;
            objectCache = MemoryCache.Default;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 获取 缓存区域名称，可作为缓存键标识，给缓存管理带来便利
        /// </summary>
        public override string Region
        {
            get { return cacheRegion; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 清空缓存
        /// </summary>
        public override void Clear()
        {
            string _token = cacheRegion + ":";
            List<string> _cacheKeys = objectCache.Where(m => m.Key.StartsWith(_token)).Select(m => m.Key).ToList();
            foreach (string cacheKey in _cacheKeys)
            {
                objectCache.Remove(cacheKey);
            }
        }

        /// <summary>
        /// 从缓存中获取数据
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>获取的数据</returns>
        public override object Get(string key)
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
        public override T Get<T>(string key)
        {
            return (T)Get(key);
        }

        /// <summary>
        /// 获取当前缓存对象中的所有数据
        /// </summary>
        /// <returns>所有数据的集合</returns>
        public override IEnumerable<object> GetAll()
        {
            string _token = string.Concat(cacheRegion, ":");
            return objectCache.Where(m => m.Key.StartsWith(_token)).Select(m => m.Value).Cast<DictionaryEntry>().Select(m => m.Value);
        }

        /// <summary>
        /// 获取当前缓存中的所有数据
        /// </summary>
        /// <typeparam name="T">项数据类型</typeparam>
        /// <returns>所有数据的集合</returns>
        public override IEnumerable<T> GetAll<T>()
        {
            return GetAll().Cast<T>();
        }

        /// <summary>
        /// 移除指定键的缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        public override void Remove(string key)
        {
            CheckedParamter(key);
            string _cacheKey = GetCacheKey(key);
            objectCache.Remove(_cacheKey);
        }

        /// <summary>
        /// 使用默认配置添加或替换缓存项
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存数据</param>
        public override void Set(string key, object value)
        {
            CheckedParamter(key, value);
            string _cacheKey = GetCacheKey(key);
            DictionaryEntry _entry = new DictionaryEntry(key, value);
            CacheItemPolicy _policy = new CacheItemPolicy();
            objectCache.Set(_cacheKey, _entry, _policy);
        }

        /// <summary>
        /// 添加或替换缓存项并设置绝对过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存数据</param>
        /// <param name="absoluteExpiration">绝对过期时间，过了这个时间点，缓存即过期</param>
        public override void Set(string key, object value, DateTime absoluteExpiration)
        {
            CheckedParamter(key, value);
            string _cacheKey = GetCacheKey(key);
            DictionaryEntry entry = new DictionaryEntry(key, value);
            CacheItemPolicy policy = new CacheItemPolicy() { AbsoluteExpiration = absoluteExpiration };
            objectCache.Set(_cacheKey, entry, policy);
        }

        /// <summary>
        /// 添加或替换缓存项并设置相对过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存数据</param>
        /// <param name="slidingExpiration">滑动过期时间，在此时间内访问缓存，缓存将继续有效</param>
        public override void Set(string key, object value, TimeSpan slidingExpiration)
        {
            CheckedParamter(key, value);
            string _cacheKey = GetCacheKey(key);
            DictionaryEntry _entry = new DictionaryEntry(key, value);
            CacheItemPolicy _policy = new CacheItemPolicy() { SlidingExpiration = slidingExpiration };
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
            return string.Concat(cacheRegion, ":", key, "@", key.GetHashCode());
        }

        #endregion Methods
    }
}