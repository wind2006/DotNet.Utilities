namespace YanZhiwei.DotNet.Core.RBAC
{
    using System.Collections.Generic;

    using YanZhiwei.DotNet.Core.RBAC.Model;

    /// <summary>
    /// 基于角色的权限配置上下文，默认存储在Sql Server
    /// </summary>
    public class RBACContext
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="rbacProvider">IRBACProvider</param>
        public RBACContext(IRBACProvider rbacProvider)
        {
            this.RoleBaseService = rbacProvider;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="roleBaseService">基于IRBACProvider实现</param>
        /// <param name="connectString">连接字符串</param>
        /// 时间：2016-04-29 10:32
        /// 备注：
        public RBACContext(IRBACProvider roleBaseService, string connectString)
        {
            this.RoleBaseService = roleBaseService;
        }

        /// <summary>
        /// 默认Ms Sql server
        /// </summary>
        /// <param name="connectString">连接字符串</param>
        /// 时间：2016-04-29 10:33
        /// 备注：
        public RBACContext(string connectString)
            : this(new SqlServerProvider(connectString))
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// IRoleBaseService接口
        /// </summary>
        public IRBACProvider RoleBaseService
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 创建角色权限
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <param name="roleCode">角色代码</param>
        /// <param name="modulePermissionId">模块操作标识ID数组</param>
        /// <returns>
        /// 创建是否成功
        /// </returns>
        public bool CreateRole(string roleName, string roleCode, string[] modulePermissionId)
        {
            return RoleBaseService.CreateRole(roleName, roleCode, modulePermissionId);
        }

        /// <summary>
        /// 根据角色代码删除角色
        /// </summary>
        /// <param name="codeArray">角色代码数组，eg:1,2,3</param>
        public bool DeleteRoleByCode(string codeArray)
        {
            return RoleBaseService.DeleteRoleByCode(codeArray);
        }

        /// <summary>
        /// 根据角色名称模糊查询
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="name">角色名称</param>
        /// <returns></returns>
        public List<T> FuzzySearchRolesByName<T>(string name)
            where T : Role, new()
        {
            return RoleBaseService.FuzzySearchRolesByName<T>(name);
        }

        /// <summary>
        /// 获取所有模块操作权限列表
        /// </summary>
        /// <returns>集合</returns>
        public List<T> GetAllModulePermission<T>()
            where T : ModulePermission, new()
        {
            return RoleBaseService.GetAllModulePermission<T>();
        }

        /// <summary>
        /// 获取所有模块功能
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>
        /// 集合
        /// </returns>
        public List<T> GetAllModules<T>()
            where T : Module, new()
        {
            return RoleBaseService.GetAllModules<T>();
        }

        /// <summary>
        /// 获取所有角色对应的模块操作权限列表
        /// </summary>
        /// <returns>集合</returns>
        public List<T> GetAllRolePermission<T>()
            where T : RolePermission, new()
        {
            return RoleBaseService.GetAllRolePermission<T>();
        }

        /// <summary>
        /// 获取所有角色列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>集合</returns>
        public List<T> GetAllRoles<T>()
            where T : Role, new()
        {
            return RoleBaseService.GetAllRoles<T>();
        }

        /// <summary>
        /// 获取所有角色对应的模块操作权限列表
        /// </summary>
        /// <returns>集合</returns>
        public List<T> GetRolePermission<T>(string roleCode)
            where T : RolePermission, new()
        {
            return RoleBaseService.GetRolePermission<T>(roleCode);
        }

        /// <summary>
        /// 保存角色权限设置
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <param name="roleCode">角色代码</param>
        /// <param name="modulePermissionId">模块操作标识ID数组</param>
        /// <returns>
        /// 保存是否成功
        /// </returns>
        public bool UpdateRole(string roleName, string roleCode, string[] modulePermissionId)
        {
            return RoleBaseService.UpdateRole(roleName, roleCode, modulePermissionId);
        }

        #endregion Methods
    }
}