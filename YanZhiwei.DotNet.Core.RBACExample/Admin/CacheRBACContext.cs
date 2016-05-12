using System.Collections.Generic;
using YanZhiwei.DotNet.Core.RBAC;
using YanZhiwei.DotNet.Core.RBAC.Model;
using YanZhiwei.DotNet2.Utilities.WebForm.Core;

namespace YanZhiwei.DotNet.Core.RBACExample.Admin
{
    public class CacheRBACContext : RBACContext
    {
        public CacheRBACContext() : base(@"server=YANZHIWEI-IT-PC\SQLEXPRESS;uid=sa;pwd=sasa;database=Permission;")
        {
        }

        public static CacheRBACContext Current = new CacheRBACContext();

        private string GetCacheKey<T>() where T : new()
        {
            string _typeName = typeof(T).Name,
                   _cacheKey = string.Format("roleBase_{0}", _typeName);
            return _cacheKey;
        }

        public List<Role> AllRoles
        {
            get
            {
                string _key = GetCacheKey<Role>();
                if (!CacheManger.Contain(_key))
                {
                    CacheManger.Set(_key, GetAllRoles<Role>());
                }
                return (List<Role>)CacheManger.Get(_key);
            }
        }

        public List<ModulePermission> AllModulePermissions
        {
            get
            {
                string _key = GetCacheKey<ModulePermission>();
                if (!CacheManger.Contain(_key))
                {
                    CacheManger.Set(_key, GetAllModulePermission<ModulePermission>());
                }
                return (List<ModulePermission>)CacheManger.Get(_key);
            }
        }

        public List<Module> AllModules
        {
            get
            {
                string _key = GetCacheKey<Module>();
                if (!CacheManger.Contain(_key))
                {
                    CacheManger.Set(_key, GetAllModules<Module>());
                }
                return (List<Module>)CacheManger.Get(_key);
            }
        }
    }
}