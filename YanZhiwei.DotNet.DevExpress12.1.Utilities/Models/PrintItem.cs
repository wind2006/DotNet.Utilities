using System.Drawing;
using System.Drawing.Printing;

namespace YanZhiwei.DotNet.DevExpress12._1.Utilities.Models
{
    /// <summary>
    /// 打印设置类
    /// </summary>
    public class PrintItem
    {
        private PaperKind _paperKind;

        /// <summary>
        /// 纸张类型，默认A3
        /// </summary>
        public PaperKind PaperKind
        {
            get
            {
                if (_paperKind == default(PaperKind))
                    _paperKind = PaperKind.A3;
                return _paperKind;
            }
            set
            {
                _paperKind = value;
            }
        }

        private Color _footerColor;

        /// <summary>
        /// 页脚字体颜色，默认DarkBlue
        /// </summary>
        public Color FooterColor
        {
            get
            {
                if (_footerColor == default(Color))
                    _footerColor = Color.DarkBlue;
                return _footerColor;
            }
            set
            {
                _footerColor = value;
            }
        }

        private Color _headerColor;

        /// <summary>
        /// 页眉字体颜色，默认DarkBlue
        /// </summary>
        public Color HeaderColor
        {
            get
            {
                if (_headerColor == default(Color))
                    _headerColor = Color.DarkBlue;
                return _headerColor;
            }
            set
            {
                _headerColor = value;
            }
        }

        /// <summary>
        /// 是否设置打印页眉,默认false
        /// </summary>
        public bool PrintHeader { get; set; }

        /// <summary>
        /// 页眉文字
        /// </summary>
        public string HeaderText { get; set; }

        private Font _headerFont;

        /// <summary>
        /// 页眉字体，默认 Font("Tahoma", 12, FontStyle.Bold);
        /// </summary>
        public Font HeaderFont
        {
            get
            {
                if (_headerFont == null)
                    _headerFont = new Font("Tahoma", 12, FontStyle.Bold);
                return _headerFont;
            }
            set
            {
                _headerFont = value;
            }
        }

        /// <summary>
        /// 是否打印页脚，默认false
        /// </summary>
        public bool PrintFooter { get; set; }

        /// <summary>
        /// 页脚文字
        /// </summary>
        public string FooterText { get; set; }

        private Font _footerFont;

        /// <summary>
        /// 页脚字体，默认Font("Tahoma", 12, FontStyle.Bold);
        /// </summary>
        public Font FooterFont
        {
            get
            {
                if (_footerFont == null)
                    _footerFont = new Font("Tahoma", 12, FontStyle.Bold);
                return _footerFont;
            }
            set
            {
                _footerFont = value;
            }
        }
    }
}