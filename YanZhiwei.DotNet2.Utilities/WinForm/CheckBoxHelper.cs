namespace YanZhiwei.DotNet2.Utilities.WinForm
{
    using System;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    ///  CheckBox 帮助类
    /// </summary>
    public static class CheckBoxHelper
    {
        #region Methods

        /// <summary>
        /// 转译checkbox的状态
        /// </summary>
        /// <param name="checkStateFactory">委托</param>
        /// <param name="checks">CheckBox</param>
        /// <returns>转译后的checkbox的状态</returns>
        public static string TranCheckState(Func<CheckState, string> checkStateFactory, params CheckBox[] checks)
        {
            StringBuilder _builder = new StringBuilder();
            if (checks != null && checks.Length > 0)
            {
                foreach (CheckBox ck in checks)
                {
                    _builder.Append(checkStateFactory(ck.CheckState));
                }
            }
            return _builder.ToString();
        }

        #endregion Methods
    }
}