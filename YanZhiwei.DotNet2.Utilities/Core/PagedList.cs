namespace YanZhiwei.DotNet2.Utilities.Core
{
    using System;
    using System.Collections.Generic;

    using YanZhiwei.DotNet2.Interfaces.DataAccess;

    /// <summary>
    /// 分页数据集合，用于后端返回分页好的集合及前端视图分页控件绑定
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public class PagedList<T> : List<T>, IPagedList
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="items">集合</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">页总数</param>
        /// 时间：2016-01-05 11:03
        /// 备注：
        public PagedList(IList<T> items, int pageIndex, int pageSize)
        {
            PageSize = pageSize;
            TotalItemCount = items.Count;
            CurrentPageIndex = pageIndex;
            for (int i = StartRecordIndex - 1; i < EndRecordIndex; i++)
            {
                Add(items[i]);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="items">集合</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">页总数</param>
        /// <param name="totalItemCount">记录条数</param>
        /// 时间：2016-01-05 11:04
        /// 备注：
        public PagedList(IEnumerable<T> items, int pageIndex, int pageSize, int totalItemCount)
        {
            AddRange(items);
            TotalItemCount = totalItemCount;
            CurrentPageIndex = pageIndex;
            PageSize = pageSize;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 当前页索引
        /// </summary>
        /// 时间：2016-01-05 11:05
        /// 备注：
        public int CurrentPageIndex
        {
            get; set;
        }

        /// <summary>
        /// 记录结束索引
        /// </summary>
        /// 时间：2016-01-05 11:06
        /// 备注：
        public int EndRecordIndex
        {
            get { return TotalItemCount > CurrentPageIndex * PageSize ? CurrentPageIndex * PageSize : TotalItemCount; }
        }

        /// <summary>
        /// Gets or sets the extra count.
        /// </summary>
        /// 时间：2016-01-05 11:06
        /// 备注：
        public int ExtraCount
        {
            get; set;
        }

        /// <summary>
        /// 分页大小
        /// </summary>
        /// 时间：2016-01-05 11:05
        /// 备注：
        public int PageSize
        {
            get; set;
        }

        /// <summary>
        /// 数据开始索引
        /// </summary>
        /// 时间：2016-01-05 11:05
        /// 备注：
        public int StartRecordIndex
        {
            get { return (CurrentPageIndex - 1) * PageSize + 1; }
        }

        /// <summary>
        /// 记录总数
        /// </summary>
        /// 时间：2016-01-05 11:05
        /// 备注：
        public int TotalItemCount
        {
            get; set;
        }

        /// <summary>
        /// 页总数
        /// </summary>
        /// 时间：2016-01-05 11:05
        /// 备注：
        public int TotalPageCount
        {
            get { return (int)Math.Ceiling(TotalItemCount / (double)PageSize); }
        }

        #endregion Properties
    }
}