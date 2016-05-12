namespace YanZhiwei.DotNet.Core.Model
{
    using System.Xml.Serialization;

    /// <summary>
    /// 缓存配置
    /// </summary>
    /// 时间：2015-12-31 14:22
    /// 备注：
    public class CacheConfigItem : ConfigNodeBase
    {
        #region Properties

        /// <summary>
        /// 描述
        /// </summary>
        [XmlAttribute(AttributeName = "desc")]
        public string Desc
        {
            get; set;
        }

        /// <summary>
        /// 是否绝对时间
        /// </summary>
        [XmlAttribute(AttributeName = "isAbsoluteExpiration")]
        public bool IsAbsoluteExpiration
        {
            get; set;
        }

        /// <summary>
        /// 缓存键的正则表达式
        /// </summary>
        [XmlAttribute(AttributeName = "keyRegex")]
        public string KeyRegex
        {
            get; set;
        }

        /// <summary>
        /// 分钟
        /// </summary>
        [XmlAttribute(AttributeName = "minitus")]
        public int Minitus
        {
            get; set;
        }

        /// <summary>
        /// 功能正则表达式
        /// </summary>
        [XmlAttribute(AttributeName = "moduleRegex")]
        public string ModuleRegex
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        [XmlAttribute(AttributeName = "priority")]
        public int Priority
        {
            get; set;
        }
        /// <summary>
        /// ProviderName
        /// </summary>
        [XmlAttribute(AttributeName = "providerName")]
        public string ProviderName
        {
            get; set;
        }

        #endregion Properties
    }
}