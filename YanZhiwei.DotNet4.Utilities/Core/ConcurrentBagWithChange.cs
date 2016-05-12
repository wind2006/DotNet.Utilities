namespace YanZhiwei.DotNet4.Utilities.Core
{
    using System;
    using System.Collections.Concurrent;

    using YanZhiwei.DotNet2.Utilities.Common;
    using YanZhiwei.DotNet4.Utilities.Models;

    /// <summary>
    /// ConcurrentBag 数据操作通知增强类
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// 日期：2015-09-17 16:00
    /// 备注：
    public class ConcurrentBagWithChange<T> : ConcurrentBag<T>
        where T : class
    {
        #region Events

        /// <summary>
        /// 已经增加数据项目时候出发
        /// </summary>
        public event EventHandler<ConBagChangEventArgs<T>> OnAdded;

        #endregion Events

        #region Methods

        /// <summary>
        /// 添加数据项目
        /// </summary>
        /// <param name="item">数据项</param>
        /// 日期：2015-09-17 16:01
        /// 备注：
        public new void Add(T item)
        {
            base.Add(item);

            this.OnAdded.RaiseEvent(this, new ConBagChangEventArgs<T>() { Item = item, Count = base.Count });
        }

        #endregion Methods
    }
}