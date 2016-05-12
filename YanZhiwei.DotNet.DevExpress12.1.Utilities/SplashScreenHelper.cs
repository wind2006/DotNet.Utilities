namespace YanZhiwei.DotNet.DevExpress12._1.Utilities
{
    using System;

    using DevExpress.XtraSplashScreen;

    /// <summary>
    /// SplashScreen帮助类
    /// </summary>
    public static class SplashScreenHelper
    {
        #region Fields

        private const bool FadeIn = false;
        private const bool FadeOut = true;
        private const bool ThrowExceptionIfIsAlreadyClosed = false;
        private const bool ThrowExceptionIfIsAlreadyShown = false;

        #endregion Fields

        #region Methods

        /// <summary>
        /// CloseSplashScreen
        /// </summary>
        public static void CloseSplashScreen()
        {
            if (SplashScreenManager.Default != null)
            {
                //Thread _task = new Thread(() =>
                //{
                SplashScreenManager.CloseForm(ThrowExceptionIfIsAlreadyClosed);
                //});
                //_task.Start();
            }
        }

        /// <summary>
        /// 设置Title
        /// </summary>
        /// <param name="caption">需要设置的Title</param>
        public static void SetCaption(string caption)
        {
            if (SplashScreenManager.Default != null && !string.IsNullOrEmpty(caption))
            {
                SplashScreenManager.Default.SetWaitFormCaption(caption);
            }
        }

        /// <summary>
        /// 设置文字提示信息
        /// </summary>
        /// <param name="description">需要设置的文字提示信息</param>
        public static void SetDescription(string description)
        {
            if (SplashScreenManager.Default != null && !string.IsNullOrEmpty(description))
            {
                SplashScreenManager.Default.SetWaitFormDescription(description);
            }
        }

        /// <summary>
        /// ShowSplashScreen
        /// </summary>
        /// <param name="type">WaitForm</param>
        public static void ShowSplashScreen(Type type)
        {
            CloseSplashScreen();
            SplashScreenManager.ShowForm(null, type, FadeIn, FadeOut, ThrowExceptionIfIsAlreadyShown);
        }

        #endregion Methods
    }
}