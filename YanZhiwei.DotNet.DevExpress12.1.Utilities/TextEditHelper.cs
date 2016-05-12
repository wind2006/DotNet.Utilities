namespace YanZhiwei.DotNet.DevExpress12._1.Utilities
{
    using DevExpress.XtraEditors;

    /// <summary>
    /// TextEdit的帮助类
    /// </summary>
    public static class TextEditHelper
    {
        #region Methods

        /// <summary>
        /// 清除TextEdit的水印文字
        /// </summary>
        /// <param name="textEdit">TextEdit</param>
        public static void ClearWatermark(this TextEdit textEdit)
        {
            if (textEdit.Properties.NullValuePromptShowForEmptyValue)
                textEdit.Properties.NullValuePrompt = string.Empty;
        }

        /// <summary>
        /// 为TextEdit设置水印文字
        /// </summary>
        /// <param name="textEdit">TextEdit</param>
        /// <param name="watermark">水印文字</param>
        public static void SetWatermark(this TextEdit textEdit, string watermark)
        {
            textEdit.Properties.NullValuePromptShowForEmptyValue = true;
            textEdit.Properties.NullValuePrompt = watermark;
        }

        #endregion Methods
    }
}