namespace YanZhiwei.DotNet.DevExpress12._1.Utilities
{
    using DevExpress.XtraTab;

    public static class TabControlHelper
    {
        #region Methods

        /// <summary>
        /// 显示TabPage
        /// </summary>
        /// <param name="tabControl">TabControl对象</param>
        /// <param name="tabpage">需要显示的TabPage</param>
        public static void SetTabPagesVisible(this XtraTabControl tabControl, XtraTabPage tabpage)
        {
            tabControl.TabPages.Clear();
            tabControl.TabPages.Add(tabpage);
        }

        /// <summary>
        /// 显示TabPage
        /// </summary>
        /// <param name="tabControl">TabControl对象</param>
        /// <param name="tabpage">需要显示的TabPage集合</param>
        public static void SetTabPagesVisible(this XtraTabControl tabControl, params XtraTabPage[] tabpage)
        {
            tabControl.TabPages.Clear();
            tabControl.TabPages.AddRange(tabpage);
        }

        #endregion Methods
    }
}