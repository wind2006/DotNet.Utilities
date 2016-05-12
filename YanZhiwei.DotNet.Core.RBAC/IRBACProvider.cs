namespace YanZhiwei.DotNet.Core.RBAC
{
    using System.Collections.Generic;

    using YanZhiwei.DotNet.Core.RBAC.Model;

    /// <summary>
    /// 角色权限接口
    /// </summary>
    public interface IRBACProvider
    {
        #region Methods

        /// <summary>
        /// 创建角色权限
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <param name="roleCode">角色代码</param>
        /// <param name="modulePermissionId">模块操作标识ID数组</param>
        /// <returns>创建是否成功</returns>
        bool CreateRole(string roleName, string roleCode, string[] modulePermissionId);

        /// <summary>
        /// 根据角色代码删除角色
        /// </summary>
        /// <param name="roleCodes">角色代码,eg:1,2,3</param>
        bool DeleteRoleByCode(string roleCodes);

        /// <summary>
        /// 根据角色名称模糊查询
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="name">角色名称</param>
        List<T> FuzzySearchRolesByName<T>(string name)
            where T : Role, new();

        /// <summary>
        /// 获取所有模块操作权限列表
        /// </summary>
        /// <returns>集合</returns>
        List<T> GetAllModulePermission<T>()
            where T : ModulePermission, new();

        /// <summary>
        /// 获取所有模块功能
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>集合</returns>
        List<T> GetAllModules<T>()
            where T : Module, new();

        /// <summary>
        /// 获取所有角色对应的模块操作权限列表
        /// </summary>
        /// <returns>集合</returns>
        List<T> GetAllRolePermission<T>()
            where T : RolePermission, new();

        /// <summary>
        /// 获取所有角色列表
        /// </summary>
        /// <returns>集合</returns>
        List<T> GetAllRoles<T>()
            where T : Role, new();

        /// <summary>
        /// 获取所有角色对应的模块操作权限列表
        /// </summary>
        /// <returns>集合</returns>
        List<T> GetRolePermission<T>(string roleCode)
            where T : RolePermission, new();

        /// <summary>
        /// 保存角色权限设置
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <param name="roleCode">角色代码</param>
        /// <param name="modulePermissionId">模块操作标识ID数组</param>
        /// <returns>保存是否成功</returns>
        bool UpdateRole(string roleName, string roleCode, string[] modulePermissionId);

        #endregion Methods
    }
}