using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.Collections;

namespace Molin_CRM.Helper
{
    /// <summary>
    /// ComboBoxEdit 帮助类
    /// </summary>
    public static class ComboBoxEditHelper
    {
        #region 设置ComboBoxEdit 数据源

        /// <summary>
        /// 设置ComboBoxEdit 数据源
        /// </summary>
        /// <param name="combox">ComboBoxEdit</param>
        /// <param name="data">ICollection</param>
        /// <param name="promptMessage">下拉提示信息</param>
        public static void SetDataSource(this ComboBoxEdit combox, ICollection data, string promptMessage)
        {
            combox.Properties.Items.AddRange(data);
            combox.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            if (!string.IsNullOrEmpty(promptMessage))
                combox.Properties.Items.Insert(0, promptMessage);
            combox.SelectedIndex = 0;
        }

        #endregion 设置ComboBoxEdit 数据源
    }
}