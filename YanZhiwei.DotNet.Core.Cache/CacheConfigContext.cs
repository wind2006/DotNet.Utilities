namespace YanZhiwei.DotNet.Core.Cache
{
    using DotNet2.Utilities.Common;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    using YanZhiwei.DotNet.Core.Cache.Models;
    using YanZhiwei.DotNet.Core.Model;

    /// <summary>
    /// 缓存配置上下文
    /// </summary>
    /// 时间：2015-12-31 15:50
    /// 备注：
    public class CacheConfigContext
    {
        #region Fields

        /// <summary>
        /// 读写锁对象
        /// </summary>
        /// 时间：2015-12-31 15:50
        /// 备注：
        private static readonly object lockObj = new object();

        /// <summary>
        /// 首次加载所有的CacheProviders
        /// </summary>
        private static Dictionary<string, ICacheProvider> cacheProviders;

        /// <summary>
        /// 得到网站项目的入口程序模块名名字，用于CacheConfigItem.ModuleRegex
        /// </summary>
        /// <returns></returns>
        private static string moduleName;

        /// <summary>
        /// 根据Key，通过正则匹配从WrapCacheConfigItems里帅选出符合的缓存项目，然后通过字典缓存起来
        /// </summary>
        private static Dictionary<string, WrapCacheConfigItem> wrapCacheConfigItemDic;

        /// <summary>
        /// 首次加载所有的CacheConfig, wrapCacheConfigItem相对于cacheConfigItem把providername通过反射还原成了具体provider类
        /// </summary>
        private static List<WrapCacheConfigItem> wrapCacheConfigItems;

        #endregion Fields

        #region Properties

        public static string ModuleName
        {
            get
            {
                if (moduleName == null)
                {
                    lock (lockObj)
                    {
                        if (moduleName == null)
                        {
                            Assembly _entryAssembly = Assembly.GetEntryAssembly();

                            if (_entryAssembly != null)
                            {
                                moduleName = _entryAssembly.FullName;
                            }
                            else
                            {
                                moduleName = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Name;
                            }
                        }
                    }
                }

                return moduleName;
            }
        }

        /// <summary>
        /// CacheConfig
        /// </summary>
        internal static CacheConfig CacheConfig
        {
            get; set;
        }

        /// <summary>
        /// 首次加载所有的CacheProviders
        /// </summary>
        internal static Dictionary<string, ICacheProvider> CacheProviders
        {
            get
            {
                if (cacheProviders == null)
                {
                    lock (lockObj)
                    {
                        if (cacheProviders == null)
                        {
                            cacheProviders = new Dictionary<string, ICacheProvider>();

                            foreach (var i in CacheConfig.CacheProviderItems)
                            {
                                cacheProviders.Add(i.Name, (ICacheProvider)Activator.CreateInstance(Type.GetType(i.Type)));
                            }
                        }
                    }
                }

                return cacheProviders;
            }
        }

        /// <summary>
        /// 首次加载所有的CacheConfig, wrapCacheConfigItem相对于cacheConfigItem把providername通过反射还原成了具体provider类
        /// </summary>
        internal static List<WrapCacheConfigItem> WrapCacheConfigItems
        {
            get
            {
                if (wrapCacheConfigItems == null)
                {
                    lock (lockObj)
                    {
                        if (wrapCacheConfigItems == null)
                        {
                            wrapCacheConfigItems = new List<WrapCacheConfigItem>();

                            foreach (var i in CacheConfig.CacheConfigItems)
                            {
                                WrapCacheConfigItem _cacheWrapConfigItem = new WrapCacheConfigItem();
                                _cacheWrapConfigItem.CacheConfigItem = i;
                                _cacheWrapConfigItem.CacheProviderItem = CacheConfig.CacheProviderItems.SingleOrDefault(c => c.Name == i.ProviderName);
                                _cacheWrapConfigItem.CacheProvider = CacheProviders[i.ProviderName];

                                wrapCacheConfigItems.Add(_cacheWrapConfigItem);
                            }
                        }
                    }
                }

                return wrapCacheConfigItems;
            }
        }

        /// <summary>
        /// 设置缓存配置
        /// </summary>
        /// <param name="cacheConfig">CacheConfig</param>
        /// 时间：2016-03-21 11:09
        /// 备注：
        public static void SetCacheConfig(CacheConfig cacheConfig)
        {
            ValidateHelper.Begin().NotNull(cacheConfig, "CacheConfig缓存配置项");
            CacheConfig = cacheConfig;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 根据Key，通过正则匹配从WrapCacheConfigItems里帅选出符合的缓存项目，然后通过字典缓存起来
        /// </summary>
        /// <param name="key">根据Key获取缓存配置项</param>
        /// <returns>缓存配置项</returns>
        /// 时间：2015-12-31 15:52
        /// 备注：
        /// <exception cref="System.Exception"></exception>
        public static WrapCacheConfigItem GetCurrentWrapCacheConfigItem(string key)
        {
            if (wrapCacheConfigItemDic == null)
                wrapCacheConfigItemDic = new Dictionary<string, WrapCacheConfigItem>();

            if (wrapCacheConfigItemDic.ContainsKey(key))
                return wrapCacheConfigItemDic[key];

            WrapCacheConfigItem _currentWrapCacheConfigItem = WrapCacheConfigItems.Where(i =>
                Regex.IsMatch(ModuleName, i.CacheConfigItem.ModuleRegex, RegexOptions.IgnoreCase) &&
                Regex.IsMatch(key, i.CacheConfigItem.KeyRegex, RegexOptions.IgnoreCase))
                .OrderByDescending(i => i.CacheConfigItem.Priority).FirstOrDefault();

            if (_currentWrapCacheConfigItem == null)
                throw new Exception(string.Format("依据'{0}'获取缓存配置项异常！", key));

            lock (lockObj)
            {
                if (!wrapCacheConfigItemDic.ContainsKey(key))
                    wrapCacheConfigItemDic.Add(key, _currentWrapCacheConfigItem);
            }

            return _currentWrapCacheConfigItem;
        }

        #endregion Methods
    }
}