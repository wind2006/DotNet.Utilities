namespace YanZhiwei.DotNet.Core.RBAC.Model
{
    /// <summary>
    /// RBAC实体基类
    /// </summary>
    /// 时间：2016-04-29 10:43
    /// 备注：
    public class RoleBase
    {
        /// <summary>
        /// 角色代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 角色排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool Visible { get; set; }
    }
}