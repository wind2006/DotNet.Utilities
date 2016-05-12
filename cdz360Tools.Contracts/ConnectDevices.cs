using System.Runtime.Serialization;

namespace cdz360Tools.Contracts
{
    [DataContract]
    public class ConnectDevices
    {
        [DataMember]
        public string DevciesSeqNo { get; set; }
    }
}