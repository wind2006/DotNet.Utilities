namespace YanZhiwei.DotNet3._5.Utilities.Common
{
    using System.Collections.Generic;
    using System.Linq;

    using YanZhiwei.DotNet2.Utilities.Core;

    /// <summary>
    /// 分页帮助类
    /// </summary>
    /// 时间：2016-01-06 16:51
    /// 备注：
    public static class PageHelper
    {
        #region Methods

        /// <summary>
        /// 转换为PagedList集合
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="allItems">IQueryable</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns>PagedList集合</returns>
        /// 时间：2016-01-06 16:51
        /// 备注：
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> allItems, int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
                pageIndex = 1;
            int _itemIndex = (pageIndex - 1) * pageSize;
            List<T> _pageOfItems = allItems.Skip(_itemIndex).Take(pageSize).ToList();
            int _totalItemCount = allItems.Count();
            return new PagedList<T>(_pageOfItems, pageIndex, pageSize, _totalItemCount);
        }

        #endregion Methods
    }
}