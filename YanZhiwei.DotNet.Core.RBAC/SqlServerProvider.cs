namespace YanZhiwei.DotNet.Core.RBAC
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;

    using YanZhiwei.DotNet.Core.RBAC.Model;
    using YanZhiwei.DotNet2.Utilities.Common;
    using YanZhiwei.DotNet2.Utilities.Core;

    /// <summary>
    /// 关于角色权限基于Sql Server实现
    /// </summary>
    /// 时间：2016-04-29 10:20
    /// 备注：
    public class SqlServerProvider : IRBACProvider
    {
        #region Fields

        /// <summary>
        /// 连接字符串
        /// </summary>
        /// 时间：2016-04-29 10:34
        /// 备注：
        public readonly string ConnectString;

        /// <summary>
        /// Sql Server数据库操作对象
        /// </summary>
        private SqlServerHelper sqlHelper = null;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数，用于初始化sql Server数据连接
        /// </summary>
        /// <param name="connectString">Sql Server连接字符串</param>
        /// 时间：2016-04-29 10:18
        /// 备注：
        public SqlServerProvider(string connectString)
        {
            ValidateHelper.Begin().NotNullOrEmpty(connectString, "Sql Server连接字符串");
            ConnectString = connectString;
            sqlHelper = new SqlServerHelper(connectString);
        }

        #endregion Constructors

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
        /// 时间：2016-04-29 10:21
        /// 备注：
        public bool CreateRole(string roleName, string roleCode, string[] modulePermissionId)
        {
            ValidateHelper.Begin().NotNullOrEmpty(roleName, "角色名称").NotNullOrEmpty(roleCode, "角色代码").NotNull(modulePermissionId, "角色对应模块操作标识ID数组");

            bool _result = false;
            using (SqlServerTransaction sqlTran = sqlHelper.BeginTranscation())
            {
                try
                {
                    SqlParameter[] _paramter = new SqlParameter[2];
                    _paramter[0] = new SqlParameter("@roleCode", roleCode);
                    _paramter[1] = new SqlParameter("@roleName", roleName);
                    sqlHelper.ExecuteNonQuery(sqlTran, "insert into Roles(R_Code,R_Name) values(@roleCode,@roleName)", _paramter);

                    foreach (string s in modulePermissionId)
                    {
                        if (!string.IsNullOrEmpty(s))
                        {
                            _paramter = new SqlParameter[2];
                            _paramter[0] = new SqlParameter("@roleCode", roleCode);
                            _paramter[1] = new SqlParameter("@mpId", s);
                            sqlHelper.ExecuteNonQuery(sqlTran, "insert into RolePermissions values(@roleCode,@mpId)", _paramter);
                        }
                    }
                    sqlTran.CommitTransaction();
                    _result = true;
                }
                catch (Exception)
                {
                    sqlTran.RollbackTransaction();
                    _result = false;
                }
                return _result;
            }
        }

        /// <summary>
        /// 根据角色代码删除角色
        /// </summary>
        /// <param name="roleCodes">角色代码,eg:1,2,3</param>
        /// <returns>操作是否成功</returns>
        /// 时间：2016-04-29 10:22
        /// 备注：
        public bool DeleteRoleByCode(string roleCodes)
        {
            ValidateHelper.Begin().NotNullOrEmpty(roleCodes, "角色代码,eg:1,2,3");
            string _sql = "exec('delete from Roles where R_Code in ('+@code+')')";
            SqlParameter[] _paramter = new SqlParameter[1];
            _paramter[0] = new SqlParameter("@code", SqlDbType.VarChar, -1) { Value = roleCodes };
            return sqlHelper.ExecuteNonQuery(_sql, _paramter) > 0;
        }

        /// <summary>
        /// 根据角色名称模糊查询
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="name">角色名称</param>
        /// <returns></returns>
        /// 时间：2015-11-26 11:37
        /// 备注：
        public List<T> FuzzySearchRolesByName<T>(string name)
            where T : Role, new()
        {
            string _sql = "select [R_Code],[R_Name],[R_Sort],[R_Visible] from Roles where r_visible=1  and R_Name like @name";
            SqlParameter[] _paramter = new SqlParameter[1];
            _paramter[0] = new SqlParameter("@name", "%" + name + "%");
            using (IDataReader reader = sqlHelper.ExecuteReader(_sql, _paramter))
            {
                List<T> _roles = new List<T>();
                while (reader.Read())
                {
                    T _singleRole = new T();
                    _singleRole.Code = reader.GetValue<string>("R_Code", string.Empty);
                    _singleRole.Name = reader.GetValue<string>("R_Name", string.Empty);
                    _singleRole.Sort = reader.GetValue<int>("R_Sort", 0);
                    _singleRole.Visible = reader.GetValue<bool>("R_Visible", false);
                    _roles.Add(_singleRole);
                }
                return _roles;
            };
        }

        /// <summary>
        /// 获取所有模块操作权限列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>
        /// 集合
        /// </returns>
        /// 时间：2016-04-29 10:23
        /// 备注：
        public List<T> GetAllModulePermission<T>()
            where T : ModulePermission, new()
        {
            string _sql = "select [M_Code],[P_Code],[P_Name],[M_Name],[Id],[M_ParentCode] from V_ModulePermissions";
            using (IDataReader reader = sqlHelper.ExecuteReader(_sql, null))
            {
                List<T> _modulePermissions = new List<T>();
                while (reader.Read())
                {
                    T _singleMP = new T();
                    _singleMP.Id = reader.GetValue<int>("Id", 0);
                    _singleMP.ModuleCode = reader.GetValue<string>("M_Code", string.Empty);
                    _singleMP.ModuleName = reader.GetValue<string>("M_Name", string.Empty);
                    _singleMP.ModuleParentCode = reader.GetValue<string>("M_ParentCode", string.Empty);
                    _singleMP.PermissionCode = reader.GetValue<string>("P_Code", string.Empty);
                    _singleMP.PermissionName = reader.GetValue<string>("P_Name", string.Empty);
                    _modulePermissions.Add(_singleMP);
                }
                return _modulePermissions;
            };
        }

        /// <summary>
        /// 获取所有模块功能
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>
        /// 集合
        /// </returns>
        /// 时间：2016-04-29 10:23
        /// 备注：
        public List<T> GetAllModules<T>()
            where T : Module, new()
        {
            string _sql = "SELECT [M_Code],[M_Name],[M_ParentCode],[M_LinkeUrl],[M_Sort],[M_Visible] FROM [Module] where M_Visible=1";
            using (IDataReader reader = sqlHelper.ExecuteReader(_sql, null))
            {
                List<T> _roles = new List<T>();
                while (reader.Read())
                {
                    T _singleModule = new T();
                    _singleModule.Code = reader.GetValue<string>("M_Code", string.Empty);
                    _singleModule.Name = reader.GetValue<string>("M_Name", string.Empty);
                    _singleModule.ParentCode = reader.GetValue<string>("M_ParentCode", string.Empty);
                    _singleModule.LinkeUrl = reader.GetValue<string>("M_LinkeUrl", "#");
                    _singleModule.Sort = reader.GetValue<int>("M_Sort", 0);
                    _singleModule.Visible = reader.GetValue<bool>("M_Visible", false);
                    _roles.Add(_singleModule);
                }
                return _roles;
            };
        }

        /// <summary>
        /// 获取所有角色对应的模块操作权限列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>
        /// 集合
        /// </returns>
        /// 时间：2016-04-29 10:23
        /// 备注：
        public List<T> GetAllRolePermission<T>()
            where T : RolePermission, new()
        {
            string _sql = "select [Id],[R_Code],[MP_Id] from Roles where r_visible=1";
            using (IDataReader reader = sqlHelper.ExecuteReader(_sql, null))
            {
                List<T> _roles = new List<T>();
                while (reader.Read())
                {
                    T _singleRP = new T();
                    _singleRP.Id = reader.GetValue<int>("Id", 0);
                    _singleRP.RoleCode = reader.GetValue<string>("R_Code", string.Empty);
                    _singleRP.ModulePermissionId = reader.GetValue<int>("MP_Id", 0);
                    _roles.Add(_singleRP);
                }
                return _roles;
            };
        }

        /// <summary>
        /// 获取所有角色列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>
        /// 集合
        /// </returns>
        /// 时间：2016-04-29 10:23
        /// 备注：
        public List<T> GetAllRoles<T>()
            where T : Role, new()
        {
            string _sql = "select [R_Code],[R_Name],[R_Sort],[R_Visible] from Roles where r_visible=1";
            using (IDataReader reader = sqlHelper.ExecuteReader(_sql, null))
            {
                List<T> _roles = new List<T>();
                while (reader.Read())
                {
                    T _singleRole = new T();
                    _singleRole.Code = reader.GetValue<string>("R_Code", string.Empty);
                    _singleRole.Name = reader.GetValue<string>("R_Name", string.Empty);
                    _singleRole.Sort = reader.GetValue<int>("R_Sort", 0);
                    _singleRole.Visible = reader.GetValue<bool>("R_Visible", false);
                    _roles.Add(_singleRole);
                }
                return _roles;
            };
        }

        /// <summary>
        /// 获取所有角色对应的模块操作权限列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="roleCode">角色代码</param>
        /// <returns>
        /// 集合
        /// </returns>
        /// 时间：2016-04-29 10:24
        /// 备注：
        public List<T> GetRolePermission<T>(string roleCode)
            where T : RolePermission, new()
        {
            ValidateHelper.Begin().NotNullOrEmpty(roleCode, "角色代码");
            string _sql = "select [Id],[R_Code],[MP_Id] from RolePermissions where R_Code=@code";
            DbParameter[] _paramter = new DbParameter[1];
            _paramter[0] = new SqlParameter("@code", roleCode);
            using (IDataReader reader = sqlHelper.ExecuteReader(_sql, _paramter))
            {
                List<T> _roles = new List<T>();
                while (reader.Read())
                {
                    T _singleRP = new T();
                    _singleRP.Id = reader.GetValue<int>("Id", 0);
                    _singleRP.RoleCode = reader.GetValue<string>("R_Code", string.Empty);
                    _singleRP.ModulePermissionId = reader.GetValue<int>("MP_Id", 0);
                    _roles.Add(_singleRP);
                }
                return _roles;
            };
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
        /// 时间：2016-04-29 10:25
        /// 备注：
        public bool UpdateRole(string roleName, string roleCode, string[] modulePermissionId)
        {
            ValidateHelper.Begin().NotNullOrEmpty(roleName, "角色名称").NotNullOrEmpty(roleCode, "角色代码").NotNull(modulePermissionId, "角色对应模块操作标识ID数组");

            bool _result = false;
            using (SqlServerTransaction sqlTran = sqlHelper.BeginTranscation())
            {
                try
                {
                    SqlParameter[] _paramter = new SqlParameter[1];
                    _paramter[0] = new SqlParameter("@roleCode", roleCode);
                    sqlHelper.ExecuteNonQuery(sqlTran, "delete from RolePermissions where R_Code=@roleCode;", _paramter);

                    _paramter = new SqlParameter[3];
                    _paramter[0] = new SqlParameter("@roleCode", roleCode);
                    _paramter[1] = new SqlParameter("@roleName", roleName);
                    _paramter[2] = new SqlParameter("@roleCodeWhere", roleCode);
                    sqlHelper.ExecuteNonQuery(sqlTran, "update Roles set R_Code=@roleCode,R_Name=@roleName where R_Code=@roleCodeWhere", _paramter);

                    foreach (string s in modulePermissionId)
                    {
                        if (!string.IsNullOrEmpty(s))
                        {
                            _paramter = new SqlParameter[2];
                            _paramter[0] = new SqlParameter("@roleCode", roleCode);
                            _paramter[1] = new SqlParameter("@mpId", s);
                            sqlHelper.ExecuteNonQuery(sqlTran, "insert into dbo.RolePermissions values(@roleCode,@mpId)", _paramter);
                        }
                    }
                    sqlTran.CommitTransaction();
                    _result = true;
                }
                catch (Exception)
                {
                    sqlTran.RollbackTransaction();
                    _result = false;
                }
                return _result;
            }
        }

        #endregion Methods
    }
}