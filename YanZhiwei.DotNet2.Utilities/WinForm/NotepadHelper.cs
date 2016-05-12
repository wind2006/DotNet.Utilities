namespace YanZhiwei.DotNet2.Utilities.WinForm
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    /// <summary>
    ///  Notepad 帮助类
    /// </summary>
    public class NotepadHelper
    {
        #region Methods

        /// <summary>
        /// 打开记事本，并往记事本写入内容
        /// </summary>
        /// <param name="message">记事本内容</param>
        /// <param name="title">记事本名称</param>
        public static void ShowMessage(string message, string title)
        {
            Process _notepad = Process.Start(new ProcessStartInfo("notepad.exe"));
            _notepad.WaitForInputIdle();
            if (!string.IsNullOrEmpty(title))
            {
                SetWindowText(_notepad.MainWindowHandle, title);
            }

            if (_notepad != null && !string.IsNullOrEmpty(message))
            {
                IntPtr _child = FindWindowEx(_notepad.MainWindowHandle, new IntPtr(0), "Edit", null);
                SendMessage(_child, 0x000C, 0, message);
            }
        }

        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);

        [DllImport("user32.dll", EntryPoint = "SetWindowText")]
        private static extern int SetWindowText(IntPtr hWnd, string text);

        #endregion Methods
    }
}