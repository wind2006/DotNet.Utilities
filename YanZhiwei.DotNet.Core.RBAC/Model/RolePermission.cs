namespace YanZhiwei.DotNet.Core.RBAC.Model
{
    /// <summary>
    /// 角色对应的模块操作权限
    /// </summary>
    /// 时间：2016-04-29 10:45
    /// 备注：
    public class RolePermission
    {
        /// <summary>
        /// 标志ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 角色代码
        /// </summary>
        public string RoleCode { get; set; }

        /// <summary>
        /// 模块操作权限Id
        /// </summary>
        public int ModulePermissionId { get; set; }
    }
}