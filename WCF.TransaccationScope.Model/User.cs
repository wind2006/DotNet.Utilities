using System.Runtime.Serialization;

namespace WCF.TransaccationScope.Model
{
    [DataContract]
    public class User
    {
        [DataMember]
        public int UserID { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}