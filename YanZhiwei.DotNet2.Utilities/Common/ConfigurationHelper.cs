namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System.Configuration;
    using System.IO;
    using System.Web.Configuration;

    using YanZhiwei.DotNet2.Utilities.Enums;

    /// <summary>
    /// Configuration 帮助类
    /// </summary>
    public class ConfigurationHelper
    {
        #region Fields

        /// <summary>
        /// Configuration对象
        /// </summary>
        private Configuration config = null;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mode">程序模式</param>
        public ConfigurationHelper(ProgramMode mode)
        {
            switch (mode)
            {
                case ProgramMode.WebForm:
                    config = WebConfigurationManager.OpenWebConfiguration("~/");
                    break;

                case ProgramMode.WinForm:
                    config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    break;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mode">程序模式</param>
        /// <param name="filePath">config文件路径</param>
        public ConfigurationHelper(ProgramMode mode, string filePath)
        {
            switch (mode)
            {
                case ProgramMode.WinForm:
                    ExeConfigurationFileMap _configFileMap = new ExeConfigurationFileMap();
                    _configFileMap.ExeConfigFilename = filePath;
                    if (File.Exists(filePath))
                    {
                        config = ConfigurationManager.OpenMappedExeConfiguration(_configFileMap, ConfigurationUserLevel.None);
                    }

                    break;

                case ProgramMode.WebForm:
                    config = WebConfigurationManager.OpenWebConfiguration(filePath);
                    break;
            }
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// 读取
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="sctionKey">节点键</param>
        /// <returns>数值</returns>
        public T ReadSection<T>(string sctionKey)
            where T : ConfigurationSection
        {
            return config.Sections[sctionKey] as T;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="setion">节点值</param>
        /// <param name="sectionKey">节点键</param>
        public void SaveSection<T>(T setion, string sectionKey)
            where T : ConfigurationSection
        {
            config.Sections.Remove(sectionKey);
            config.Sections.Add(sectionKey, setion);
            config.Save();
        }

        #endregion Methods
    }
}