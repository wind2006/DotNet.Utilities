using System.Web.Caching;
using YanZhiwei.DotNet.Core.Config;
using YanZhiwei.DotNet.Core.ConfigExamples.Models;
using YanZhiwei.DotNet.Core.Model;
using YanZhiwei.DotNet2.Utilities.WebForm.Core;

namespace YanZhiwei.DotNet.Core.ConfigExamples
{
    public class CachedConfigContext : ConfigContext
    {
        /// <summary>
        /// 重写基类的取配置，加入缓存机制
        /// </summary>
        public override T Get<T>(string index = null)
        {
            string _fileName = this.GetConfigFileName<T>(index),
                   _key = "ConfigFile_" + _fileName;
            object _content = CacheManger.Get(_key);
            if (_content != null)
            {
                return (T)_content;
            }
            else {
                T _value = base.Get<T>(index);
                CacheManger.Set(_key, _value, new CacheDependency(ConfigService.GetFilePath(_fileName)));
                return _value;
            }
        }

        public static CachedConfigContext Current = new CachedConfigContext();

        public CacheConfig CacheConfig
        {
            get
            {
                return this.Get<CacheConfig>();
            }
        }

        public DaoConfig DaoConfig
        {
            get
            {
                return this.Get<DaoConfig>();
            }
        }

        public CabInComeConfig CabInComeConfig
        {
            get
            {
                return this.Get<CabInComeConfig>();
            }
        }
    }
}