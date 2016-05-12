namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Assembly 帮助类
    /// </summary>
    public class AssemblyHelper
    {
        #region Fields

        /// <summary>
        /// Assembly对象
        /// </summary>
        private Assembly assembly = null;

        /// <summary>
        /// 程序集路径
        /// </summary>
        private string filePath = string.Empty;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyHelper"/> class.
        /// </summary>
        public AssemblyHelper()
        {
            assembly = Assembly.GetExecutingAssembly();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyHelper"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public AssemblyHelper(string path)
        {
            if (File.Exists(path))
            {
                assembly = Assembly.LoadFile(path);
                filePath = path;
            }
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// 获取程序集显示名称
        /// </summary>
        /// <returns>程序集显示名称</returns>
        public string GetAppFullName()
        {
            return assembly.FullName.ToString();
        }

        /// <summary>
        /// 获取编译日期
        /// </summary>
        /// <returns>编译日期</returns>
        public DateTime GetBuildDateTime()
        {
            if (File.Exists(filePath))
            {
                const int _peHeaderOffset = 60,
                          _linkerTimestampOffset = 8;
                byte[] _buffer = new byte[2048];
                Stream _readerStream = null;

                try
                {
                    _readerStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    _readerStream.Read(_buffer, 0, 2048);
                }
                finally
                {
                    if (_readerStream != null)
                    {
                        _readerStream.Close();
                    }
                }

                int _position = BitConverter.ToInt32(_buffer, _peHeaderOffset);
                int _since1970 = BitConverter.ToInt32(_buffer, _position + _linkerTimestampOffset);
                DateTime _builderDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                _builderDate = _builderDate.AddSeconds(_since1970);
                _builderDate = _builderDate.ToLocalTime();
                return _builderDate;
            }
            else
            {
                return new DateTime();
            }
        }

        /// <summary>
        /// 获取根据实际编译版本信息
        /// </summary>
        /// <returns>实际编译版本信息</returns>
        public DateTime GetBuildDateTimeByVersion()
        {
            if (File.Exists(filePath))
            {
                Version _version = assembly.GetName().Version;
                return new DateTime(2000, 01, 01).AddDays(_version.Build).AddSeconds(_version.Revision * 2);
            }
            else
            {
                return new DateTime();
            }
        }

        /// <summary>
        /// 获取公司名称信息
        /// </summary>
        /// <returns>公司名称信息</returns>
        public string GetCompany()
        {
            string _company = string.Empty;
            GetAssemblyCommon<AssemblyCompanyAttribute>(_ass => _company = _ass.Company);
            return _company;
        }

        /// <summary>
        /// 获取版权信息
        /// </summary>
        /// <returns>版权信息</returns>
        public string GetCopyright()
        {
            string _copyright = string.Empty;
            GetAssemblyCommon<AssemblyCopyrightAttribute>(_ass => _copyright = _ass.Copyright);
            return _copyright;
        }

        /// <summary>
        /// 获取说明信息
        /// </summary>
        /// <returns>说明信息</returns>
        public string GetDescription()
        {
            string _description = string.Empty;
            GetAssemblyCommon<AssemblyDescriptionAttribute>(_ass => _description = _ass.Description);
            return _description;
        }

        /// <summary>
        /// 获取产品名称信息
        /// </summary>
        /// <returns>产品名称信息</returns>
        public string GetProductName()
        {
            string _product = string.Empty;
            GetAssemblyCommon<AssemblyProductAttribute>(_ass => _product = _ass.Product);
            return _product;
        }

        /// <summary>
        /// 获取文件名
        /// </summary>
        /// <returns>文件名</returns>
        public string GetTitle()
        {
            string _title = string.Empty;
            GetAssemblyCommon<AssemblyTitleAttribute>(_ass => _title = _ass.Title);
            if (string.IsNullOrEmpty(_title))
            {
                _title = Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }

            return _title;
        }

        /// <summary>
        /// 获取主版本号，次版本号；
        /// </summary>
        /// <returns>主版本号，次版本号</returns>
        public string GetVersion()
        {
            return assembly.GetName().Version.ToString();
        }

        /// <summary>
        /// 获取程序集信息
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="assemblyFacotry">参数委托</param>
        /// 时间：2015-09-09 15:56
        /// 备注：
        private void GetAssemblyCommon<T>(Action<T> assemblyFacotry)
            where T : Attribute
        {
            if (assembly != null)
            {
                object[] _attributes = assembly.GetCustomAttributes(typeof(T), false);
                if (_attributes.Length > 0)
                {
                    T _attribute = (T)_attributes[0];
                    assemblyFacotry(_attribute);
                }
            }
        }

        #endregion Methods
    }
}