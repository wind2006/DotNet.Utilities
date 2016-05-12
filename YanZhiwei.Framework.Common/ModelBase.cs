namespace YanZhiwei.DotNet.Framework.Contract
{
    using System;
    //using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 实体类基类
    /// </summary>
    /// 时间：2016-01-06 16:13
    /// 备注：
    public class ModelBase
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// 时间：2016-01-06 16:13
        /// 备注：
        public ModelBase()
        {
            CreateTime = DateTime.Now;
            ModifyTime = DateTime.Now;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 创建时间
        /// </summary>
        //[Display(Name = "创建时间")]
        public virtual DateTime CreateTime
        {
            get; set;
        }

        /// <summary>
        /// 主键
        /// </summary>
       // [Key]
        public virtual int ID
        {
            get; set;
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        //[Display(Name = "更新时间")]
        public virtual DateTime ModifyTime
        {
            get; set;
        }

        #endregion Properties
    }
}