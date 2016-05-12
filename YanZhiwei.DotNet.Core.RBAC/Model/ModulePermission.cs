namespace YanZhiwei.DotNet.Core.RBAC.Model
{
    /// <summary>
    /// 模块对应的操作权限
    /// </summary>
    /// 时间：2016-04-29 10:44
    /// 备注：
    public class ModulePermission
    {
        /// <summary>
        /// 标志ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 模块代码
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 模块代码
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 模块父一级代码
        /// </summary>
        public string ModuleParentCode { get; set; }
        /// <summary>
        /// 操作权限代码
        /// </summary>
        public string PermissionCode { get; set; }

        /// <summary>
        /// 操作权限名称
        /// </summary>
        public string PermissionName { get; set; }
    }
}