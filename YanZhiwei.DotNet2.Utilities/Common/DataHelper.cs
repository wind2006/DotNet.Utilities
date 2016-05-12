namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// 数据辅助操作类
    /// </summary>
    /// 时间：2016-02-25 12:00
    /// 备注：
    public static class DataHelper
    {
        #region Methods

        /// <summary>
        /// 创建Sql Server身份认证连接字符串
        /// </summary>
        /// <param name="server">服务器</param>
        /// <param name="database">数据库</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>Sql Server身份认证连接字符串</returns>
        public static string CreateSqlServerConnectString(string server, string database, string userName, string password)
        {
            return string.Format(@"Server={0};DataBase={1};uid={2};pwd={3};", server, database, userName, password);
        }

        /// <summary>
        /// 创建Sql Server Windows身份认证连接字符串
        /// </summary>
        /// <param name="server">服务器</param>
        /// <param name="datatabase">数据库</param>
        /// <returns>Sql Server Windows身份认证连接字符串</returns>
        /// 时间：2016-02-26 9:21
        /// 备注：
        public static string CreateSqlServerConnectString(string server, string datatabase)
        {
            return string.Format(@"Server={0}; Database={1}; Integrated Security=True;", server, datatabase);
        }

        /// <summary>
        /// 创建Datatable，规范：列名|列类型,列名|列类型,列名|列类型
        /// <para>举例：CustomeName|string,Gender|bool,Address</para>
        /// </summary>
        /// <param name="columnsInfo">创建表的字符串规则信息</param>
        /// <returns>DataTable</returns>
        public static DataTable CreateTable(string columnsInfo)
        {
            DataTable _dtNew = new DataTable();
            string[] _columnsList = columnsInfo.Split(',');
            string _columnName;
            string _columnType;
            string[] _singleColumnInfo;
            foreach (string s in _columnsList)
            {
                _singleColumnInfo = s.Split('|');
                _columnName = _singleColumnInfo[0];
                if (_singleColumnInfo.Length == 2)
                {
                    _columnType = _singleColumnInfo[1];
                    _dtNew.Columns.Add(new DataColumn(_columnName, Type.GetType(TransColumnType(_columnType))));
                }
                else
                {
                    _dtNew.Columns.Add(new DataColumn(_columnName));
                }
            }

            return _dtNew;
        }

        /// <summary>
        /// 过滤HTML标记
        /// </summary>
        /// <param name="data">包括HTML，脚本，数据库关键字，特殊字符的源码 </param>
        /// <returns>已经去除标记后的文字</returns>
        public static string FilterHtmlTag(this string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                data = Regex.Replace(data, @"<script[^>]*?>.*?</script>", string.Empty, RegexOptions.IgnoreCase);
                data = Regex.Replace(data, @"<(.[^>]*)>", string.Empty, RegexOptions.IgnoreCase);
                data = Regex.Replace(data, @"([\r\n])[\s]+", string.Empty, RegexOptions.IgnoreCase);
                data = Regex.Replace(data, @"-->", string.Empty, RegexOptions.IgnoreCase);
                data = Regex.Replace(data, @"<!--.*", string.Empty, RegexOptions.IgnoreCase);
                data = Regex.Replace(data, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, @"&#(\d+);", string.Empty, RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "xp_cmdshell", string.Empty, RegexOptions.IgnoreCase);
            }

            return data;
        }

        /// <summary>
        /// 过滤特殊字符
        /// </summary>
        /// <param name="data">字符串</param>
        /// <returns>过滤后的字符串</returns>
        public static string FilterSpecial(this string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                data = data.Replace("<", string.Empty);
                data = data.Replace(">", string.Empty);
                data = data.Replace("*", string.Empty);
                data = data.Replace("-", string.Empty);
                data = data.Replace("?", string.Empty);
                data = data.Replace("'", "''");
                data = data.Replace(",", string.Empty);
                data = data.Replace("/", string.Empty);
                data = data.Replace(";", string.Empty);
                data = data.Replace("*/", string.Empty);
                data = data.Replace("\r\n", string.Empty);
            }

            return data;
        }

        /// <summary>
        /// 过滤SQL语句字符串中的注入脚本
        /// </summary>
        /// <param name="data">传入的字符串</param>
        /// <returns>过滤后的字符串</returns>
        public static string FilterSqlInject(this string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                data = data.Replace("'", "''");
                data = data.Replace(";", "；");
                data = data.Replace("(", "（");
                data = data.Replace(")", "）");
                data = data.Replace("Exec", string.Empty);
                data = data.Replace("Execute", string.Empty);
                data = data.Replace("xp_", "x p_");
                data = data.Replace("sp_", "s p_");
                data = data.Replace("0x", "0 x");
            }

            return data;
        }

        /// <summary>
        /// SQL注入筛选
        /// </summary>
        /// <param name="str">sql语句</param>
        /// <returns>SQL注入筛选</returns>
        public static string FilterSqlInjection(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace(";", string.Empty);
                str = str.Replace("'", string.Empty);
                str = str.Replace("&", string.Empty);
                str = str.Replace("%20", string.Empty);
                str = str.Replace("-", string.Empty);
                str = str.Replace("=", string.Empty);
                str = str.Replace("==", string.Empty);
                str = str.Replace("<", string.Empty);
                str = str.Replace(">", string.Empty);
                str = str.Replace("%", string.Empty);
                str = str.Replace(" or", string.Empty);
                str = str.Replace("or ", string.Empty);
                str = str.Replace(" and", string.Empty);
                str = str.Replace("and ", string.Empty);
                str = str.Replace(" not", string.Empty);
                str = str.Replace("not ", string.Empty);
                str = str.Replace("!", string.Empty);
                str = str.Replace("{", string.Empty);
                str = str.Replace("}", string.Empty);
                str = str.Replace("[", string.Empty);
                str = str.Replace("]", string.Empty);
                str = str.Replace("(", string.Empty);
                str = str.Replace(")", string.Empty);
                str = str.Replace("|", string.Empty);
                str = str.Replace("_", string.Empty);
            }

            return str;
        }

        /// <summary>
        /// 过滤字符串【HTML标记，敏感SQL操作关键，特殊字符】
        /// </summary>
        /// <param name="data">字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string FilterString(this string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                data = FilterHtmlTag(data);
                data = FilterUnSafeSQL(data);
                data = FilterSpecial(data);
            }

            return data;
        }

        /// <summary>
        /// 过滤SQL不安全的字符串
        /// </summary>
        /// <param name="data">字符串</param>
        /// <returns>过滤后的字符串</returns>
        public static string FilterUnSafeSQL(this string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                data = data.Replace("'", string.Empty);
                data = data.Replace("\"", string.Empty);
                data = data.Replace("&", "&amp");
                data = data.Replace("<", "&lt");
                data = data.Replace(">", "&gt");
                data = Regex.Replace(data, "select", string.Empty, RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "insert", string.Empty, RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "delete from", string.Empty, RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "count''", string.Empty, RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "drop table", string.Empty, RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "truncate", string.Empty, RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "asc", string.Empty, RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "mid", string.Empty, RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "char", string.Empty, RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "xp_cmdshell", string.Empty, RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "exec master", string.Empty, RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "net localgroup administrators", string.Empty, RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "and", string.Empty, RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "net user", string.Empty, RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "or", string.Empty, RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "net", string.Empty, RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "-", string.Empty, RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "delete", string.Empty, RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "drop", string.Empty, RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "script", string.Empty, RegexOptions.IgnoreCase);
            }

            return data;
        }

        /// <summary>
        /// 由错误码返回指定的自定义SqlException异常信息
        /// </summary>
        /// <param name="sqlException">SqlException</param>
        /// <returns>SqlException异常信息</returns>
        public static string GetSqlExceptionMessage(this SqlException sqlException)
        {
            string _msg = GetSqlExceptionMessage(sqlException.Number);
            if (string.IsNullOrEmpty(_msg))
                _msg = sqlException.Message.Trim();
            return _msg;
        }

        /// <summary>
        /// 由错误码返回指定的自定义SqlException异常信息
        /// <para>DataHelper.GetSqlExceptionMessage(sqlEx.Number);</para>
        /// </summary>
        /// <param name="number">错误标识数字 </param>
        /// <returns>描述信息 </returns>
        public static string GetSqlExceptionMessage(int number)
        {
            string _msg = string.Empty;
            switch (number)
            {
                case 2:
                    _msg = "连接数据库超时，请检查网络连接或者数据库服务器是否正常。";
                    break;

                case 17:
                    _msg = "SqlServer服务不存在或拒绝访问。";
                    break;

                case 17142:
                    _msg = "SqlServer服务已暂停，不能提供数据服务。";
                    break;

                case 2812:
                    _msg = "指定存储过程不存在。";
                    break;

                case 208:
                    _msg = "指定名称的表不存在。";
                    break;

                case 4060: //数据库无效。
                    _msg = "所连接的数据库无效。";
                    break;

                case 18456: //登录失败
                    _msg = "使用设定的用户名与密码登录数据库失败。";
                    break;

                case 547:
                    _msg = "外键约束，无法保存数据的变更。";
                    break;

                case 2627:
                    _msg = "主键重复，无法插入数据。";
                    break;

                case 2601:
                    _msg = "未知错误。";
                    break;
            }
            return _msg;
        }

        /// <summary>
        /// DataTable的列求和
        /// </summary>
        /// <param name="datatable">DataTable</param>
        /// <param name="sumColumnName">sum的列</param>
        /// <returns>计算的值</returns>
        public static object GetSumByColumn(this DataTable datatable, string sumColumnName)
        {
            object _result = null;
            if (datatable != null && !string.IsNullOrEmpty(sumColumnName))
            {
                _result = datatable.Compute("Sum(" + sumColumnName + ")", string.Empty);
            }

            return _result;
        }

        /// <summary>
        /// DataTable的group by sum计算
        /// <para>eg:eg:DBHelper.GroupByToSum(_dt, "CTLampType", "钠灯- 100W", "CTLastMonthCount");</para>
        /// </summary>
        /// <param name="datatable">DataTable</param>
        /// <param name="gColumnName">分组字段名称</param>
        /// <param name="gValue">分组数值</param>
        /// <param name="sColumnName">求和字段名称</param>
        /// <returns>object</returns>
        public static object GetSumByGroup(this DataTable datatable, string gColumnName, string gValue, string sColumnName)
        {
            object _result = null;
            if (datatable != null && !string.IsNullOrEmpty(gColumnName) && !string.IsNullOrEmpty(sColumnName))
            {
                _result = datatable.Compute("Sum(" + sColumnName + ")", " " + gColumnName + "='" + gValue + "'");
            }

            return _result;
        }

        /// <summary>
        /// 将连接字符串转换成字典
        /// </summary>
        /// <param name="connectString">The connect string.eg:IP=127.0.0.1; DataSource=YanZhiwei-PC;User=sa;Password=sasa;DataBase=NorthWind</param>
        /// <returns>字典IDictionary</returns>
        public static IDictionary<string, string> ParseConnnectString(this string connectString)
        {
            string[] _groups = connectString.Split(';');
            IDictionary<string, string> _dic = new Dictionary<string, string>();
            foreach (string group in _groups)
            {
                string _groupOk = group.Trim();
                if (!string.IsNullOrEmpty(_groupOk))
                {
                    string[] keyVal = _groupOk.Split('=');
                    _dic.Add(keyVal[0].Trim(), keyVal[1].Trim());
                }
            }

            return _dic;
        }

        /// <summary>
        /// 将LIST集合转换对应的DataTable
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="data">集合</param>
        /// <returns>将LIST集合转换对应的DataTable</returns>
        public static DataTable ParseList<T>(this List<T> data)
        {
            PropertyDescriptorCollection _props = TypeDescriptor.GetProperties(typeof(T));
            DataTable _table = new DataTable();
            _table = CreateColumns(_props, _table);
            _table = FillRow(data, _props, _table);
            return _table;
        }

        /// <summary>
        /// 将DataTable导出到CSV.
        /// </summary>
        /// <param name="table">DataTable</param>
        /// <param name="fullSavePath">保存路径</param>
        /// <param name="tableheader">标题信息</param>
        /// <param name="columname">列名称『eg:姓名,年龄』</param>
        /// <returns>导出成功true;导出失败false</returns>
        public static bool ToCSV(this DataTable table, string fullSavePath, string tableheader, string columname)
        {
            //------------------------------------------------------------------------------------
            try
            {
                string _bufferLine = string.Empty;
                using (StreamWriter _writerObj = new StreamWriter(fullSavePath, false, Encoding.UTF8))
                {
                    if (!string.IsNullOrEmpty(tableheader))
                    {
                        _writerObj.WriteLine(tableheader);
                    }

                    if (!string.IsNullOrEmpty(columname))
                    {
                        _writerObj.WriteLine(columname);
                    }

                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        _bufferLine = string.Empty;
                        for (int j = 0; j < table.Columns.Count; j++)
                        {
                            if (j > 0)
                            {
                                _bufferLine += ",";
                            }

                            _bufferLine += table.Rows[i][j].ToString();
                        }

                        _writerObj.WriteLine(_bufferLine);
                    }

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 将IDataReader转换成List
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="dataReader">IDataReader</param>
        /// <returns>集合</returns>
        public static List<T> ToEntitys<T>(this IDataReader dataReader)
            where T : new()
        {
            if (dataReader == null)
            {
                return null;
            }

            Type _type = typeof(T);
            Hashtable _tProperties = new Hashtable();
            PropertyInfo[] _tPropertiesCache = _type.GetProperties();
            foreach (PropertyInfo info in _tPropertiesCache)
            {
                _tProperties[info.Name.ToUpper()] = info;
            }

            List<T> _entitys = new List<T>();
            while (dataReader.Read())
            {
                T _entity = new T();
                for (int index = 0; index < dataReader.FieldCount; index++)
                {
                    PropertyInfo _rProperty = (PropertyInfo)_tProperties[dataReader.GetName(index).ToUpper()];
                    if ((_rProperty == null) || !_rProperty.CanWrite)
                    {
                        continue;
                    }

                    object _rValue = dataReader.GetValue(index);
                    Type _rType = _rValue.GetType();
                    if (_rType == typeof(Guid))
                    {
                        _rProperty.SetValue(_entity, _rValue == DBNull.Value ? null : _rValue.ToString(), null);
                    }
                    else
                    {
                        _rProperty.SetValue(_entity, _rValue == DBNull.Value ? null : _rValue, null);
                    }
                }

                _entitys.Add(_entity);
            }

            dataReader.Close();
            return _entitys;
        }

        /// <summary>
        /// 创建列
        /// </summary>
        /// <param name="property">属性</param>
        /// <param name="dataTable">DataTable</param>
        /// <returns>DataTable</returns>：
        private static DataTable CreateColumns(PropertyDescriptorCollection property, DataTable dataTable)
        {
            if (dataTable != null && property != null)
            {
                for (int i = 0; i < property.Count; i++)
                {
                    PropertyDescriptor prop = property[i];
                    dataTable.Columns.Add(prop.Name, prop.PropertyType);
                }
            }

            return dataTable;
        }

        /// <summary>
        /// 填充数据.
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="data">集合数据</param>
        /// <param name="property">属性</param>
        /// <param name="dataTable">DataTable</param>
        /// <returns>DataTable</returns>
        private static DataTable FillRow<T>(IList<T> data, PropertyDescriptorCollection property, DataTable dataTable)
        {
            object[] _values = new object[property.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < _values.Length; i++)
                {
                    _values[i] = property[i].GetValue(item);
                }

                dataTable.Rows.Add(_values);
            }

            return dataTable;
        }

        /// <summary>
        /// 转义数据类型
        /// </summary>
        /// <param name="columnType">列类型</param>
        /// <returns>转义后实际数据类型</returns>
        private static string TransColumnType(string columnType)
        {
            string _currentType = string.Empty;
            switch (columnType.ToLower())
            {
                case "int":
                    _currentType = "System.Int32";
                    break;

                case "string":
                    _currentType = "System.String";
                    break;

                case "decimal":
                    _currentType = "System.Decimal";
                    break;

                case "double":
                    _currentType = "System.Double";
                    break;

                case "dateTime":
                    _currentType = "System.DateTime";
                    break;

                case "bool":
                    _currentType = "System.Boolean";
                    break;

                case "image":
                    _currentType = "System.Byte[]";
                    break;

                case "object":
                    _currentType = "System.Object";
                    break;

                default:
                    _currentType = "System.String";
                    break;
            }

            return _currentType;
        }

        #endregion Methods
    }
}