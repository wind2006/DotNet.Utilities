using System;
using System.ComponentModel;

namespace Molin_CRM.Model
{
    /// <summary>
    /// 产品明细
    /// </summary>
    public class Product
    {
        /// <summary>
        /// 品名
        /// </summary>
        [DisplayName("品名")]
        public string Name { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [DisplayName("数量")]
        public int Number { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        [DisplayName("单价")]
        public decimal Price { get; set; }

        /// <summary>
        /// 总价
        /// </summary>
        [DisplayName("总价")]
        public decimal TotalPrice
        {
            get { return Math.Round(Number * Price, 2); }
        }

        /// <summary>
        /// 操作时间
        /// </summary>
        [DisplayName("操作时间")]
        public DateTime OptTime { get; set; }
    }
}