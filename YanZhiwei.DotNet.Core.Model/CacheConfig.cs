namespace YanZhiwei.DotNet.Core.Model
{
    using System;

    /// <summary>
    /// 缓存配置项
    /// </summary>
    /// 时间：2015-12-31 16:58
    /// 备注：
    [Serializable]
    public class CacheConfig : ConfigFileBase
    {
        #region Constructors

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CacheConfig()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 缓存配置项集合
        /// </summary>
        public CacheConfigItem[] CacheConfigItems
        {
            get; set;
        }

        /// <summary>
        /// 缓存提供者项集合
        /// </summary>
        public CacheProviderItem[] CacheProviderItems
        {
            get; set;
        }

        #endregion Properties
    }
}