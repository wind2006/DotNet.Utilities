namespace YanZhiwei.DotNet2.Utilities.WinForm
{
    using System.Windows.Forms;

    /// <summary>
    /// ListBox 帮助类
    /// </summary>
    public static class ListBoxHelper
    {
        #region Methods

        /// <summary>
        /// ITEM下移动，适合不是datasource绑定情况
        /// </summary>
        /// <param name="lsbox">ListBox</param>
        public static void ItemMoveDown(this ListBox lsbox)
        {
            int _itemCnt = lsbox.Items.Count;
            int _selectedIndex = lsbox.SelectedIndex;
            if (_itemCnt > _selectedIndex && _selectedIndex < _itemCnt - 1)
            {
                object _selectedItem = lsbox.SelectedItem;
                lsbox.Items.RemoveAt(_selectedIndex);
                lsbox.Items.Insert(_selectedIndex + 1, _selectedItem);
                lsbox.SelectedIndex = _selectedIndex + 1;
            }
        }

        /// <summary>
        /// ITEM上移动，适合不是datasource绑定情况
        /// </summary>
        /// <param name="lsbox">ListBox</param>
        public static void ItemMoveUp(this ListBox lsbox)
        {
            int _itemCnt = lsbox.Items.Count;
            int _selectedIndex = lsbox.SelectedIndex;
            if (_itemCnt > _selectedIndex && _selectedIndex > 0)
            {
                object _selectedItem = lsbox.SelectedItem;
                lsbox.Items.RemoveAt(_selectedIndex);
                lsbox.Items.Insert(_selectedIndex - 1, _selectedItem);
                lsbox.SelectedIndex = _selectedIndex - 1;
            }
        }

        #endregion Methods
    }
}