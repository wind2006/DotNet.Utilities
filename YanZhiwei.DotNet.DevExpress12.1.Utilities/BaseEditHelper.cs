namespace YanZhiwei.DotNet.DevExpress12._1.Utilities
{
    using System;

    using DevExpress.Utils;
    using DevExpress.XtraEditors;

    /// <summary>
    /// BaseEdit 辅助类
    /// </summary>
    public static class BaseEditHelper
    {
        #region Methods

        /// <summary>
        /// 为BaseEdit 提供即时消息提示；
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="control">BaseEdit</param>
        /// <param name="tip">ToolTipController</param>
        /// <param name="message">消息</param>
        public static void PromptTimelyMessage<T>(this T control, ToolTipController tip, string message)
            where T : BaseEdit
        {
            if (control != null && tip != null)
            {
                control.MouseEnter += (object sender, EventArgs e) =>
                {
                    T _curControl = sender as T;
                    tip.ShowHint(message, sender as T, DevExpress.Utils.ToolTipLocation.RightCenter);
                };
            }
        }

        #endregion Methods
    }
}