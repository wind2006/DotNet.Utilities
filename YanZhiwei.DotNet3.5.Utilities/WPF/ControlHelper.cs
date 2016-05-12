namespace YanZhiwei.DotNet3._5.Utilities.WPF
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Control 帮助类
    /// </summary>
    public static class ControlHelper
    {
        #region Methods

        /// <summary>
        /// 窗体全屏
        /// eg:MetroWindow_Loaded事件中使用
        /// </summary>
        /// <param name="windows">Window</param>
        public static void FullScreen(this Window windows)
        {
            windows.WindowState = WindowState.Normal;
            windows.WindowStyle = WindowStyle.None;
            windows.ResizeMode = ResizeMode.NoResize;
            windows.Topmost = true;

            windows.Left = 0.0;
            windows.Top = 0.0;
            windows.Width = SystemParameters.PrimaryScreenWidth;
            windows.Height = SystemParameters.PrimaryScreenHeight;
        }

        /// <summary>
        /// 将WPF控件告诉设置为auto
        /// </summary>
        /// <typeparam name="T">控件泛型</typeparam>
        /// <param name="t">控件</param>
        public static void SetHeightAuto<T>(this T t) where T : Control
        {
            t.Dispatch(c => c.Height = double.NaN);
        }

        /// <summary>
        /// 将WPF控件宽度设置为auto【线程安全】
        /// </summary>
        /// <typeparam name="T">控件泛型</typeparam>
        /// <param name="t">控件</param>
        public static void SetWidthAuto<T>(this T t)  where T : Control
        {
            t.Dispatch(c => c.Width = double.NaN);
        }

        #endregion Methods
    }
}