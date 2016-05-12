namespace YanZhiwei.DotNet4.Utilities.Models
{
    using System;

    /// <summary>
    /// ConcurrentBagWithChange 事件参数
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// 日期：2015-09-17 15:47
    /// 备注：
    public class ConBagChangEventArgs<T> : EventArgs
    {
        #region Properties

        /// <summary>
        /// 当前ConcurrentBag包含项数量
        /// </summary>
        public int Count
        {
            get;
            set;
        }

        /// <summary>
        /// 数据项目
        /// </summary>
        public T Item
        {
            get;
            set;
        }

        #endregion Properties
    }
}