namespace YanZhiwei.DotNet2.Utilities.WinForm
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Threading;
    using System.Windows.Forms;

    /// <summary>
    /// WinForm语言国际化帮助类
    /// </summary>
    /// 时间：2015-09-02 16:45
    /// 备注：
    public static class LanguageHelper
    {
        #region Methods

        /// <summary>
        /// 设置界面国际化
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="form">需要国际化的界面</param>
        /// <param name="lang">language:zh-CN, en-US</param>
        /// 时间：2015-09-02 16:44
        /// 备注：
        public static void SetGlobalization<T>(this T form, string lang)
            where T : Form
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            if (form != null)
            {
                Type _formType = form.GetType();
                ComponentResourceManager _resources = new ComponentResourceManager(_formType);
                _resources.ApplyResources(form, "$this");
                ApplyGlobalization(form, _resources);
            }
        }

        private static void ApplyGlobalization(Control control, ComponentResourceManager resources)
        {
            if (control is MenuStrip)
            {
                resources.ApplyResources(control, control.Name);
                MenuStrip ms = (MenuStrip)control;
                if (ms.Items.Count > 0)
                {
                    foreach (ToolStripMenuItem c in ms.Items)
                    {
                        ApplyGlobalization(c, resources);
                    }
                }
            }
            foreach (Control c in control.Controls)
            {
                resources.ApplyResources(c, c.Name);
                ApplyGlobalization(c, resources);
            }
        }

        private static void ApplyGlobalization(ToolStripMenuItem item, ComponentResourceManager resources)
        {
            if (item is ToolStripMenuItem)
            {
                resources.ApplyResources(item, item.Name);
                ToolStripMenuItem _tsmi = (ToolStripMenuItem)item;
                if (_tsmi.DropDownItems.Count > 0)
                {
                    foreach (ToolStripMenuItem c in _tsmi.DropDownItems)
                    {
                        ApplyGlobalization(c, resources);
                    }
                }
            }
        }

        #endregion Methods
    }
}