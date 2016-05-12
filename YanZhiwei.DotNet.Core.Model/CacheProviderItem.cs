namespace YanZhiwei.DotNet.Core.Model
{
    using System.Xml.Serialization;

    /// <summary>
    /// 缓存Provider
    /// </summary>
    /// 时间：2015-12-31 14:21
    /// 备注：
    public class CacheProviderItem : ConfigNodeBase
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
        /// 名称
        /// </summary>
        [XmlAttribute(AttributeName = "name")]
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// 类型，格式：类全名,程序集名
        /// </summary>
        [XmlAttribute(AttributeName = "type")]
        public string Type
        {
            get; set;
        }

        #endregion Properties
    }
}