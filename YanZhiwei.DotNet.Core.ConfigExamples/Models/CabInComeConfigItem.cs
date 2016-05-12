using System.Xml.Serialization;
using YanZhiwei.DotNet.Core.Model;

namespace YanZhiwei.DotNet.Core.ConfigExamples.Models
{
    public class CabInComeConfigItem : ConfigNodeBase
    {
        [XmlAttribute(AttributeName = "cabId")]
        public string CabId { get; set; }

        [XmlAttribute(AttributeName = "keyId")]
        public string KeyId { get; set; }

        [XmlAttribute(AttributeName = "currentMaxValue")]
        public double CurrentMaxValue { get; set; }

        [XmlAttribute(AttributeName = "currentMinValue")]
        public double CurrentMinValue { get; set; }

        [XmlAttribute(AttributeName = "voltageMaxValue")]
        public double VoltageMaxValue { get; set; }

        [XmlAttribute(AttributeName = "voltageMinValue")]
        public double VoltageMinValue { get; set; }
    }
}