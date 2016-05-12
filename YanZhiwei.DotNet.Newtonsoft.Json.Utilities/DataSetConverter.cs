using Newtonsoft.Json;
using System;
using System.Data;

namespace YanZhiwei.DotNet.Newtonsoft.Json.Utilities
{
    /// <summary>
    /// DataSetConverter转换器
    /// </summary>
    internal class DataSetConverter : JsonConverter
    {
        /// <summary>
        /// override CanConvert
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type valueType)
        {
            return typeof(DataSet).IsAssignableFrom(valueType);
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

        /// <summary>
        /// override WriteJson
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="dataset"></param>
        /// <param name="ser"></param>
        public override void WriteJson(JsonWriter writer, object dataset, JsonSerializer ser)
        {
            DataSet _dataSet = dataset as DataSet;
            DataTableConverter _converter = new DataTableConverter();
            writer.WriteStartObject();
            writer.WritePropertyName("Tables");
            writer.WriteStartArray();
            foreach (DataTable table in _dataSet.Tables)
            {
                _converter.WriteJson(writer, table, ser);
            }
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
    }
}