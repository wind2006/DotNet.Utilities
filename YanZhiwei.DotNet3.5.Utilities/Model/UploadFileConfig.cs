using YanZhiwei.DotNet3._5.Utilities.Enums;

namespace YanZhiwei.DotNet3._5.Utilities.Model
{
    /// <summary>
    /// 文件上传参数配置
    /// </summary>
    /// 时间：2015-12-17 11:11
    /// 备注：
    public class UploadFileConfig
    {
        /// <summary>
        /// 文件保存路径
        /// </summary>
        public string FileDirectory { get; set; }

        /// <summary>
        /// 允许上传的文件类型, 逗号分割,必须全部小写!
        /// </summary>
        public string FileType { get; set; }

        /// <summary>
        /// 允许上传多少大小
        /// </summary>
        public double MaxSizeM { get; set; }

        /// <summary>
        /// 路径存储类型
        /// </summary>
        public UploadFileSaveType PathSaveType { get; set; }

        /// <summary>
        /// 重命名同名文件?
        /// </summary>
        public bool IsRenameSameFile { get; set; }

        /// <summary>
        /// 是否使用原始文件名
        /// </summary>
        public bool IsUseOldFileName { get; set; }
    }
}