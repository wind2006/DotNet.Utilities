namespace YanZhiwei.DotNet2.Utilities.WinForm
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using System.Windows.Forms;

    using YanZhiwei.DotNet2.Utilities.Enums;

    /// <summary>
    /// Application 帮助类
    /// </summary>
    public static class ApplicationHelper
    {
        #region Fields

        /// <summary>
        /// Mutex 对象
        /// </summary>
        private static Mutex singleton;

        #endregion Fields

        #region Methods

        /// <summary>
        /// 设置程序唯一实例运行.
        /// </summary>
        /// <param name="oneInstanceHanlder">委托，参数：是否是唯一实例</param>
        public static void ApplyOnlyOneInstance(Action<bool> oneInstanceHanlder)
        {
            bool _onlyRun = false;
            singleton = new Mutex(false, Assembly.GetExecutingAssembly().FullName, out _onlyRun);
            oneInstanceHanlder(_onlyRun);
        }

        /// <summary>
        /// 捕获异常
        /// <para>在应用程序的主入口点Main方法使用</para>
        /// </summary>
        /// <param name="capturedHanlder">The captured hanlder.</param>
        public static void CapturedException(Action<Exception, ExceptionMode> capturedHanlder)
        {
            try
            {
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                Application.ThreadException += (sender, e) =>
                {
                    capturedHanlder(e.Exception, ExceptionMode.ThreadException);
                };
                AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
                {
                    Exception _ex = (Exception)e.ExceptionObject;
                    capturedHanlder(_ex, ExceptionMode.UnhandledException);
                };
            }
            catch (Exception ex)
            {
                capturedHanlder(ex, ExceptionMode.UnhandledException);
            }
        }

        /// <summary>
        /// 捕获退出
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="mainForm">主窗口</param>
        /// <param name="closingHanlder">窗体正在关闭事件</param>
        public static void CapturedExit<T>(Form mainForm, Func<bool> closingHanlder)
            where T : Form
        {
            mainForm.FormClosing += (sender, e) =>
            {
                if (closingHanlder != null)
                {
                    e.Cancel = !closingHanlder();
                }
                else
                {
                    e.Cancel = true;
                }
            };
            mainForm.FormClosed += (sender, e) =>
            {
                Environment.Exit(Environment.ExitCode);
                Form _mainForm = sender as Form;
                _mainForm.Dispose();
                _mainForm.Close();
            };
        }

        /// <summary>
        /// 获取exe执行文件夹路径
        /// </summary>
        /// <returns>exe执行文件夹路径</returns>
        public static string GetExecuteDirectory()
        {
            return Path.GetDirectoryName(Application.ExecutablePath);
        }

        /// <summary>
        /// 全屏
        /// </summary>
        /// <param name="form">Form</param>
        public static void ToFullScreen(Form form)
        {
            ToFullScreen(form, 0);
        }

        /// <summary>
        /// 全屏
        /// </summary>
        /// <param name="form">Form</param>
        /// <param name="screen">0：主屏;1,2分屏</param>
        public static void ToFullScreen(Form form, int screen)
        {
            form.WindowState = FormWindowState.Maximized;
            form.StartPosition = FormStartPosition.Manual;
            form.Bounds = Screen.AllScreens[screen].Bounds;
        }

        #endregion Methods
    }
}