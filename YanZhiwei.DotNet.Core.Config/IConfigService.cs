namespace YanZhiwei.DotNet.Core.Config
{
    /// <summary>
    /// 配置服务接口
    /// </summary>
    public interface IConfigService
    {
        #region Methods

        /// <summary>
        /// 根据名称获取配置
        /// </summary>
        /// <param name="filename">配置文件</param>
        /// <returns>字符串</returns>
        /// 时间：2015-12-30 17:05
        /// 备注：
        string GetConfig(string filename);

        /// <summary>
        /// 根据名称获取配置文件路径
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <returns>文件路径</returns>
        string GetFilePath(string name);

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <param name="content">配置内容</param>
        void SaveConfig(string name, string content);

        #endregion Methods
    }
}