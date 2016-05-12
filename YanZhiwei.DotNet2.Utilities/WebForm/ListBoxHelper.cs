namespace YanZhiwei.DotNet2.Utilities.WebForm
{
    using System.Web.UI.WebControls;

    /// <summary>
    /// ListBox 帮助类
    /// </summary>
    public static class ListBoxHelper
    {
        #region Methods

        /// <summary>
        /// 增加ListBox的Tooltip提示
        /// </summary>
        /// <param name="lb">ListBox</param>
        public static void AddItemTooltip(this ListBox lb)
        {
            for (int i = 0; i < lb.Items.Count; i++)
            {
                lb.Items[i].Attributes.Add("title", lb.Items[i].Text);
            }
        }

        #endregion Methods
    }
}