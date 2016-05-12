namespace cdz360Tools.Contracts
{
    /// <summary>
    /// Wcf 通用返回实体类
    /// </summary>
    /// 时间：2016-04-20 14:44
    /// 备注：
    public class SvrRetMessage
    {
        /// <summary>
        /// 执行结果
        /// </summary>
        public bool ExcuResult { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string Message { get; set; }
    }
}