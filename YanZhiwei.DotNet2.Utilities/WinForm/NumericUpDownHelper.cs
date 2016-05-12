namespace YanZhiwei.DotNet2.Utilities.WinForm
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// NumericUpDown 帮助类
    /// </summary>
    public static class NumericUpDownHelper
    {
        #region Methods

        /// <summary>
        /// 设置NumericUpDown值前，判断是否大于或者小于其控件本身maxvalue,minValue；
        /// </summary>
        /// <param name="numericUpDown">NumericUpDown</param>
        /// <param name="value">数值</param>
        public static void SetSafeValue(this NumericUpDown numericUpDown, decimal value)
        {
            decimal _ctrlMax = numericUpDown.Maximum;
            decimal _ctrMin = numericUpDown.Minimum;
            decimal _legalMinValue = Math.Min(value, _ctrlMax);
            numericUpDown.Value = Math.Max(_ctrMin, _legalMinValue);
        }

        #endregion Methods
    }
}