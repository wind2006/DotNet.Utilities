namespace YanZhiwei.DotNet2.Utilities.Core
{
    using System.Configuration;

    /// <summary>
    /// 获取默认的连接字符串
    /// </summary>
    /// 时间：2015-12-25 17:21
    /// 备注：
    public class ConfigSettings
    {
        #region Constructors

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static ConfigSettings()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 默认的连接字符串,name='default'
        /// </summary>
        public static string ConnectionString
        {
            get; set;
        }

        #endregion Properties
    }
}