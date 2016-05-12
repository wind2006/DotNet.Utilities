using System.IO;

namespace YanZhiwei.DotNet3._5.Utilities.Model
{
    /// <summary>
    /// 上传图片返回信息
    /// </summary>
    public class UploadImageMessage
    {
        #region Properties

        /// <summary>
        /// 图片目录
        /// </summary>
        public string Directory
        {
            get
            {
                if (WebPath == null) return null;
                return WebPath.Replace(FileName, "");
            }
        }

        /// <summary>
        /// 图片名
        /// </summary>
        public string FileName
        {
            get; set;
        }

        /// <summary>
        /// 文件物理路径
        /// </summary>
        public string FilePath
        {
            get; set;
        }

        /// <summary>
        /// 是否遇到错误
        /// </summary>
        public bool IsError
        {
            get; set;
        }

        /// <summary>
        /// 反回消息
        /// </summary>
        public string Message
        {
            get; set;
        }

        /// <summary>
        /// 文件大小
        /// </summary>
        public double Size
        {
            get; set;
        }

        /// <summary>
        /// web路径
        /// </summary>
        public string WebPath
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 缩略图路径
        /// </summary>
        public string SmallPath(int index)
        {
            return string.Format("{0}{1}_{2}{3}", Directory, Path.GetFileNameWithoutExtension(FileName), index, Path.GetExtension(FileName));
        }

        #endregion Methods
    }
}