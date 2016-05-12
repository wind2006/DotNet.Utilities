namespace YanZhiwei.DotNet3._5.Utilities.Model
{
    using System;

    /// <summary>
    /// Json
    /// </summary>
    [Serializable]
    public class JsonResult
    {
        #region Properties

        /// <summary>
        /// Message
        /// </summary>
        public object Message
        {
            get; set;
        }

        /// <summary>
        /// 状态码 eg：System.Net.HttpStatusCode.BadRequest
        /// </summary>
        public int StatusCode
        {
            get; set;
        }

        /// <summary>
        /// 错误代码，用于辨识错误类型
        /// </summary>
        public int ErrorCode
        {
            get; set;
        }

        #endregion Properties
    }
}