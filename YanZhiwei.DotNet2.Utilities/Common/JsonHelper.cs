namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    /// <summary>
    /// JSON转换类
    /// </summary>
    public static class JsonHelper
    {
        #region Methods

        /// <summary>
        /// DataSet转换为Json
        /// </summary>
        /// <param name="dataSet">DataSet对象</param>
        /// <returns>Json字符串</returns>
        public static string ToJson(this DataSet dataSet)
        {
            string _jsonString = "{";
            foreach (DataTable table in dataSet.Tables)
            {
                _jsonString += "\"" + table.TableName + "\":" + ToJson(table) + ",";
            }
            _jsonString = _jsonString.TrimEnd(',');
            return _jsonString + "}";
        }

        /// <summary>
        /// Datatable转换为Json
        /// </summary>
        /// <param name="table">Datatable对象</param>
        /// <returns>Json字符串</returns>
        public static string ToJson(this DataTable table)
        {
            StringBuilder _jsonString = new StringBuilder();
            _jsonString.Append("[");
            DataRowCollection drc = table.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                _jsonString.Append("{");
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    string _key = table.Columns[j].ColumnName;
                    string _value = drc[i][j].ToString();
                    Type _type = table.Columns[j].DataType;
                    _jsonString.Append("\"" + _key + "\":");
                    _value = StringFormat(_value, _type);
                    if (j < table.Columns.Count - 1)
                    {
                        _jsonString.Append(_value + ",");
                    }
                    else
                    {
                        _jsonString.Append(_value);
                    }
                }
                _jsonString.Append("},");
            }
            _jsonString.Remove(_jsonString.Length - 1, 1);
            _jsonString.Append("]");
            return _jsonString.ToString();
        }

        /// <summary>
        /// DataTable转换为Json
        /// </summary>
        public static string ToJson(this DataTable dataTable, string jsonName)
        {
            StringBuilder _jsonString = new StringBuilder();
            if (string.IsNullOrEmpty(jsonName)) jsonName = dataTable.TableName;
            _jsonString.Append("{\"" + jsonName + "\":[");
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    _jsonString.Append("{");
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        Type type = dataTable.Rows[i][j].GetType();
                        _jsonString.Append("\"" + dataTable.Columns[j].ColumnName.ToString() + "\":" + StringFormat(dataTable.Rows[i][j].ToString(), type));
                        if (j < dataTable.Columns.Count - 1)
                        {
                            _jsonString.Append(",");
                        }
                    }
                    _jsonString.Append("}");
                    if (i < dataTable.Rows.Count - 1)
                    {
                        _jsonString.Append(",");
                    }
                }
            }
            _jsonString.Append("]}");
            return _jsonString.ToString();
        }

        /// <summary>
        /// DataReader转换为Json
        /// </summary>
        /// <param name="dataReader">DataReader对象</param>
        /// <returns>Json字符串</returns>
        public static string ToJson(DbDataReader dataReader)
        {
            StringBuilder _jsonString = new StringBuilder();
            _jsonString.Append("[");
            while (dataReader.Read())
            {
                _jsonString.Append("{");
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    Type _type = dataReader.GetFieldType(i);
                    string _key = dataReader.GetName(i);
                    string _value = dataReader[i].ToString();
                    _jsonString.Append("\"" + _key + "\":");
                    _value = StringFormat(_value, _type);
                    if (i < dataReader.FieldCount - 1)
                    {
                        _jsonString.Append(_value + ",");
                    }
                    else
                    {
                        _jsonString.Append(_value);
                    }
                }
                _jsonString.Append("},");
            }
            dataReader.Close();
            _jsonString.Remove(_jsonString.Length - 1, 1);
            _jsonString.Append("]");
            return _jsonString.ToString();
        }

        /// <summary>
        /// 过滤特殊字符
        /// </summary>
        private static string HanlderJsonString(String data)
        {
            StringBuilder _builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                char _c = data.ToCharArray()[i];
                switch (_c)
                {
                    case '\"':
                        _builder.Append("\\\""); break;
                    case '\\':
                        _builder.Append("\\\\"); break;
                    case '/':
                        _builder.Append("\\/"); break;
                    case '\b':
                        _builder.Append("\\b"); break;
                    case '\f':
                        _builder.Append("\\f"); break;
                    case '\n':
                        _builder.Append("\\n"); break;
                    case '\r':
                        _builder.Append("\\r"); break;
                    case '\t':
                        _builder.Append("\\t"); break;
                    default:
                        _builder.Append(_c); break;
                }
            }
            return _builder.ToString();
        }

        /// <summary>
        /// 格式化字符型、日期型、布尔型
        /// </summary>
        private static string StringFormat(string data, Type type)
        {
            if (type == typeof(string))
            {
                data = HanlderJsonString(data);
                data = "\"" + data + "\"";
            }
            else if (type == typeof(DateTime))
            {
                data = "\"" + data + "\"";
            }
            else if (type == typeof(bool))
            {
                data = data.ToLower();
            }
            else if (type != typeof(string) && string.IsNullOrEmpty(data))
            {
                data = "\"" + data + "\"";
            }
            return data;
        }

        #endregion Methods
    }
}