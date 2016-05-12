namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System;
    using System.Data;
    using System.IO;

    /// <summary>
    /// IDataReader 帮助类
    /// </summary>
    /// 日期：2015-09-23 16:05
    /// 备注：
    public static class IDataReaderHelper
    {
        #region Methods

        /// <summary>
        /// 从IDataReader获取值
        /// </summary>
        /// <typeparam name="T">返回值泛型</typeparam>
        /// <param name="reader">IDataReader</param>
        /// <param name="columnName">列名称</param>
        /// <param name="failValue">如数值等于DBNull.Value时候返回的值</param>
        /// <returns>数值</returns>
        /// 日期：2015-09-23 16:03
        /// 备注：
        public static T GetValue<T>(this IDataReader reader, string columnName, T failValue)
        {
            bool _result = reader[columnName] != DBNull.Value;
            return _result == true ? (T)reader[columnName] : failValue;
        }

        /// <summary>
        /// 从IDataReader获取值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="reader">IDataReader</param>
        /// <param name="columnName">列名称</param>
        /// <returns>数值</returns>
        /// 时间：2016-01-04 16:52
        /// 备注：
        public static T GetValueOrDefault<T>(this IDataReader reader, string columnName)
        {
            return GetValueOrDefault<T>(reader, columnName, default(T));
        }

        /// <summary>
        /// 从IDataReader获取值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="reader">IDataReader</param>
        /// <param name="columnName">列名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>数值</returns>
        /// 时间：2016-01-04 16:52
        /// 备注：
        public static T GetValueOrDefault<T>(this IDataReader reader, string columnName, T defaultValue)
        {
            T _returnValue = defaultValue;
            object _columnValue = reader[columnName];
            if (!(_columnValue is DBNull))
            {
                Type _returnType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
                _returnValue = (T)Convert.ChangeType(_columnValue, _returnType);
            }
            return _returnValue;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="reader">IDataReader</param>
        /// <param name="columnIndex">读取索引</param>
        /// <param name="saveFilePath">保存路径</param>
        /// <param name="bufferSize">缓存大小</param>
        /// 时间：2016-01-04 16:07
        /// 备注：
        public static void SaveFile(this IDataReader reader, int columnIndex, string saveFilePath, int bufferSize)
        {
            if (bufferSize < 0)
                bufferSize = 65536;
            using (reader)
            {
                byte[] _buffer = new byte[bufferSize];
                int _offset = 0;
                using (FileStream tmpfile = File.Create(saveFilePath))
                {
                    if (reader.Read())
                    {
                        int _datasize = (int)reader.GetBytes(columnIndex, _offset, _buffer, 0, bufferSize);
                        while (_datasize == bufferSize)
                        {
                            tmpfile.Write(_buffer, 0, _datasize);
                            _offset += bufferSize;
                            _datasize = (int)reader.GetBytes(columnIndex, _offset, _buffer, 0, bufferSize);
                        }
                        tmpfile.Write(_buffer, 0, _datasize);
                    }
                }
            }
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="reader">IDataReader</param>
        /// <param name="index">读取索引</param>
        /// <param name="saveFilePath">保存路径</param>
        /// 时间：2016-01-04 16:47
        /// 备注：
        public static void SaveFile(this IDataReader reader, int index, string saveFilePath)
        {
            SaveFile(reader, index, saveFilePath, 65536);
        }

        #endregion Methods
    }
}