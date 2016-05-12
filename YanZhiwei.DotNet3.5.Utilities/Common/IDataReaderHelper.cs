namespace YanZhiwei.DotNet3._5.Utilities.Common
{
    using System;
    using System.Data;

    /// <summary>
    /// IDataReader 帮助类
    /// </summary>
    /// 时间：2016-01-05 16:54
    /// 备注：
    public static class IDataReaderHelper
    {
        #region Methods

        /// <summary>
        ///  从IDataReader获取值
        /// </summary>
        /// <param name="reader">IDataReader</param>
        /// <param name="columnName">列名称</param>
        /// <returns>T?</returns>
        /// 时间：2016-01-05 16:54
        /// 备注：
        public static bool? GetNullableBool(this IDataRecord reader, string columnName)
        {
            int _ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(_ordinal) ? (bool?)null : reader.GetBoolean(_ordinal);
        }

        /// <summary>
        ///  从IDataReader获取值
        /// </summary>
        /// <param name="reader">IDataReader</param>
        /// <param name="columnName">列名称</param>
        /// <returns>T?</returns>
        /// 时间：2016-01-05 16:54
        /// 备注：
        public static byte? GetNullableByte(this IDataRecord reader, string columnName)
        {
            int _ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(_ordinal) ? (byte?)null : reader.GetByte(_ordinal);
        }

        /// <summary>
        ///  从IDataReader获取值
        /// </summary>
        /// <param name="reader">IDataReader</param>
        /// <param name="columnName">列名称</param>
        /// <returns>T?</returns>
        /// 时间：2016-01-05 16:54
        /// 备注：
        public static DateTime? GetNullableDateTime(this IDataRecord reader, string columnName)
        {
            int _ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(_ordinal) ? (DateTime?)null : reader.GetDateTime(_ordinal);
        }

        /// <summary>
        ///  从IDataReader获取值
        /// </summary>
        /// <param name="reader">IDataReader</param>
        /// <param name="columnName">列名称</param>
        /// <returns>T?</returns>
        /// 时间：2016-01-05 16:54
        /// 备注：
        public static double? GetNullableDouble(this IDataRecord reader, string columnName)
        {
            int _ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(_ordinal) ? (double?)null : reader.GetDouble(_ordinal);
        }

        /// <summary>
        ///  从IDataReader获取值
        /// </summary>
        /// <param name="reader">IDataReader</param>
        /// <param name="columnName">列名称</param>
        /// <returns>T?</returns>
        /// 时间：2016-01-05 16:54
        /// 备注：
        public static float? GetNullableFloat(this IDataRecord reader, string columnName)
        {
            int _ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(_ordinal) ? (float?)null : reader.GetFloat(_ordinal);
        }

        /// <summary>
        ///  从IDataReader获取值
        /// </summary>
        /// <param name="reader">IDataReader</param>
        /// <param name="columnName">列名称</param>
        /// <returns>T?</returns>
        /// 时间：2016-01-05 16:54
        /// 备注：
        public static short? GetNullableInt16(this IDataRecord reader, string columnName)
        {
            int _ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(_ordinal) ? (short?)null : reader.GetInt16(_ordinal);
        }

        /// <summary>
        ///  从IDataReader获取值
        /// </summary>
        /// <param name="reader">IDataReader</param>
        /// <param name="columnName">列名称</param>
        /// <returns>T?</returns>
        /// 时间：2016-01-05 16:54
        /// 备注：
        public static int? GetNullableInt32(this IDataRecord reader, string columnName)
        {
            int _ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(_ordinal) ? (int?)null : reader.GetInt32(_ordinal);
        }

        /// <summary>
        /// 从IDataReader获取值
        /// </summary>
        /// <param name="reader">IDataReader</param>
        /// <param name="columnName">列名称</param>
        /// <returns>数值</returns>
        /// 时间：2016-01-05 16:51
        /// 备注：
        public static string GetNullableString(this IDataRecord reader, string columnName)
        {
            int _ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(_ordinal) ? null : reader.GetString(_ordinal);
        }

        /// <summary>
        ///  从IDataReader获取值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="reader">IDataReader</param>
        /// <param name="columnName">列名称</param>
        /// <returns>T?</returns>
        /// 时间：2016-01-05 16:31
        /// 备注：
        public static T? GetValueOrNull<T>(IDataReader reader, string columnName)
            where T : struct
        {
            T? _value = null;
            int _ordinal = reader.GetOrdinal(columnName);
            if (!reader.IsDBNull(_ordinal))
                _value = (T)reader[columnName];
            return (_value);
        }

        #endregion Methods
    }
}