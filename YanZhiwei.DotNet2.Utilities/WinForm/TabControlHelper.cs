namespace YanZhiwei.DotNet2.Utilities.WinForm
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    /// <summary>
    /// TabControl 帮助类
    /// </summary>
    public class TabControlHelper
    {
        #region Fields

        /// <summary>
        /// TabPge集合
        /// </summary>
        public readonly List<TabPage> TabPageList = null;

        /// <summary>
        /// TabControl对象
        /// </summary>
        private TabControl tabControl = null;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TabControlHelper"/> class.
        /// </summary>
        /// <param name="tabControl">The tab control.</param>
        public TabControlHelper(TabControl tabControl)
        {
            TabPageList = new List<TabPage>();
            this.tabControl = tabControl;
            foreach (TabPage tp in tabControl.TabPages)
            {
                TabPageList.Add(tp);
            }
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// 显示TabPage
        /// </summary>
        /// <param name="tabpage">需要显示的TabPage</param>
        public void Show(TabPage tabpage)
        {
            if (tabpage != null)
            {
                tabControl.TabPages.Clear();
                tabControl.TabPages.Add(tabpage);
            }
        }

        /// <summary>
        ///  显示TabPage
        /// </summary>
        /// <param name="showTabpageHanlder">委托</param>
        public void Show(Predicate<TabPage> showTabpageHanlder)
        {
            TabPage _finded = null;
            foreach (TabPage tp in TabPageList)
            {
                if (showTabpageHanlder(tp))
                {
                    _finded = tp;
                    break;
                }
            }

            Show(_finded);
        }

        /// <summary>
        /// 显示TabPage
        /// </summary>
        /// <param name="tabpage">需要显示的TabPage集合</param>
        public void Show(params TabPage[] tabpage)
        {
            tabControl.TabPages.Clear();
            tabControl.TabPages.AddRange(tabpage);
        }

        #endregion Methods
    }
}