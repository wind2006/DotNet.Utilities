namespace YanZhiwei.DotNet.Core.Cache.Models
{
    using Model;

    /// <summary>
    /// 缓存项
    /// </summary>
    /// 时间：2015-12-31 14:24
    /// 备注：
    public class WrapCacheConfigItem
    {
        #region Properties

        /// <summary>
        /// 缓存配置项
        /// </summary>
        public CacheConfigItem CacheConfigItem
        {
            get; set;
        }

        /// <summary>
        /// 缓存接口
        /// </summary>
        public ICacheProvider CacheProvider
        {
            get; set;
        }

        /// <summary>
        /// 缓存提供者配置项
        /// </summary>
        public CacheProviderItem CacheProviderItem
        {
            get; set;
        }

        #endregion Properties
    }
}