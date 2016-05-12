namespace YanZhiwei.DotNet2.Utilities.WinForm
{
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// RichTextBox 帮助类
    /// </summary>
    public static class RichTextBoxHelper
    {
        #region Methods

        /// <summary>
        /// 设置RichTextBox的值，并且设置焦点最最后
        /// </summary>
        /// <param name="richText">RichTextBox</param>
        /// <param name="text">文本内容</param>
        /// <param name="splitCharacter">分隔符</param>
        public static void SetTextFocused(this RichTextBox richText, string text, string splitCharacter)
        {
            StringBuilder _richTextMsg = new StringBuilder();
            _richTextMsg.Append(text);
            richText.Text = _richTextMsg.ToString().Trim();
            richText.SelectionStart = _richTextMsg.Length;
            richText.Focus();
            if (!string.IsNullOrEmpty(splitCharacter))
            {
                _richTextMsg.Append(splitCharacter);
            }
        }

        #endregion Methods
    }
}