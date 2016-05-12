namespace YanZhiwei.DotNet2.Utilities.WebForm
{
    using System;
    using System.IO;
    using System.Web;
    using System.Web.UI;

    /// <summary>
    /// WebForm 帮助类
    /// </summary>
    public static class WebFormHelper
    {
        #region Methods

        /// <summary>
        /// 根据ID递归查找
        /// </summary>
        /// <param name="control">Control</param>
        /// <param name="id">控件ID</param>
        /// <returns>查找到的控件</returns>
        public static Control FindControlRecursive(this Control control, string id)
        {
            Control _control = control.FindControl(id);

            if (_control == null)
            {
                foreach (Control ctrl in control.Controls)
                {
                    _control = ctrl.FindControl(id);
                    if (_control != null)
                    {
                        break;
                    }
                    else
                    {
                        _control = FindControlRecursive(ctrl, id);
                    }
                }
            }

            return _control;
        }

        /// <summary>
        /// 根据Key获取QueryString
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns>数值</returns>
        public static string GetQueryString(this string key)
        {
            if ((HttpContext.Current.Request.QueryString[key] != null) &&
                (HttpContext.Current.Request.QueryString[key] != "undefined"))
            {
                return HttpContext.Current.Request.QueryString[key].Trim();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 重启程序
        /// </summary>
        /// <returns>重启是否成功</returns>
        public static bool RestartWebApplication()
        {
            bool _error = false;

            try
            {
                HttpRuntime.UnloadAppDomain();
            }
            catch
            {
                _error = true;
            }

            if (!_error)
            {
                return true;
            }

            try
            {
                File.SetLastWriteTimeUtc(
                    HttpContext.Current.Request.ApplicationPath + "\\web.config", DateTime.UtcNow);
            }
            catch
            {
                return false;
            }

            return true;
        }

        #endregion Methods
    }
}