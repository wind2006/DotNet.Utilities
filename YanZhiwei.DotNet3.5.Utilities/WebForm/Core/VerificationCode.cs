namespace YanZhiwei.DotNet3._5.Utilities.WebForm.Core
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Web;

    /// <summary>
    /// 验证码类
    /// </summary>
    public class VerificationCode
    {
        #region Fields

        /// <summary>
        /// 是否加入小写字母
        /// </summary>
        public bool SetAddLowerLetter = false;

        /// <summary>
        /// 是否加入大写字母
        /// </summary>
        public bool SetAddUpperLetter = false;

        /// <summary>
        /// 背景色
        /// </summary>
        public Color SetBackgroundColor = Color.AliceBlue;

        /// <summary>
        ///  字体颜色
        /// </summary>
        public Color SetFontColor = Color.Blue;

        /// <summary>
        /// 字体类型
        /// </summary>
        public string SetFontFamily = "Verdana";

        /// <summary>
        /// 字体大小
        /// </summary>
        public int SetFontSize = 18;

        /// <summary>
        /// 前景噪点数量
        /// </summary>
        public int SetForeNoisePointCount = 2;

        /// <summary>
        /// 是否随机字体颜色
        /// </summary>
        public bool SetIsRandomColor = false;

        /// <summary>
        /// 验证码长度
        /// </summary>
        public int SetLength = 4;

        /// <summary>
        /// 随机码的旋转角度
        /// </summary>
        public int SetRandomAngle = 40;

        private Random objRandom = new Random();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public VerificationCode()
        {
            this.GetVerifyCodeText();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 验证码字符串
        /// </summary>
        public string SetVerifyCodeText
        {
            get; set;
        }

        /// <summary>
        /// 图片高度
        /// </summary>
        private int SetHeight
        {
            get
            {
                return Convert.ToInt32((60.0 / 100) * SetFontSize + SetFontSize);
            }
        }

        /// <summary>
        /// 图片宽度
        /// </summary>
        private int SetWith
        {
            get
            {
                return this.SetVerifyCodeText.Length * SetFontSize;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 获取问题
        /// </summary>
        /// <param name="questionList">默认数字加减验证</param>
        /// <returns>返回随机问题</returns>
        public KeyValuePair<string, string> GetQuestion(Dictionary<string, string> questionList)
        {
            if (questionList == null)
            {
                questionList = new Dictionary<string, string>();
                string[] _operArray = new string[] { "+", "*", "num" };
                int _left = objRandom.Next(0, 10),
                    _right = objRandom.Next(0, 10);
                string _oper = _operArray[objRandom.Next(0, _operArray.Length)];
                if (_oper == "+")
                {
                    string _key = string.Format("{0}+{1}=?", _left, _right),
                           _val = (_left + _right).ToString();
                    questionList.Add(_key, _val);
                }
                else if (_oper == "*")
                {
                    string _key = string.Format("{0}×{1}=?", _left, _right),
                           _val = (_left * _right).ToString();
                    questionList.Add(_key, _val);
                }
                else
                {
                    int _num = objRandom.Next(1000, 9999); ;
                    questionList.Add(_num.ToString(), _num.ToString());
                }
            }
            return questionList.ToList()[objRandom.Next(0, questionList.Count)];
        }

        /// <summary>
        /// 输出验证码图片
        /// </summary>
        /// <param name="objHttpResponse">Http响应实例</param>
        /// <returns>输出是否成功</returns>
        public bool OutputImage(HttpResponse objHttpResponse)
        {
            bool _result = false;
            if (this.SetIsRandomColor)
            {
                this.SetFontColor = GetRandomColor(); ;
            }

            using (Bitmap bmap = this.GetVerifyCodeImage())
            {
                if (bmap != null)
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        bmap.Save(stream, ImageFormat.Jpeg);

                        HttpContext.Current.Response.ClearContent();
                        HttpContext.Current.Response.ContentType = "image/Jpeg";
                        HttpContext.Current.Response.BinaryWrite(stream.ToArray());
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.End();

                        _result = true;
                    }
                }
            }

            return _result;
        }

        /// <summary>
        /// 添加背景噪点
        /// </summary>
        /// <param name="objBitmap">Bitmap</param>
        /// <param name="objGraphics">Graphics</param>
        private void AddBackgroundNoisePoint(Bitmap objBitmap, Graphics objGraphics)
        {
            using (Pen pen = new Pen(Color.Azure, 0))
            {
                for (int i = 0; i < objBitmap.Width * 2; i++)
                {
                    objGraphics.DrawRectangle(pen, objRandom.Next(objBitmap.Width), objRandom.Next(objBitmap.Height), 1, 1);
                }
            }
        }

        /// <summary>
        /// 添加前景噪点
        /// </summary>
        /// <param name="objBitmap"></param>
        private void AddForeNoisePoint(Bitmap objBitmap)
        {
            for (int i = 0; i < objBitmap.Width * this.SetForeNoisePointCount; i++)
            {
                objBitmap.SetPixel(objRandom.Next(objBitmap.Width), objRandom.Next(objBitmap.Height), this.SetFontColor);
            }
        }

        /// <summary>
        /// 获取随机颜色
        /// </summary>
        /// <returns></returns>
        private Color GetRandomColor()
        {
            Random _first = new Random((int)DateTime.Now.Ticks);
            Thread.Sleep(_first.Next(50));
            Random _second = new Random((int)DateTime.Now.Ticks);
            int _red = _first.Next(256),
                _green = _second.Next(256),
                _blue = (_red + _green > 400) ? 0 : 400 - _red - _green;
            _blue = (_blue > 255) ? 255 : _blue;
            return Color.FromArgb(_red, _green, _blue);
        }

        /// <summary>
        /// 得到验证码图片
        /// </summary>
        private Bitmap GetVerifyCodeImage()
        {
            Bitmap _bmpVeriCode = new Bitmap(SetWith, SetHeight); ;

            using (Graphics objGraphics = Graphics.FromImage(_bmpVeriCode))
            {
                objGraphics.SmoothingMode = SmoothingMode.HighQuality;

                //清除整个绘图面并以指定背景色填充
                objGraphics.Clear(this.SetBackgroundColor);

                //创建画笔
                using (SolidBrush objSolidBrush = new SolidBrush(this.SetFontColor))
                {
                    this.AddForeNoisePoint(_bmpVeriCode);

                    this.AddBackgroundNoisePoint(_bmpVeriCode, objGraphics);

                    //文字居中
                    StringFormat _objStringFormat = new StringFormat(StringFormatFlags.NoClip);

                    _objStringFormat.Alignment = StringAlignment.Center;
                    _objStringFormat.LineAlignment = StringAlignment.Center;

                    //字体样式
                    Font _objFont = new Font(this.SetFontFamily, objRandom.Next(this.SetFontSize - 3, this.SetFontSize), FontStyle.Regular);

                    //验证码旋转，防止机器识别
                    char[] _chars = this.SetVerifyCodeText.ToCharArray();

                    for (int i = 0; i < _chars.Length; i++)
                    {
                        //转动的度数
                        float _angle = objRandom.Next(-this.SetRandomAngle, this.SetRandomAngle);

                        objGraphics.TranslateTransform(12, 12);
                        objGraphics.RotateTransform(_angle);
                        objGraphics.DrawString(_chars[i].ToString(), _objFont, objSolidBrush, -2, 2, _objStringFormat);
                        objGraphics.RotateTransform(-_angle);
                        objGraphics.TranslateTransform(2, -12);
                    }
                }
            }

            return _bmpVeriCode;
        }

        /// <summary>
        /// 得到验证码字符串
        /// </summary>
        private void GetVerifyCodeText()
        {
            //没有外部输入验证码时随机生成
            if (string.IsNullOrEmpty(this.SetVerifyCodeText))
            {
                StringBuilder _objStringBuilder = new StringBuilder();

                //加入数字1-9
                for (int i = 1; i <= 9; i++)
                {
                    _objStringBuilder.Append(i.ToString());
                }

                //加入大写字母A-Z，不包括O
                if (this.SetAddUpperLetter)
                {
                    char _temp = ' ';

                    for (int i = 0; i < 26; i++)
                    {
                        _temp = Convert.ToChar(i + 65);

                        //如果生成的字母不是'O'
                        if (!_temp.Equals('O'))
                        {
                            _objStringBuilder.Append(_temp);
                        }
                    }
                }

                //加入小写字母a-z，不包括o
                if (this.SetAddLowerLetter)
                {
                    char _temp = ' ';

                    for (int i = 0; i < 26; i++)
                    {
                        _temp = Convert.ToChar(i + 97);

                        //如果生成的字母不是'o'
                        if (!_temp.Equals('o'))
                        {
                            _objStringBuilder.Append(_temp);
                        }
                    }
                }

                //生成验证码字符串
                {
                    int _index = 0;

                    for (int i = 0; i < SetLength; i++)
                    {
                        _index = objRandom.Next(0, _objStringBuilder.Length);

                        this.SetVerifyCodeText += _objStringBuilder[_index];

                        _objStringBuilder.Remove(_index, 1);
                    }
                }
            }
        }

        #endregion Methods
    }
}