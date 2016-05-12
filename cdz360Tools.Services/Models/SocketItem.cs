using System.Xml.Serialization;
using YanZhiwei.DotNet.Core.Model;

namespace cdz360Tools.Services.Models
{
    /// <summary>
    /// Socket 配置参数
    /// </summary>
    /// 时间：2016-04-12 15:19
    /// 备注：
    public class SocketItem : ConfigNodeBase
    {
        /// <summary>
        /// Ip地址
        /// </summary>
        [XmlAttribute(AttributeName = "IpAddress")]
        public string IpAddress { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        [XmlAttribute(AttributeName = "Port")]
        public int Port { get; set; }
    }
}