using Newtonsoft.Json;
using System;
using System.Data;

namespace YanZhiwei.DotNet.Newtonsoft.Json.Utilities
{
    /// <summary>
    /// DataRowConverter转换器
    /// </summary>
    internal class DataRowConverter : JsonConverter
    {
        /// <summary>
        /// 重写WriteJson
        /// </summary>
        public override void WriteJson(JsonWriter writer, object dataRow, JsonSerializer ser)
        {
            DataRow _row = dataRow as DataRow;
            writer.WriteStartObject();
            foreach (DataColumn column in _row.Table.Columns)
            {
                writer.WritePropertyName(column.ColumnName);
                ser.Serialize(writer, _row[column]);
            }
            writer.WriteEndObject();
        }

        /// <summary>
        /// override CanConvert
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type valueType)
        {
            return typeof(DataRow).IsAssignableFrom(valueType);
        }

        /// <summary>
        /// override ReadJson
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="ser"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer ser)
        {
            throw new NotImplementedException();
        }
    }
}