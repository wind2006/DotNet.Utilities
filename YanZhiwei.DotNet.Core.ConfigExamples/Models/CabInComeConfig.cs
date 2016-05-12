using System;
using YanZhiwei.DotNet.Core.Model;

namespace YanZhiwei.DotNet.Core.ConfigExamples.Models
{
    [Serializable]
    public class CabInComeConfig : ConfigFileBase
    {
        public CabInComeConfig()
        {
        }

        /// <summary>
        /// 缓存配置项集合
        /// </summary>
        public CabInComeConfigItem[] CacheConfigItems
        {
            get; set;
        }
    }
}