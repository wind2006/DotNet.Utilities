namespace YanZhiwei.DotNet.Core.RBAC.Model
{
    /// <summary>
    /// 功能模块实体类
    /// </summary>
    /// 时间：2016-04-29 10:45
    /// 备注：
    public class Module : RoleBase
    {
        /// <summary>
        /// 模块父一级代码
        /// </summary>
        public string ParentCode { get; set; }

        /// <summary>
        /// 模块链接地址
        /// </summary>
        public string LinkeUrl { get; set; }
    }
}