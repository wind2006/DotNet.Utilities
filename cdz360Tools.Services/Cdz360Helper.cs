namespace cdz360Tools.Services
{
    using System;

    using YanZhiwei.DotNet.Log4Net.Utilities;
    using YanZhiwei.DotNet2.Utilities.Common;

    /// <summary>
    /// 通用辅助类
    /// </summary>
    /// 时间：2016-04-12 20:17
    /// 备注：
    public class Cdz360Helper
    {
        #region Methods

        /// <summary>
        /// 打印日志
        /// </summary>
        /// <param name="v">The v.</param>
        /// 时间：2016-04-12 15:49
        /// 备注：
        public static void PrintLog(string v)
        {
            Log4NetHelper.WriteInfo(v);
            v = string.Format("{0} {1}", DateTime.Now.FormatDate(1), v);
            Console.WriteLine(v);
        }

        

        #endregion Methods
    }
}