using System;
using System.Collections.Concurrent;
using YanZhiwei.DotNet2.Utilities.Common;

namespace YanZhiwei.DotNet.Core.Cache
{
    /// <summary>
    /// 缓存操作管理器
    /// </summary>
    public static class CacheManager
    {
        private static readonly ConcurrentDictionary<string, ICache> Cachers;

        //两级缓存
        internal static readonly ICacheProvider[] Providers = new ICacheProvider[2];

        static CacheManager()
        {
            Cachers = new ConcurrentDictionary<string, ICache>();
        }

        /// <summary>
        /// 设置缓存提供者
        /// </summary>
        /// <param name="provider">缓存提供者</param>
        /// <param name="level">缓存级别</param>
        public static void SetProvider(ICacheProvider provider, CacheLevel level)
        {
            ValidateHelper.Begin().NotNull(provider, "缓存提供者");
            switch (level)
            {
                case CacheLevel.First:
                    Providers[0] = provider;
                    break;

                case CacheLevel.Second:
                    Providers[1] = provider;
                    break;
            }
        }

        /// <summary>
        /// 移除指定级别的缓存提供者
        /// </summary>
        /// <param name="level">缓存级别</param>
        public static void RemoveProvider(CacheLevel level)
        {
            switch (level)
            {
                case CacheLevel.First:
                    Providers[0] = null;
                    break;

                case CacheLevel.Second:
                    Providers[1] = null;
                    break;
            }
        }

        /// <summary>
        /// 获取指定区域的缓存执行者实例
        /// </summary>
        public static ICache GetCacher(string region)
        {
            ValidateHelper.Begin().NotNullOrEmpty(region, "缓存区域名称");
            ICache cache;
            if (Cachers.TryGetValue(region, out cache))
            {
                return cache;
            }
            cache = new InternalCacher(region);
            Cachers[region] = cache;
            return cache;
        }

        /// <summary>
        /// 获取指定类型的缓存执行者实例
        /// </summary>
        /// <param name="type">类型实例</param>
        public static ICache GetCacher(Type type)
        {
            ValidateHelper.Begin().NotNull(type, "类型实例");
            return GetCacher(type.FullName);
        }

        /// <summary>
        /// 获取指定类型的缓存执行者实例
        /// </summary>
        public static ICache GetCacher<T>()
        {
            return GetCacher(typeof(T));
        }
    }
}