namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System.Collections.Specialized;
    using System.Configuration;

    /// <summary>
    /// 配置文件 Section节点操作帮助类
    /// </summary>
    public class SectionHelper
    {
        #region Fields

        /// <summary>
        /// NameValueCollection对象
        /// </summary>
        private NameValueCollection modulSettings = null;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sectionName">section名称</param>
        public SectionHelper(string sectionName)
        {
            modulSettings = ConfigurationManager.GetSection(sectionName) as NameValueCollection;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Section是否包含Key
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public bool ContainKey(string key)
        {
            if (ContainSection())
            {
                return !(modulSettings[key] == null);
            }

            return false;
        }

        /// <summary>
        /// 是否包含该Section
        /// </summary>
        /// <returns>是否包含</returns>
        public bool ContainSection()
        {
            return !(modulSettings == null);
        }

        /// <summary>
        /// 根据键获取值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>当不存在键的时候，返回string.Empty</returns>
        public string GetValue(string key)
        {
            string _value = string.Empty;
            if (ContainKey(key))
            {
                _value = modulSettings[key];
            }

            return _value;
        }

        #endregion Methods
    }
}