namespace YanZhiwei.DotNet.DevExpress12._1.Utilities
{
    using System.Drawing;
    using System.Windows.Forms;

    using DevExpress.Utils;
    using DevExpress.XtraEditors;

    using YanZhiwei.DotNet2.Utilities.Common;
    using YanZhiwei.DotNet2.Utilities.Enums;

    /// <summary>
    /// ListBox帮助类
    /// </summary>
    public static class ListBoxHelper
    {
        #region Methods

        /// <summary>
        /// 设置item颜色，注意需设置属性：listbox.AllowHtmlDraw = DefaultBoolean.True;才可生效
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="color">颜色</param>
        /// <returns>格式化后的颜色</returns>
        public static string FormatColor(string message, Color color)
        {
            if (!string.IsNullOrEmpty(message))
                message = string.Format("<color={0}>{1}</color>", color.Name, message);
            return message;
        }

        /// <summary>
        /// ListBoxControl插入ITEM并选中，【线程安全】
        /// </summary>
        /// <param name="listbox">ListBoxControl</param>
        /// <param name="message">插入内容</param>
        public static void InsertItemWithSelect(this ListBoxControl listbox, string message)
        {
            if (!string.IsNullOrEmpty(message) && listbox != null)
            {
                listbox.Items.Add(message);
                listbox.SelectedIndex = listbox.Items.Count - 1;
            }
        }

        /// <summary>
        /// 为LISTBOX的ITEM设置toolTip
        /// </summary>
        /// <param name="listbox">ListBoxControl</param>
        /// <param name="e">MouseEventArgs</param>
        /// <param name="toolTip">ToolTipController</param>
        /// <param name="title">title</param>
        public static void ShowToolTip(this ListBoxControl listbox, MouseEventArgs e, ToolTipController toolTip, string title)
        {
            int _selectedIndex = listbox.IndexFromPoint(new Point(e.X, e.Y));
            if (_selectedIndex != -1)
            {
                object _itemContent = listbox.Items[_selectedIndex];
                if (_itemContent != null)
                {
                    string _item = _itemContent.ToString();
                    _item = HtmlHelper.StripTags(_item, StripHtmlType.CharArray);
                    ToolTipHelper.ShowToolTip(toolTip, title, _item, listbox.PointToScreen(e.Location));
                }
            }
        }

        #endregion Methods
    }
}