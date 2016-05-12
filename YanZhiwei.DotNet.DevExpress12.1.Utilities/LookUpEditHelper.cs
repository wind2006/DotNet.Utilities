namespace YanZhiwei.DotNet.DevExpress12._1.Utilities
{
    using System.Windows.Forms;

    using DevExpress.Utils;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;

    /// <summary>
    /// LookUpEdit帮助类
    /// </summary>
    public static class LookUpEditHelper
    {
        #region Methods

        /// <summary>
        /// 为LookUpEdit添加删除按钮
        /// </summary>
        /// <param name="lue">LookUpEdit</param>
        /// <param name="prompttext">删除按钮提示文字</param>
        public static void AddDeleteButton(this LookUpEdit lue, string prompttext)
        {
            prompttext = string.IsNullOrEmpty(prompttext) ? "删除选中项" : prompttext;
            lue.Properties.Buttons.AddRange(new EditorButton[]
            {
                new EditorButton(
                    ButtonPredefines.Delete,
                    "删除", -1, true, true, false, ImageLocation.MiddleCenter,
                    null,
                    new KeyShortcut(Keys.Delete),
                    new SerializableAppearanceObject(),
                    prompttext,
                    "Delete",
                    null,
                    true)
            });
            lue.ButtonClick += (sender, e) =>
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    LookUpEdit _curLue = sender as LookUpEdit;
                    _curLue.EditValue = null;
                }
            };
        }

        /// <summary>
        /// 自动完成功能绑定实现
        /// </summary>
        /// <param name="lue">LookUpEdit</param>
        /// <param name="source">数据源</param>
        /// <param name="value">隐式字段</param>
        /// <param name="displayName">显示字段</param>
        /// <param name="prompttext">提示文字</param>
        public static void BindWithAutoCompletion(this LookUpEdit lue, object source, string value, string displayName, string prompttext)
        {
            lue.Properties.DataSource = source;
            lue.Properties.DisplayMember = displayName;
            lue.Properties.ValueMember = value;
            lue.Properties.NullText = prompttext;
            lue.Properties.TextEditStyle = TextEditStyles.Standard;
            lue.Properties.SearchMode = SearchMode.AutoFilter;
        }

        #endregion Methods
    }
}