using Microsoft.International.Converters.PinYinConverter;
using System.Text;

namespace YanZhiwei.DotNet.International.Utilities
{
    /// <summary>
    /// 中文拼音操作帮助类
    /// </summary>
    public class PinYinHelper
    {
        /// <summary>
        /// 获取中文全拼
        /// </summary>
        /// <param name="chinese">中文</param>
        /// <returns>全拼</returns>
        public static string GetAllPinYin(string chinese)
        {
            StringBuilder _builder = new StringBuilder();
            foreach (char c in chinese)
            {
                _builder.Append(SpecialAllTrans(c));
            }
            return _builder.ToString();
        }

        /// <summary>
        /// 获取中文简拼
        /// <para>eg:  Assert.AreEqual("YZW", PinYinHelper.GetSimplePinYin("言志伟"));</para>
        /// </summary>
        /// <param name="chinese">中文</param>
        /// <returns>简拼</returns>
        public static string GetSimplePinYin(string chinese)
        {
            StringBuilder _pinyin = new StringBuilder();
            char[] _chineses = chinese.ToCharArray();
            foreach (char c in _chineses)
            {
                _pinyin.Append(SpecialSimpleTrans(c));
            }
            return _pinyin.ToString();
        }

        /// <summary>
        /// 特殊处理
        /// </summary>
        /// <param name="chinese"></param>
        /// <returns></returns>
        private static string SpecialAllTrans(char chinese)
        {
            string _pinyin = string.Empty;
            if (ChineseChar.IsValidChar(chinese) && !char.IsWhiteSpace(chinese))
            {
                switch (chinese)
                {
                    case '区':
                        _pinyin = "QU";
                        break;

                    case '家':
                        _pinyin = "JIA";
                        break;

                    case '弄':
                        _pinyin = "NONG";
                        break;

                    case '藏':
                        _pinyin = "ZANG";
                        break;

                    default:
                        ChineseChar _chineseChar = new ChineseChar(chinese);
                        _pinyin = _chineseChar.Pinyins[0].Substring(0, _chineseChar.Pinyins[0].Length - 1);
                        break;
                }
            }

            return _pinyin;
        }

        /// <summary>
        /// 特殊处理
        /// </summary>
        /// <param name="chinese">中文</param>
        /// <returns>简拼</returns>
        private static string SpecialSimpleTrans(char chinese)
        {
            string _pinyin = string.Empty;
            if (ChineseChar.IsValidChar(chinese) && !char.IsWhiteSpace(chinese))
            {
                switch (chinese)
                {
                    case '区':
                        _pinyin = "Q";//QU
                        break;

                    case '家':
                        _pinyin = "J";//JIA
                        break;

                    case '弄':
                        _pinyin = "N";//NONG
                        break;

                    case '藏':
                        _pinyin = "Z";//ZANG
                        break;

                    default:
                        _pinyin = (new ChineseChar(chinese).Pinyins[0]).Substring(0, 1);
                        break;
                }
            }
            else
            {
                _pinyin = chinese.ToString().ToUpper();
            }
            return _pinyin;
        }
    }
}