using DevExpress.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Molin_CRM.Helper
{
    /// <summary>
    /// ToolTip帮助类
    /// </summary>
    public static class ToolTipHelper
    {
        #region 展现ToolTip

        /// <summary>
        /// 展现ToolTip
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="t">控件</param>
        /// <param name="toolTip">ToolTipController</param>
        /// <param name="contnent">ToolTip内容</param>
        /// <param name="toolTipRule">委托</param>
        public static void ShowToolTip<T>(this T t, ToolTipController toolTip, string contnent, Action<ToolTipController> toolTipRule) where T : Control
        {
            toolTip.ShowBeak = true;
            toolTip.ShowShadow = true;
            toolTip.Rounded = true;
            toolTip.SetToolTipIconType(t, ToolTipIconType.Exclamation);
            if (toolTipRule != null)
                toolTipRule(toolTip);
            toolTip.ShowHint(contnent, t, ToolTipLocation.RightCenter);
        }

        /// <summary>
        /// 展现ToolTip
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="t">控件</param>
        /// <param name="toolTip">ToolTipController</param>
        /// <param name="contnent">ToolTip内容</param>
        public static void ShowToolTip<T>(this T t, ToolTipController toolTip, string content) where T : Control
        {
            ShowToolTip(t, toolTip, content, null);
        }

        /// <summary>
        ///  展现ToolTip
        /// </summary>
        /// <param name="toolTip">ToolTipController</param>
        /// <param name="title">ToolTip标题</param>
        /// <param name="contnent">ToolTip内容</param>
        /// <param name="point">Point</param>
        /// <param name="toolTipRule">委托</param>
        public static void ShowToolTip(ToolTipController toolTip, string title, string content, Point point, Action<ToolTipController> toolTipRule)
        {
            ToolTipControllerShowEventArgs _args = toolTip.CreateShowArgs();
            toolTip.ShowBeak = true;
            toolTip.ShowShadow = true;
            toolTip.Rounded = true;
            _args.Title = title;
            _args.ToolTip = content;
            _args.Rounded = true;
            _args.ToolTipType = ToolTipType.Default;
            if (toolTipRule != null)
                toolTipRule(toolTip);
            toolTip.ShowHint(_args, point);
        }

        /// <summary>
        ///  展现ToolTip
        /// </summary>
        /// <param name="toolTip">ToolTipController</param>
        /// <param name="title">ToolTip标题</param>
        /// <param name="contnent">ToolTip内容</param>
        /// <param name="point">Point</param>
        public static void ShowToolTip(ToolTipController toolTip, string title, string content, Point point)
        {
            ShowToolTip(toolTip, title, content, point, null);
        }

        /// <summary>
        ///  展现ToolTip
        /// </summary>
        /// <param name="toolTip">ToolTipController</param>
        /// <param name="title">ToolTip标题</param>
        /// <param name="contnent">ToolTip内容</param>
        public static void ShowToolTip(this ToolTipController toolTip, string title, string content)
        {
            Point _mousePoint = Control.MousePosition;
            toolTip.ShowHint(content, title, _mousePoint);
        }

        #endregion 展现ToolTip
    }
}