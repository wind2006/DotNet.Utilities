using YanZhiwei.DotNet.Framework.Contract;

namespace MvcSolution.GMS.Contract.Models
{
    public class UserRequest : BusinessRequest
    {
        public string LoginName { get; set; }
        public string Mobile { get; set; }
    }

    public class RoleRequest : BusinessRequest
    {
        public string RoleName { get; set; }
    }
}