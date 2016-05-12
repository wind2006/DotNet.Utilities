namespace YanZhiwei.DotNet.DevExpress12._1.Utilities
{
    using System.Drawing;
    using System.Text;

    /// <summary>
    /// HTML Text Formatting 帮助类
    /// </summary>
    public static class HTMLTextFormattingHelper
    {
        #region Methods

        /*
         * 参考：
         * 1. https://documentation.devexpress.com/#WindowsForms/CustomDocument4874
         */
        /// <summary>
        /// 添加换行标签
        /// </summary>
        /// <param name="data">字符串</param>
        /// <returns>操作后的字符串</returns>
        public static string AddBRTag(this string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                StringBuilder _builder = new StringBuilder();
                _builder.Append(data);
                _builder.Append("<br>");
                data = _builder.ToString();
            }
            return data;
        }

        /// <summary>
        /// 添加加粗标签
        /// </summary>
        /// <param name="data">字符串</param>
        /// <param name="bContent">需要加粗的内容</param>
        /// <returns>操作后的字符串</returns>
        public static string AddBTag(this string data, string bContent)
        {
            if (!string.IsNullOrEmpty(data))
            {
                StringBuilder _builder = new StringBuilder();
                _builder.Append(data);
                _builder.AppendFormat("<b>{0}</b>", bContent);
                data = _builder.ToString();
            }
            return data;
        }

        /// <summary>
        /// 添加Href 标记
        /// </summary>
        /// <param name="data">字符串</param>
        /// <param name="url">超链接</param>
        /// <param name="urlText">超链接文本</param>
        /// <returns>操作后的字符串</returns>
        public static string AddHrefTag(this string data, string url, string urlText)
        {
            if (!string.IsNullOrEmpty(data))
            {
                StringBuilder _builder = new StringBuilder();
                _builder.Append(data);
                _builder.AppendFormat("<href={0}>{1}</href>", url, urlText);
                data = _builder.ToString();
            }
            return data;
        }

        /// <summary>
        /// 设置背景色
        /// </summary>
        /// <param name="data">字符串</param>
        /// <param name="color">Color</param>
        /// <returns>操作后的字符串</returns>
        public static string SetbackColor(this string data, Color color)
        {
            if (!string.IsNullOrEmpty(data))
                data = string.Format("<backcolor={0}>{1}</color>", color.Name, data);
            return data;
        }

        /// <summary>
        /// 设置颜色
        /// </summary>
        /// <param name="data">字符串</param>
        /// <param name="color">Color</param>
        /// <returns>操作后的字符串</returns>
        public static string SetColor(this string data, Color color)
        {
            if (!string.IsNullOrEmpty(data))
                data = string.Format("<color={0}>{1}</color>", color.Name, data);
            return data;
        }

        /// <summary>
        /// 设置字体大小
        /// </summary>
        /// <param name="data">字符串</param>
        /// <param name="size">字体大小</param>
        /// <returns>操作后的字符串</returns>
        public static string SetFontSize(this string data, int size)
        {
            if (!string.IsNullOrEmpty(data))
                data = string.Format("<size={0}>{1}</color>", size, data);
            return data;
        }

        #endregion Methods
    }
}