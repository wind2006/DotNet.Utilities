using MahApps.Metro.Controls;
using System.Collections;
using YanZhiwei.DotNet3._5.Utilities.WPF;
namespace YanZhiwei.DotNet.MahApps.Metro0.Utilities
{
    /// <summary>
    /// 基于.NET 4.0的MahAppsMetro-SplitButton工具类
    /// </summary>
    public static class SplitButtonHelper
    {
        /// <summary>
        /// 绑定数据源，线程安全
        /// </summary>
        /// <param name="splitButton">SplitButton</param>
        /// <param name="itemsource">数据源</param>
        /// <returns>SplitButton</returns>
        public static SplitButton BindTF(this SplitButton splitButton, IEnumerable itemsource)
        {
            splitButton.Dispatch(sb =>
            {
                sb.ItemsSource = itemsource;
            });
            return splitButton;
        }

        /// <summary>
        /// 设置Enable
        /// </summary>
        /// <param name="splitButton">SplitButton</param>
        /// <param name="enable">IsEnabled</param>
        /// <returns>SplitButton</returns>
        public static SplitButton SetEnableTF(this SplitButton splitButton, bool enable)
        {
            splitButton.Dispatch(sb =>
            {
                sb.IsEnabled = enable;
            });
            return splitButton;
        }
    }
}