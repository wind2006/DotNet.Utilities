using cdz360Tools.Services.Models;
using System.Web.Caching;
using YanZhiwei.DotNet.Core.Config;
using YanZhiwei.DotNet2.Utilities.WebForm.Core;

namespace cdz360Tools.Services
{
    /// <summary>
    /// 缓存Config文件配置
    /// </summary>
    /// 时间：2016-04-12 15:25
    /// 备注：
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

        public SocketConfig SocketConfig
        {
            get
            {
                return this.Get<SocketConfig>();
            }
        }

        public DaoConfig DaoConfig
        {
            get
            {
                return this.Get<DaoConfig>();
            }
        }

        public JobConfig JobConfig
        {
            get
            {
                return this.Get<JobConfig>();
            }
        }
    }
}