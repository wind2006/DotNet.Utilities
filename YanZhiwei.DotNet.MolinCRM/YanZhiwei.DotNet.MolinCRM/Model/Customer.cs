namespace Molin_CRM.Model
{
    /// <summary>
    /// 客户实体类
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// 标识ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 客户电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 客户地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}