using System;

namespace YanZhiwei.DotNet.Office11.Utilities
{
    /// <summary>
    /// Office 帮助类
    /// </summary>
    public static class OfficeHelper
    {
        /*
         *参考
         *1. http://www.codeproject.com/Tips/701015/How-to-Check-Whether-Word-is-Installed-in-the-Syst
         */

        /// <summary>
        /// 检查是否安装excel
        /// </summary>
        /// <returns></returns>
        public static bool isExcelInstalled()
        {
            return isApplicationInstalled("Excel.Application");
        }

        /// <summary>
        /// 检查是否安装word.
        /// </summary>
        /// <returns></returns>
        public static bool isWordInstalled()
        {
            return isApplicationInstalled("Word.Application");
        }

        private static bool isApplicationInstalled(string applicationName)
        {
            Type _officeType = Type.GetTypeFromProgID(applicationName);
            return _officeType != null;
        }
    }
}