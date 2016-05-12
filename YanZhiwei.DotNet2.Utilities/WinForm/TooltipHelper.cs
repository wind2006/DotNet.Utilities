namespace YanZhiwei.DotNet2.Utilities.WinForm
{
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Tooltip 帮助类
    /// </summary>
    public static class TooltipHelper
    {
        #region Methods

        /// <summary>
        ///  ToolTip——Error
        /// </summary>
        /// <typeparam name="T">Control</typeparam>
        /// <param name="t">Control</param>
        /// <param name="tooltip">ToolTip</param>
        /// <param name="titile">标题</param>
        /// <param name="content">内容</param>
        public static void ShowErrorToolTip<T>(this T t, ToolTip tooltip, string titile, string content)
            where T : Control
        {
            t.ShowBaseToolTip<T>(tooltip, ToolTipIcon.Error, titile, content);
        }

        /// <summary>
        ///  ToolTip——Info
        /// </summary>
        /// <typeparam name="T">Control</typeparam>
        /// <param name="t">Control</param>
        /// <param name="tooltip">ToolTip</param>
        /// <param name="titile">标题</param>
        /// <param name="content">内容</param>
        public static void ShowInfoToolTip<T>(this T t, ToolTip tooltip, string titile, string content)
            where T : Control
        {
            t.ShowBaseToolTip<T>(tooltip, ToolTipIcon.Info, titile, content);
        }

        /// <summary>
        ///  ToolTip——Normal
        /// </summary>
        /// <typeparam name="T">Control</typeparam>
        /// <param name="t">Control</param>
        /// <param name="tooltip">ToolTip</param>
        /// <param name="content">内容</param>
        public static void ShowNormalToolTip<T>(this T t, ToolTip tooltip, string content)
            where T : Control
        {
            t.ShowBaseToolTip<T>(tooltip, ToolTipIcon.None, string.Empty, content);
        }

        /// <summary>
        /// 为控件提供Tooltip
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="tip">ToolTip</param>
        /// <param name="message">提示消息</param>
        public static void ShowTooltip(this Control control, ToolTip tip, string message)
        {
            Point _mousePoint = Control.MousePosition;
            int _x = control.PointToClient(_mousePoint).X;
            int _y = control.PointToClient(_mousePoint).Y;
            tip.Show(message, control, _x, _y);
            tip.Active = true;
        }

        /// <summary>
        /// 为控件提供Tooltip
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="tip">ToolTip</param>
        /// <param name="message">提示消息</param>
        /// <param name="durationTime">保持提示的持续时间</param>
        public static void ShowTooltip(this Control control, ToolTip tip, string message, int durationTime)
        {
            Point _mousePoint = Control.MousePosition;
            int _x = control.PointToClient(_mousePoint).X;
            int _y = control.PointToClient(_mousePoint).Y;
            tip.Show(message, control, _x, _y, durationTime);
            tip.Active = true;
        }

        /// <summary>
        /// 为控件提供Tooltip
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="tip">ToolTip</param>
        /// <param name="message">提示消息</param>
        /// <param name="xoffset">水平偏移量</param>
        /// <param name="yoffset">垂直偏移量</param>
        public static void ShowTooltip(this Control control, ToolTip tip, string message, int xoffset, int yoffset)
        {
            Point _mousePoint = Control.MousePosition;
            int _x = control.PointToClient(_mousePoint).X;
            int _y = control.PointToClient(_mousePoint).Y;
            tip.Show(message, control, _x + xoffset, _y + yoffset);
            tip.Active = true;
        }

        /// <summary>
        /// 为控件提供Tooltip
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="tip">ToolTip</param>
        /// <param name="message">提示消息</param>
        /// <param name="xoffset">水平偏移量</param>
        /// <param name="yoffset">垂直偏移量</param>
        /// <param name="durationTime">保持提示的持续时间</param>
        public static void ShowTooltip(this Control control, ToolTip tip, string message, int xoffset, int yoffset, int durationTime)
        {
            Point _mousePoint = Control.MousePosition;
            int _x = control.PointToClient(_mousePoint).X;
            int _y = control.PointToClient(_mousePoint).Y;
            tip.Show(message, control, _x + xoffset, _y + yoffset, durationTime);
            tip.Active = true;
        }

        /// <summary>
        ///  ToolTip——Warning
        /// </summary>
        /// <typeparam name="T">Control</typeparam>
        /// <param name="t">Control</param>
        /// <param name="tooltip">ToolTip</param>
        /// <param name="titile">标题</param>
        /// <param name="content">内容</param>
        public static void ShowWarningToolTip<T>(this T t, ToolTip tooltip, string titile, string content)
            where T : Control
        {
            t.ShowBaseToolTip<T>(tooltip, ToolTipIcon.Warning, titile, content);
        }

        private static ToolTip ShowBaseToolTip<T>(this T t, ToolTip tooltip, ToolTipIcon toolTipIcon, string titile, string content)
            where T : Control
        {
            tooltip.Active = false;
            tooltip.Active = true;
            tooltip.ToolTipIcon = toolTipIcon;
            tooltip.ToolTipTitle = titile;
            tooltip.IsBalloon = true;
            tooltip.Show(content, t, 3000);
            return tooltip;
        }

        #endregion Methods
    }
}