namespace YanZhiwei.DotNet2.Utilities.WebForm
{
    using System.Web;

    /// <summary>
    /// WebForm 文件帮助类
    /// </summary>
    public class FileHelper
    {
        #region Methods

        /// <summary>
        /// 获取物理地址
        /// </summary>
        /// <param name="fileVirtualPath">文件虚拟路径</param>
        /// <returns>物理路径</returns>
        public static string GetMapPathFile(string fileVirtualPath)
        {
            return HttpContext.Current.Server.MapPath(fileVirtualPath);
        }

        #endregion Methods
    }
}