using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using YanZhiwei.DotNet2.Utilities.Common;
using YanZhiwei.DotNet3._5.Utilities.Model;

namespace YanZhiwei.DotNet3._5.Utilities.WebForm.Jquery
{
    /// <summary>
    /// Jquery DataTables帮助类
    /// </summary>
    /// 时间：2016-04-19 16:04
    /// 备注：
    public static class DataTableHelper
    {
        /// <summary>
        /// 数据分页
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <param name="getDataFactory">委托，顺位输入参数：1.用于指定一屏显示的条数，需开启分页器;2.用于指定从哪一条数据开始显示到表格中去，3.排序列索引;4.升序还是降序，返回DataTablePageResult</param>
        /// 时间：2016-04-19 16:35
        /// 备注：
        public static void ExecutePageQuery<T>(this HttpContext context, Func<int, int, int, string, DataTablePageResult> getDataFactory) where T : class
        {
            int _iDisplayLength = context.Request["iDisplayLength"].ToInt32OrDefault(10);//用于指定一屏显示的条数，需开启分页器
            int _iDisplayStart = context.Request["iDisplayStart"].ToInt32OrDefault(0);//用于指定从哪一条数据开始显示到表格中去
            int _iSortCol = context.Request["iSortCol_0"].ToInt32OrDefault(0);//排序列索引
            string _iSortDir = context.Request["sSortDir_0"].ToStringOrDefault("desc");//升序还是降序

            DataTablePageResult _result = getDataFactory(_iDisplayLength, _iDisplayStart, _iSortCol, _iSortDir);
            if (_result.ExecuteState != HttpStatusCode.OK)
            {
                _result.iTotalDisplayRecords = 0;
                _result.iTotalRecords = 0;
                _result.aaData = new List<T>();
            }
            context.CreateResponse(_result, _result.ExecuteState);
        }
    }
}