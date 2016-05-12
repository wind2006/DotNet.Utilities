namespace YanZhiwei.DotNet.Core.Config
{
    using System;
    using System.IO;

    /// <summary>
    /// 配置以文件形式保存在使用目录下的Config，可以实现DBConfigService保存到数据库里去
    /// </summary>
    public class FileConfigService : IConfigService
    {
        #region Fields

        /// <summary>
        /// 默认配置存储文件夹
        /// </summary>
        /// 时间：2015-12-30 17:07
        /// 备注：
        private readonly string configFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");

        #endregion Fields

        #region Methods

        /// <summary>
        /// 根据文件名称获取配置内容
        /// </summary>
        /// <param name="fileName">配置文件</param>
        /// <returns>配置内容</returns>
        /// 时间：2015-12-30 17:07
        /// 备注：
        public string GetConfig(string fileName)
        {
        
            string _configPath = GetFilePath(fileName);
            if (!File.Exists(_configPath))
                return null;
            else
                return File.ReadAllText(_configPath);
        }

        /// <summary>
        /// 获取配置文件路径
        /// </summary>
        /// <param name="fileName">配置文件名称</param>
        /// <returns>配置文件路径</returns>
        /// 时间：2015-12-30 17:11
        /// 备注：
        public string GetFilePath(string fileName)
        {
            if (!Directory.Exists(configFolder))
                Directory.CreateDirectory(configFolder);
            string _configPath = string.Format(@"{0}\{1}.xml", configFolder, fileName);
            return _configPath;
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="fileName">配置文件</param>
        /// <param name="content">内容</param>
        /// 时间：2015-12-30 17:11
        /// 备注：
        public void SaveConfig(string fileName, string content)
        {
            string _configPath = GetFilePath(fileName);
            File.WriteAllText(_configPath, content);
        }

        #endregion Methods
    }
}