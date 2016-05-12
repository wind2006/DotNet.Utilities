using YanZhiwei.DotNet.Framework.Contract;

namespace MvcSolution.OA.Contract.Models
{
    public class StaffRequest : BusinessRequest
    {
        public string Name { get; set; }
        public int BranchId { get; set; }
    }

    public class BranchRequest : BusinessRequest
    {
        public string Name { get; set; }
    }
}