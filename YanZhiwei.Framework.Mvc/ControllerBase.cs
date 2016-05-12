namespace YanZhiwei.Framework.Mvc
{
    using DotNet.Framework.Contract;
    using System;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;

    using YanZhiwei.DotNet2.Utilities.WebForm;
    using YanZhiwei.DotNet3._5.Utilities.Common;

    public class ControllerBase : Controller
    {
        #region Properties

        /// <summary>
        /// 操作人，传IP....到后端记录
        /// </summary>
        public virtual Operater Operater
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// 分页大小
        /// </summary>
        public virtual int PageSize
        {
            get
            {
                return 15;
            }
        }

        /// <summary>
        /// 当前Http上下文信息，用于写Log或其他作用
        /// </summary>
        public WebExceptionContext WebExceptionContext
        {
            get
            {
                var exceptionContext = new WebExceptionContext
                {
                    IP = FetchHelper.UserIp,
                    CurrentUrl = HyperlinkHelper.CurrentUrl,
                    RefUrl = (Request == null || Request.UrlReferrer == null) ? string.Empty : Request.UrlReferrer.AbsoluteUri,
                    IsAjaxRequest = (Request == null) ? false : Request.IsAjaxRequest(),
                    FormData = (Request == null) ? null : Request.Form,
                    QueryData = (Request == null) ? null : Request.QueryString,
                    RouteData = (Request == null || Request.RequestContext == null || Request.RequestContext.RouteData == null) ? null : Request.RequestContext.RouteData.Values
                };

                return exceptionContext;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        ///  警告并且历史返回
        /// </summary>
        /// <param name="notice">需要弹出的消息</param>
        /// <returns>ContentResult</returns>
        public ContentResult Back(string notice)
        {
            var content = new StringBuilder("<script>");
            if (!string.IsNullOrEmpty(notice))
                content.AppendFormat("alert('{0}');", notice);
            content.Append("history.go(-1)</script>");
            return this.Content(content.ToString());
        }

        /// <summary>
        /// 清除操作者
        /// </summary>
        public virtual void ClearOperater()
        {
            //TODO
        }

        /// <summary>
        /// 用JS关闭弹窗
        /// </summary>
        /// <returns>ContentResult</returns>
        public ContentResult CloseThickbox()
        {
            return this.Content("<script>top.tb_remove()</script>");
        }

        /// <summary>
        /// 页面返回
        /// </summary>
        /// <param name="msg">需要弹出的消息</param>
        /// <param name="url">需要返回连接</param>
        /// <returns>ContentResult</returns>
        /// 时间：2016-01-14 17:22
        /// 备注：
        public ContentResult PageReturn(string msg, string url = null)
        {
            var content = new StringBuilder("<script type='text/javascript'>");
            if (!string.IsNullOrEmpty(msg))
                content.AppendFormat("alert('{0}');", msg);
            if (string.IsNullOrWhiteSpace(url))
                url = Request.Url.ToString();
            content.Append("window.location.href='" + url + "'</script>");
            return this.Content(content.ToString());
        }

        /// <summary>
        /// 当弹出DIV弹窗时，需要刷新浏览器整个页面
        /// </summary>
        /// <returns>ContentResult</returns>
        public ContentResult RefreshParent(string alert = null)
        {
            var script = string.Format("<script>{0}; parent.location.reload(1)</script>", string.IsNullOrEmpty(alert) ? string.Empty : "alert('" + alert + "')");
            return this.Content(script);
        }

        /// <summary>
        /// 刷新父窗体
        /// </summary>
        /// <param name="alert">需要弹出的消息</param>
        /// <returns>ContentResult</returns>
        /// 时间：2016-01-14 17:23
        /// 备注：
        public ContentResult RefreshParentTab(string alert = null)
        {
            var script = string.Format("<script>{0}; if (window.opener != null) {{ window.opener.location.reload(); window.opener = null;window.open('', '_self', '');  window.close()}} else {{parent.location.reload(1)}}</script>", string.IsNullOrEmpty(alert) ? string.Empty : "alert('" + alert + "')");
            return this.Content(script);
        }

        /// <summary>
        /// 转向到一个提示页面，然后自动返回指定的页面
        /// </summary>
        /// <param name="notice"></param>
        /// <param name="redirect"></param>
        /// <returns></returns>
        public ContentResult Stop(string notice, string redirect, bool isAlert = false)
        {
            var content = "<meta http-equiv='refresh' content='1;url=" + redirect + "' /><body style='margin-top:0px;color:red;font-size:24px;'>" + notice + "</body>";

            if (isAlert)
                content = string.Format("<script>alert('{0}'); window.location.href='{1}'</script>", notice, redirect);

            return this.Content(content);
        }

        /// <summary>
        /// 在方法执行前更新操作人
        /// </summary>
        /// <param name="filterContext"></param>
        public virtual void UpdateOperater(ActionExecutingContext filterContext)
        {
            if (this.Operater == null)
                return;

            ServiceCallContext.Current.Operater = this.Operater;
        }

        /// <summary>
        /// 跨域Json
        /// </summary>
        /// <param name="callback">callback</param>
        /// <param name="data">需要序列化的对象</param>
        /// <returns>ContentResult</returns>
        /// 时间：2016-01-14 17:24
        /// 备注：
        protected ContentResult JsonP(string callback, object data)
        {
            var json = SerializationHelper.JsonSerialize(data);
            return this.Content(string.Format("{0}({1})", callback, json));
        }

        /// <summary>
        /// 记录异常
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <param name="exceptionContext">WebExceptionContext</param>
        /// 时间：2016-01-14 17:25
        /// 备注：
        protected virtual void LogException(Exception exception, WebExceptionContext exceptionContext = null)
        {
            //do nothing!
        }

        /// <summary>
        /// AOP拦截，在Action执行后
        /// </summary>
        /// <param name="filterContext">ActionExecutedContext</param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            if (!filterContext.RequestContext.HttpContext.Request.IsAjaxRequest() && !filterContext.IsChildAction)
                RenderViewData();

            this.ClearOperater();
        }

        /// <summary>
        /// 在调用Action方法之前调用
        /// </summary>
        /// <param name="filterContext">ActionExecutingContext</param>
        /// 时间：2016-01-14 11:32
        /// 备注：
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.UpdateOperater(filterContext);
            base.OnActionExecuting(filterContext);

            //在方法执行前，附加上PageSize值
            filterContext.ActionParameters.Values.Where(v => v is BusinessRequest).ToList().ForEach(v => ((BusinessRequest)v).PageSize = this.PageSize);
        }

        /// <summary>
        /// 发生异常写Log
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            var e = filterContext.Exception;

            LogException(e, this.WebExceptionContext);
        }

        /// <summary>
        /// 产生一些视图数据
        /// </summary>
        protected virtual void RenderViewData()
        {
        }

        #endregion Methods

        #region Other

        /*
        ASP.NET MVC 框架会在调用Action方法之前调用你Action过滤器中的OnActionExecuting方法，在之后调用Action过滤器中的OnActionExecuted方法。
        */

        #endregion Other
    }
}