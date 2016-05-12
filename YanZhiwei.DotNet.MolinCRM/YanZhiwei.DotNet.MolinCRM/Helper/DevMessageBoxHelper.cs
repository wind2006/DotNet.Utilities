using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace Molin_CRM.Helper
{
    /// <summary>
    ///devexpress消息框帮助类
    /// </summary>
    public class DevMessageBoxHelper
    {
        #region 一般提示信息

        /// <summary>
        /// 一般提示信息
        /// </summary>
        public static DialogResult ShowInfo(string message)
        {
            return ShowInfo(message, "提示信息");
        }

        /// <summary>
        /// 一般提示信息
        /// </summary>
        public static DialogResult ShowInfo(string message, string caption)
        {
            return XtraMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion 一般提示信息

        #region 警告信息

        /// <summary>
        /// 警告信息
        /// </summary>
        public static DialogResult ShowWarning(string message, string caption)
        {
            return XtraMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 警告信息
        /// </summary>
        public static DialogResult ShowWarning(string message)
        {
            return ShowWarning(message, "警告信息");
        }

        #endregion 警告信息

        #region 错误信息

        /// <summary>
        /// 错误信息
        /// </summary>
        public static DialogResult ShowError(string message, string caption)
        {
            return XtraMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public static DialogResult ShowError(string message)
        {
            return ShowError(message, "错误信息");
        }

        #endregion 错误信息

        #region 显示询问用户信息，并显示错误标志

        /// <summary>
        /// 显示询问用户信息，并显示错误标志
        /// </summary>
        /// <param name="message">错误信息</param>
        public static DialogResult ShowYesNoAndError(string message)
        {
            return XtraMessageBox.Show(message, "错误信息", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
        }

        #endregion 显示询问用户信息，并显示错误标志

        #region 显示询问用户信息，并显示提示标志

        /// <summary>
        /// 显示询问用户信息，并显示提示标志
        /// </summary>
        public static DialogResult ShowYesNoAndTips(string message)
        {
            return XtraMessageBox.Show(message, "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }

        #endregion 显示询问用户信息，并显示提示标志

        #region 显示询问用户信息，并显示警告标志

        /// <summary>
        /// 显示询问用户信息，并显示警告标志
        /// </summary>
        public static DialogResult ShowYesNoAndWarning(string message)
        {
            return XtraMessageBox.Show(message, "警告信息", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        #endregion 显示询问用户信息，并显示警告标志

        #region 显示询问用户信息，并显示提示标志

        /// <summary>
        /// 显示询问用户信息，并显示提示标志
        /// </summary>
        public static DialogResult ShowYesNoCancelAndInfo(string message)
        {
            return XtraMessageBox.Show(message, "提示信息", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
        }

        #endregion 显示询问用户信息，并显示提示标志
    }
}