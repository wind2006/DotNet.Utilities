namespace YanZhiwei.DotNet2.Utilities.WinForm.Core
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    /// <summary>
    /// Messagebox自动定时关闭
    /// </summary>
    /// 日期：2015-10-21 16:01
    /// 备注：
    public class MessageBoxTimeOut
    {
        #region Fields        
        /// <summary>
        /// The w m_ close
        /// </summary>
        public const int WM_CLOSE = 0x10;

        private string keyGuidId;

        #endregion Fields

        #region Methods        
        /// <summary>
        /// Posts the message.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="msg">The MSG.</param>
        /// <param name="wParam">The w parameter.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns>int</returns>
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// 弹出消息框_默认三秒后关闭
        /// </summary>
        /// <param name="text">消息框中显示的文本</param>
        /// <param name="caption">标题文本</param>
        /// 日期：2015-10-21 16:02
        /// 备注：
        public void Show(string text, string caption)
        {
            Show(3000, text, caption);
        }

        /// <summary>
        /// 弹出消息框
        /// </summary>
        /// <param name="timeout">自动关闭时间_毫秒</param>
        /// <param name="text">消息框中显示的文本</param>
        /// <param name="caption">标题文本</param>
        /// 日期：2015-10-21 16:04
        /// 备注：
        public void Show(int timeout, string text, string caption)
        {
            Show(timeout, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 弹出消息框
        /// </summary>
        /// <param name="timeout">自动关闭时间_毫秒</param>
        /// <param name="text">消息框中显示的文本</param>
        /// <param name="caption">标题文本</param>
        /// <param name="buttons">MessageBoxButtons</param>
        /// <param name="icon">MessageBoxIcon</param>
        /// 日期：2015-10-21 16:05
        /// 备注：
        public void Show(int timeout, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            this.keyGuidId = caption;
            StartTimer(timeout);
            MessageBox.Show(text, caption, buttons, icon);
        }

        [DllImport("User32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private void KillMessageBox()
        {
            IntPtr _ptr = FindWindow(null, this.keyGuidId);
            if (_ptr != IntPtr.Zero)
            {
                PostMessage(_ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            }
        }

        private void StartTimer(int interval)
        {
            Timer _timer = new Timer();
            _timer.Interval = interval;
            _timer.Tick += new EventHandler(Timer_Tick);
            _timer.Enabled = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            KillMessageBox();
            ((Timer)sender).Enabled = false;
        }

        #endregion Methods
    }
}