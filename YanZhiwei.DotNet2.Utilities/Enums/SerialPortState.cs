namespace YanZhiwei.DotNet2.Utilities.Enums
{
    #region Enumerations

    /// <summary>
    /// 串口操作状态
    /// </summary>
    public enum SerialPortState
    {
        /// <summary>
        /// 发送成功;
        /// </summary>
        SendSucceed,

        /// <summary>
        /// 发送超时
        /// </summary>
        SendTimeout,

        /// <summary>
        /// 发送失败
        /// </summary>
        SendFailed,

        /// <summary>
        /// 串口未打开
        /// </summary>
        UnOpened
    }

    #endregion Enumerations
}