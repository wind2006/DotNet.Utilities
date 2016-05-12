using YanZhiwei.DotNet.Framework.Contract;

namespace YanZhiwei.DotNet4.Framework.Data.Example.Contract
{
    public class CustomerRequest : BusinessRequest
    {
        public string CustomerID { get; set; }
        public string ContactName { get; set; }
    }
}