using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;

namespace YanZhiwei.DotNet.MahApps.Metro.Utilities
{
    /// <summary>
    /// 基于.NET 4.0的ShowMessageAsync和ShowInputAsync的工具类
    /// </summary>
    public static class MessageHelper
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="windows"></param>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <returns>MessageDialogResult</returns>
        public static async Task<MessageDialogResult> ShowInfoOk(this MetroWindow windows, string title, string message)
        {
            return await windows.ShowMessageAsync(title, message, MessageDialogStyle.Affirmative, CreateMetroDialog_Ok());
        }

        private static MetroDialogSettings CreateMetroDialog_Ok()
        {
            MetroDialogSettings _setting = new MetroDialogSettings();
            _setting.AffirmativeButtonText = "确定";
            return _setting;
        }

        /// <summary>
        /// 异步消息提示
        /// </summary>
        /// <param name="windows">MetroWindow</param>
        /// <param name="title">标题栏</param>
        /// <param name="message">消息内容</param>
        /// <returns>MessageDialogResult</returns>
        public static async Task<MessageDialogResult> ShowInfoOkAndCancel(this MetroWindow windows, string title, string message)
        {
            return await windows.ShowMessageAsync(title, message, MessageDialogStyle.AffirmativeAndNegative, CreateMetroDialog_OkCancel());
        }

        private static MetroDialogSettings CreateMetroDialog_OkCancel()
        {
            MetroDialogSettings _setting = new MetroDialogSettings();
            _setting.AffirmativeButtonText = "确定";
            _setting.NegativeButtonText = "取消";
            return _setting;
        }

        /// <summary>
        /// 异步消息提示
        /// </summary>
        /// <param name="windows">MetroWindow</param>
        /// <param name="title">标题栏</param>
        /// <param name="message">消息内容</param>
        /// <param name="auxiliaryText">辅助按钮文字内容</param>
        /// <returns>MessageDialogResult</returns>
        public static async Task<MessageDialogResult> ShowInfoOKCancelAndAuxiliary(this MetroWindow windows, string title, string message, string auxiliaryText)
        {
            return await windows.ShowMessageAsync(title, message, MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary, CreateMetroDialog_OkCancelAndAuxiliary(auxiliaryText));
        }

        private static MetroDialogSettings CreateMetroDialog_OkCancelAndAuxiliary(string auxiliaryText)
        {
            MetroDialogSettings _setting = new MetroDialogSettings();
            _setting.AffirmativeButtonText = "确定";
            _setting.NegativeButtonText = "取消";
            _setting.FirstAuxiliaryButtonText = auxiliaryText;
            return _setting;
        }

        /// <summary>
        /// 带输入框的异步消息提示
        /// </summary>
        /// <param name="windows">MetroWindow</param>
        /// <param name="title">标题栏</param>
        /// <param name="message">消息内容</param>
        /// <param name="lableText">输入框Lable文字</param>
        /// <returns>输入内容</returns>
        public static async Task<string> ShowInputOkAndCancel(this MetroWindow windows, string title, string message, string lableText)
        {
            MetroDialogSettings _setting = CreateMetroDialog_OkCancel();
            _setting.DefaultText = lableText;
            return await windows.ShowInputAsync(title, message, _setting);
        }
    }
}