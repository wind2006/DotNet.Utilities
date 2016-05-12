namespace YanZhiwei.DotNet2.Utilities.Models
{
    using System;

    /// <summary>
    /// 枚举属性
    /// </summary>
    /// 时间：2016-01-15 11:16
    /// 备注：
    public class EnumTitleAttribute : Attribute
    {
        #region Fields

        private bool isDisplay = true;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="title">The title.</param>
        /// 时间：2016-01-15 11:22
        /// 备注：
        public EnumTitleAttribute(string title)
        {
            Title = title;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsDisplay
        {
            get { return isDisplay; }
            set { isDisplay = value; }
        }

        /// <summary>
        /// 显示标题
        /// </summary>
        public string Title
        {
            get; set;
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order
        {
            get;
            set;
        }

        #endregion Properties
    }
}