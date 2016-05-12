namespace cdz360Tools.Clients
{
    /// <summary>
    /// 客户端连接服务器端的状态
    /// </summary>
    public enum CommState
    {
        /// <summary>
        /// 连接成功
        /// </summary>
        Scuess,

        /// <summary>
        /// 连接失败
        /// </summary>
        Failed,

        /// <summary>
        /// 连接超时
        /// </summary>
        TimeOut,

        /// <summary>
        /// 未注册
        /// </summary>
        NoRegister,

        /// <summary>
        /// 连接过程中出现的其他状况
        /// </summary>
        Other,

        /// <summary>
        /// 未连接
        /// </summary>
        NoConnect,
    }
}