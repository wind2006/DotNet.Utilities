namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System.Data;

    /// <summary>
    /// DataTable 帮助类
    /// </summary>
    /// 时间：2016-01-05 13:13
    /// 备注：
    public static class DataTableHelper
    {
        #region Methods

        /// <summary>
        /// 判断DataTable是否是NULL或者Row行数等于零
        /// </summary>
        /// <param name="datatable">DataTable</param>
        /// <returns>否是NULL或者Row行数等于零</returns>
        /// 时间：2016-01-05 13:15
        /// 备注：
        public static bool IsNullOrEmpty(this DataTable datatable)
        {
            return datatable == null || datatable.Rows.Count == 0;
        }

        #endregion Methods
    }
}