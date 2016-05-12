namespace YanZhiwei.DotNet.Ftp.Utilities
{
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.FtpClient;

    using YanZhiwei.DotNet2.Utilities.Common;

    /// <summary>
    /// Ftp辅助类
    /// </summary>
    /// 时间：2016-04-26 8:58
    /// 备注：
    public class FTPHelper
    {
        #region Fields

        /// <summary>
        /// 服务器IP
        /// </summary>
        public readonly string ServerHost;

        /// <summary>
        /// 用户名称
        /// </summary>
        public readonly string UserName;

        /// <summary>
        /// 用户密码
        /// </summary>
        public readonly string UserPassword;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serverHost">服务器IP</param>
        /// <param name="userName">用户名</param>
        /// <param name="userPassword">密码</param>
        /// 时间：2016-04-26 9:01
        /// 备注：
        public FTPHelper(string serverHost, string userName, string userPassword)
        {
            ValidateHelper.Begin().NotNullOrEmpty(serverHost, "服务器IP").NotNullOrEmpty(UserName, "用户名").NotNullOrEmpty(userPassword, "用户密码");
            UserName = userName;
            UserPassword = userPassword;
            ServerHost = serverHost;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// 连接FTP服务器函数
        /// </summary>
        /// <param name="strServer">服务器IP</param>
        /// <param name="strUser">用户名</param>
        /// <param name="strPassword">密码</param>
        public bool Connected()
        {
            using (FtpClient ftp = new FtpClient())
            {
                ftp.Host = ServerHost;
                ftp.Credentials = new NetworkCredential(UserName, UserPassword);
                ftp.Connect();
                return ftp.IsConnected;
            }
        }

        /// <summary>
        /// FTP下载文件
        /// </summary>
        /// <param name="serverpath">服务器路径，eg："/Serverpath/"</param>
        /// <param name="localpath">本地保存路径</param>
        public bool DownloadFile(string serverpath, string localpath)
        {
            ValidateHelper.Begin().NotNullOrEmpty(serverpath, "服务器路径，eg：'/Serverpath/'").NotNullOrEmpty(localpath, "本地保存路径");

            FtpClient _ftpClient = new FtpClient();
            _ftpClient.Host = ServerHost;
            _ftpClient.Credentials = new NetworkCredential(UserName, UserPassword);
            _ftpClient.Connect();

            string _descDirectory = localpath;
            List<string> _docFileName = new List<string>();
            bool _downloadStatus = false;

            if (Directory.Exists(_descDirectory))
            {
                #region 从FTP服务器下载文件

                foreach (var ftpListItem in _ftpClient.GetListing(serverpath, FtpListOption.Modify | FtpListOption.Size))
                {
                    string _descPath = string.Format(@"{0}\{1}", _descDirectory, ftpListItem.Name);
                    using (Stream ftpStream = _ftpClient.OpenRead(ftpListItem.FullName))
                    {
                        using (FileStream fileStream = File.Create(_descPath, (int)ftpStream.Length))
                        {
                            var _buffer = new byte[200 * 1024];
                            int _count;
                            while ((_count = ftpStream.Read(_buffer, 0, _buffer.Length)) > 0)
                            {
                                fileStream.Write(_buffer, 0, _count);
                            }
                        }
                        _docFileName.Add(ftpListItem.Name);
                    }
                }

                #endregion 从FTP服务器下载文件

                #region 验证本地是否有该文件

                string[] _files = Directory.GetFiles(localpath);
                int _filenumber = 0;
                foreach (string filename in _files)
                {
                    foreach (string strrecievefile in _docFileName)
                    {
                        if (strrecievefile == Path.GetFileName(filename))
                        {
                            _filenumber++;
                            break;
                        }
                    }
                }
                if (_filenumber == _docFileName.Count)
                {
                    _downloadStatus = true;
                }

                #endregion 验证本地是否有该文件
            }
            return _downloadStatus;
        }

        #endregion Methods
    }
}