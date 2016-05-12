using System;
using System.Windows.Forms;
using YanZhiwei.DotNet2.Utilities.Enums;
using YanZhiwei.DotNet2.Utilities.WinForm;

namespace YanZhiwei.DotNet2.Utilities.WinFromExamples
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ApplicationHelper.CapturedException((ex, mode) =>
            {
                switch (mode)
                {
                    case ExceptionMode.UnhandledException:
                        MessageBox.Show("发生未捕获的异常:" + ex.Message);
                        break;

                    case ExceptionMode.ThreadException:
                        MessageBox.Show("发生线程异常:" + ex.Message);
                        break;
                }
            });
            ApplicationHelper.ApplyOnlyOneInstance(t =>
            {
                if (t)
                {
                    FormHelper.ShowLoginForm<winLogin, winMain>();
                }
                //Application.Run(new winMain());
                else
                {
                    MessageBox.Show("已经有程序在运行。");
                }
            });
        }
    }
}