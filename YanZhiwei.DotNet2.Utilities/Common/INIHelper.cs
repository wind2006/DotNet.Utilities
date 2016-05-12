namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// INI工具类
    ///<para>参考:</para>
    /// </summary>
    public class INIHelper
    {
        #region Fields

        private static string FilePath = null;

        /// <summary>
        /// 当读取不到值得时候缺省值
        /// </summary>
        private static string ReadDefaultValue = string.Empty;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="filePath">INI路径eg：@"C:\test.ini"</param>
        public INIHelper(string filePath)
        {
            FilePath = filePath;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// 检查INI文件路径是否存在
        /// </summary>
        /// <returns>是否存在</returns>
        public bool Exist()
        {
            if (!string.IsNullOrEmpty(FilePath))
            {
                return File.Exists(FilePath);
            }
            return false;
        }

        /// <summary>
        /// 读取INI
        /// </summary>
        /// <param name="Section">段落名称</param>
        /// <param name="Key">关键字</param>
        /// <returns>读取值</returns>
        public string ReadValue(string Section, string Key)
        {
            StringBuilder _valueBuilder = new StringBuilder(500);
            GetPrivateProfileString(Section, Key, ReadDefaultValue, _valueBuilder, 500, FilePath);
            return _valueBuilder.ToString();
        }

        /// <summary>
        /// 读取INI
        /// </summary>
        /// <param name="Section">段落名称</param>
        /// <param name="Key">关键字</param>
        /// <param name="defaultValue">当根据KEY读取不到值得时候缺省值</param>
        /// <returns>读取的值</returns>
        public string ReadValue(string Section, string Key, string defaultValue)
        {
            StringBuilder _valueBuilder = new StringBuilder(500);
            GetPrivateProfileString(Section, Key, defaultValue, _valueBuilder, 500, FilePath);
            return _valueBuilder.ToString();
        }

        /// <summary>
        /// 写入INI
        /// eg:_iniHelper.WriteValue("测试", "Name", "YanZhiwei");
        /// </summary>
        /// <param name="Section">段落名称</param>
        /// <param name="Key">关键字</param>
        /// <param name="Value">关键字对应的值</param>
        public void WriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, FilePath);
        }

        /// <summary>
        /// 将对象保存在ini
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="Section">段落名称</param>
        /// <param name="t">类型</param>
        public void WriteValue<T>(string Section, T t)
            where T : class
        {
            IDictionary<string, string> _property = ReflectHelper.ToDictionary<T>();
            foreach (KeyValuePair<string, string> entry in _property)
            {
                object _value = typeof(T).InvokeMember(entry.Key, BindingFlags.GetProperty, null, t, null);
                Trace.WriteLine(_value);
                if (_value != null)
                    WriteValue(Section, entry.Value, _value.ToString());
            }
        }

        /// <summary>
        /// 声明INI文件的读操作函数
        /// </summary>
        /// <param name="section">段落名称</param>
        /// <param name="key">关键字</param>
        /// <param name="def">无法读取时候时候的缺省数值</param>
        /// <param name="retVal">读取数值</param>
        /// <param name="size">数值的大小></param>
        /// <param name="filePath">路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// 声明INI文件的写操作函数
        /// </summary>
        /// <param name="section">段落名称</param>
        /// <param name="key">关键字</param>
        /// <param name="val">关键字对应的值</param>
        /// <param name="filePath">路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        #endregion Methods

        #region Other

        /*
         * 参考：
         * 1. http://www.cnblogs.com/leelike/archive/2011/01/27/1946061.html
         * 2. http://www.cnblogs.com/zzyyll2/archive/2007/11/06/950584.html
         */

        #endregion Other
    }
}