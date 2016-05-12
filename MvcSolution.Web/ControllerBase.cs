using MvcSolution.Cms.Contract;
using MvcSolution.Core.Log;
using MvcSolution.Crm.BLL;
using MvcSolution.GMS.Contract;
using MvcSolution.OA.Contract;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using YanZhiwei.DotNet.Core.Log;
using YanZhiwei.Framework.Mvc;

namespace MvcSolution.Web
{
    public abstract class ControllerBase : YanZhiwei.Framework.Mvc.ControllerBase
    {
        public virtual IAccountService AccountService
        {
            get
            {
                return ServiceContext.Current.AccountService;
            }
        }

        public virtual ICmsService CmsService
        {
            get
            {
                return ServiceContext.Current.CmsService;
            }
        }

        public virtual ICrmService CrmService
        {
            get
            {
                return ServiceContext.Current.CrmService;
            }
        }

        public virtual IOAService OAService
        {
            get
            {
                return ServiceContext.Current.OAService;
            }
        }

        protected override void LogException(Exception exception,
            WebExceptionContext exceptionContext = null)
        {
            base.LogException(exception);

            var message = new
            {
                exception = exception.Message,
                exceptionContext = exceptionContext,
            };

            AppLog4NetHelper.Instance.Error(LoggerType.WebExceptionLog, message, exception);
        }

        public IDictionary<string, object> CurrentActionParameters { get; set; }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
    }
}