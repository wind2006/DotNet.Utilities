using MvcSolution.GMS.Contract.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MvcSolution.Web
{
    /// <summary>
    /// 用于权限点认证，标记在Action上面
    /// </summary>
    public class PermissionAttribute : FilterAttribute, IActionFilter
    {
        public List<EnumBusinessPermission> Permissions { get; set; }

        public PermissionAttribute(params EnumBusinessPermission[] parameters)
        {
            Permissions = parameters.ToList();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //throw new NotImplementedException();
        }
    }
}