namespace YanZhiwei.DotNet2.Utilities.WebForm
{
    using System.Web.UI.WebControls;

    /// <summary>
    /// LinkButton 帮助类
    /// </summary>
    public static class LinkButtonHelper
    {
        #region Methods

        /// <summary>
        /// 为LinkButton添加确认窗口
        /// </summary>
        /// <param name="linkbutton">LinkButton</param>
        /// <param name="message">确认窗口文字信息</param>
        public static void Confirm(this LinkButton linkbutton, string message)
        {
            linkbutton.Attributes.Add("onclick", "if (!confirm('" + message + "')) {return false;}");
        }

        #endregion Methods
    }
}