namespace YanZhiwei.DotNet3._5.Utilities.Model
{
    /// <summary>
    /// 上传返回信息
    /// </summary>
    public class UploadFileMessage
    {
        /// <summary>
        /// 上传错误
        /// </summary>
        public bool HasError { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 网站路径
        /// </summary>
        public string WebFilePath { get; set; }

        /// <summary>
        /// 获取文件名
        /// </summary>
        public string FileName { get; set; }
    }
}