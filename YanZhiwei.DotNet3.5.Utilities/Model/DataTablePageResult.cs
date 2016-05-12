using System.Collections;
using System.Net;

namespace YanZhiwei.DotNet3._5.Utilities.Model
{
    /// <summary>
    /// Jquery DataTable分页结果集
    /// </summary>
    /// 时间：2016-04-19 16:12
    /// 备注：
    public class DataTablePageResult 
    {
        /// <summary>
        /// 实际的行数
        /// </summary>
        public int iTotalRecords { get; set; }

        /// <summary>
        /// 过滤之后，实际的行数。
        /// </summary>
        public int iTotalDisplayRecords { get; set; }

        /// <summary>
        /// 数组的数组，表格中的实际数据。
        /// </summary>
        public IEnumerable aaData
        {
            get;
            set;
        }

        /// <summary>
        /// 错误提示消息
        /// </summary>
        public string ExecuteMessage { get; set; }

        /// <summary>
        /// 执行状态
        /// </summary>
        public HttpStatusCode ExecuteState { get; set; }
    }
}