namespace YanZhiwei.DotNet2.Utilities.WinForm
{
    using System.Collections.Generic;
    using System.Windows.Forms;

    using YanZhiwei.DotNet2.Utilities.Models;

    /// <summary>
    /// 窗体控件自适应大小
    ///<para>eg:</para>
    ///<para> private void FormMain_SizeChanged(object sender, EventArgs e)</para>
    ///<para>{</para>
    ///<para>  As.controlAutoSize(this);</para>
    ///<para>}</para>
    /// </summary>
    public class AutoSizeFormHelper
    {
        #region Fields

        /// <summary>
        /// 记录的始终是当前的大小
        /// </summary>
        public List<ControlRect> oldCtrl = new List<ControlRect>();

        private int ctrlNo = 0;

        #endregion Fields

        #region Methods

        /// <summary>
        /// 控件自适应大小
        /// </summary>
        /// <param name="form">Control</param>
        public void ControlAutoSize(Control form)
        {
            if (ctrlNo == 0)
            {
                ControlRect _cR;
                _cR.Left = form.Left; _cR.Top = form.Top; _cR.Width = form.Width; _cR.Height = form.Height;
                oldCtrl.Add(_cR);//第一个为"窗体本身",只加入一次即可
                AddControl(form);//窗体内其余控件可能嵌套其它控件(比如panel),故单独抽出以便递归调用
            }
            float _wScale = (float)form.Width / (float)oldCtrl[0].Width;//新旧窗体之间的比例，与最早的旧窗体
            float _hScale = (float)form.Height / (float)oldCtrl[0].Height;//.Height;
            ctrlNo = 1;//进入=1，第0个为窗体本身,窗体内的控件,从序号1开始
            AutoScaleControl(form, _wScale, _hScale);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
        }

        /// <summary>
        /// 记录窗体和其控件的初始位置和大小,
        /// </summary>
        /// <param name="form">Control</param>
        public void ControllInitializeSize(Control form)
        {
            ControlRect _cR;
            _cR.Left = form.Left; _cR.Top = form.Top;
            _cR.Width = form.Width; _cR.Height = form.Height;
            oldCtrl.Add(_cR);//第一个为"窗体本身",只加入一次即可
            AddControl(form);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
        }

        private void AddControl(Control ctl)
        {
            foreach (Control c in ctl.Controls)
            {
                ControlRect _objCtrl;
                _objCtrl.Left = c.Left; _objCtrl.Top = c.Top; _objCtrl.Width = c.Width; _objCtrl.Height = c.Height;
                oldCtrl.Add(_objCtrl);
                if (c.Controls.Count > 0)
                    AddControl(c);
            }
        }

        private void AutoScaleControl(Control ctl, float wScale, float hScale)
        {
            int _ctrLeft0, _ctrTop0, _ctrWidth0, _ctrHeight0;
            //int ctrlNo = 1;//第1个是窗体自身的 Left,Top,Width,Height，所以窗体控件从ctrlNo=1开始
            foreach (Control c in ctl.Controls)
            { //**放在这里，是先缩放控件的子控件，后缩放控件本身
                //if (c.Controls.Count > 0)
                //   AutoScaleControl(c, wScale, hScale);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
                _ctrLeft0 = oldCtrl[ctrlNo].Left;
                _ctrTop0 = oldCtrl[ctrlNo].Top;
                _ctrWidth0 = oldCtrl[ctrlNo].Width;
                _ctrHeight0 = oldCtrl[ctrlNo].Height;
                //c.Left = (int)((ctrLeft0 - wLeft0) * wScale) + wLeft1;//新旧控件之间的线性比例
                //c.Top = (int)((ctrTop0 - wTop0) * h) + wTop1;
                c.Left = (int)((_ctrLeft0) * wScale);//新旧控件之间的线性比例。控件位置只相对于窗体，所以不能加 + wLeft1
                c.Top = (int)((_ctrTop0) * hScale);//
                c.Width = (int)(_ctrWidth0 * wScale);//只与最初的大小相关，所以不能与现在的宽度相乘 (int)(c.Width * w);
                c.Height = (int)(_ctrHeight0 * hScale);//
                ctrlNo++;//累加序号
                //**放在这里，是先缩放控件本身，后缩放控件的子控件
                if (c.Controls.Count > 0)
                    AutoScaleControl(c, wScale, hScale);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
            }
        }

        #endregion Methods
    }
}