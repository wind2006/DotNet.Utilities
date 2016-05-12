using System.Runtime.Serialization;

namespace WCF.TransaccationScope.Model
{
    [DataContract]
    public class Shop
    {
        [DataMember]
        public int ShopID { get; set; }

        [DataMember]
        public int UserID { get; set; }

        [DataMember]
        public string ShopName { get; set; }

        [DataMember]
        public string ShopUrl { get; set; }
    }
}