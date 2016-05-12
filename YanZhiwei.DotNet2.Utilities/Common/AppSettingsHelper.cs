namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System.Configuration;
    using System.IO;
    using System.Web.Configuration;

    using YanZhiwei.DotNet2.Utilities.Enums;

    /// <summary>
    /// AppSetting节点操作 帮助类
    /// </summary>
    public class AppSettingsHelper
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
        /// <param name="mode">The mode.</param>
        public AppSettingsHelper(ProgramMode mode)
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
        /// Initializes a new instance of the <see cref="AppSettingsHelper"/> class.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="filePath">The file path.</param>
        public AppSettingsHelper(ProgramMode mode, string filePath)
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
        /// 添加或修改
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>是否添加或者修改成功</returns>
        public bool AddOrUpdate(string key, string value)
        {
            if (Exist())
            {
                KeyValueConfigurationElement _key = config.AppSettings.Settings[key];
                if (_key == null)
                {
                    config.AppSettings.Settings.Add(key, value);
                }
                else
                {
                    config.AppSettings.Settings[key].Value = value;
                }

                config.Save(ConfigurationSaveMode.Modified);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Exists this instance.
        /// </summary>
        /// <returns>是否存在</returns>
        public bool Exist()
        {
            bool _result = false;
            if (config != null)
            {
                _result = config.HasFile;
            }

            return _result;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>获取的值</returns>
        public string GetValue(string key)
        {
            if (Exist())
            {
                ConfigurationManager.RefreshSection("appSettings");
                return config.AppSettings.Settings[key].Value;
            }

            return null;
        }

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>移除是否成功</returns>
        public bool Remove(string key)
        {
            if (Exist())
            {
                config.AppSettings.Settings.Remove(key);
                config.Save(ConfigurationSaveMode.Modified);
                return true;
            }

            return false;
        }

        #endregion Methods
    }
}