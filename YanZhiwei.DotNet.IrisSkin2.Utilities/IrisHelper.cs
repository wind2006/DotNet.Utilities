using Sunisoft.IrisSkin;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace YanZhiwei.DotNet.IrisSkin2.Utilities
{
    /// <summary>
    /// Iris 帮助类
    /// </summary>
    public class IrisHelper
    {
        #region 高亮按钮控件

        /// <summary>
        /// 高亮按钮控件
        /// </summary>
        /// <typeparam name="T">诸如panl</typeparam>
        /// <param name="t">诸如panl</param>
        /// <param name="button">需要高亮的按钮</param>
        /// <param name="color1">color1</param>
        /// <param name="color2">color2</param>
        /// <param name="fontColor">字体颜色</param>
        public static void HighlightButtonColor<T>(T t, Button button, Color color1, Color color2, Color fontColor)
            where T : Control
        {
            foreach (Control ct in t.Controls)
            {
                if (ct is Button)
                {
                    Button _button = (Button)ct;
                    if (_button.Name == button.Name)
                        ChangControlColor<Button>(button, color1, color2, fontColor);
                    else
                        RestoreButtonColor(_button);
                }
            }
        }

        #endregion 高亮按钮控件

        #region 高亮控件颜色『需要控件有tag属性』

        private const int DisableTag = 9999;

        /// <summary>
        /// 高亮控件颜色『需要控件有tag属性』
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="color1"></param>
        /// <param name="color2"></param>
        /// <param name="fontColor"></param>
        public static void ChangControlColor<T>(T t, Color color1, Color color2, Color fontColor) where T : Control
        {
            Bitmap _bmp = new Bitmap(t.Width, t.Height);
            using (Graphics _g = Graphics.FromImage(_bmp))
            {
                Rectangle _r = new Rectangle(0, 0, _bmp.Width, _bmp.Height);
                using (LinearGradientBrush br = new LinearGradientBrush(
                                                    _r,
                                                    color1,
                                                    color2,
                                                    LinearGradientMode.Vertical))
                {
                    _g.FillRectangle(br, _r);
                }
            }
            t.BackgroundImage = _bmp;
            t.ForeColor = fontColor;
            t.Tag = DisableTag;
        }

        #endregion 高亮控件颜色『需要控件有tag属性』

        #region 还原按钮默认主题

        /// <summary>
        /// 还原按钮默认主题
        /// </summary>
        /// <param name="button">Button</param>
        public static void RestoreButtonColor(Button button)
        {
            button.UseVisualStyleBackColor = true;
            button.Tag = null;
        }

        #endregion 还原按钮默认主题

        #region 设置程序主题

        /// <summary>
        /// 设置程序主题
        /// </summary>
        /// <param name="skin">SkinEngine</param>
        /// <param name="bytes">byte数组</param>
        public static void SetupTheme(SkinEngine skin, byte[] bytes)
        {
            if (skin == null || bytes == null)
                throw new ArgumentNullException();
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                skin.SkinStream = memoryStream;
            }
        }

        #endregion 设置程序主题
    }
}