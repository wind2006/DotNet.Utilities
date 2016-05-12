namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System.Data;

    /// <summary>
    /// DataSet 帮助类
    /// </summary>
    /// 时间：2016-01-05 13:18
    /// 备注：
    public static class DataSetHelper
    {
        #region Methods

        /// <summary>
        /// 判断DataSet是否是NULL或者没有DataTable
        /// </summary>
        /// <param name="dataset">The dataset.</param>
        /// <returns></returns>
        /// 时间：2016-01-05 13:19
        /// 备注：
        public static bool IsNullOrEmpty(this DataSet dataset)
        {
            return dataset == null || dataset.Tables.Count == 0;
        }

        #endregion Methods
    }
}