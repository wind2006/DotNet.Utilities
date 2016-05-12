namespace YanZhiwei.DotNet3._5.Utilities.Core.ScriptConverter
{
    using System;
    using System.Collections.Generic;
    using System.Web.Script.Serialization;

    /// <summary>
    /// 时间类型转换器
    /// </summary>
    /// 时间：2015-12-10 14:48
    /// 备注：
    public class DateTimeConverter : JavaScriptConverter
    {
        #region Fields

        private string format = "yyyy-MM-dd HH:mm:ss";

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeConverter"/> class.
        /// </summary>
        /// 时间：2015-12-10 14:49
        /// 备注：
        public DateTimeConverter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeConverter"/> class.
        /// </summary>
        /// <param name="customFormat">The custom format.</param>
        /// 时间：2015-12-10 14:49
        /// 备注：
        public DateTimeConverter(string customFormat)
        {
            format = customFormat;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// When overridden in a derived class, gets a collection of the supported types.
        /// </summary>
        /// 时间：2015-12-10 14:49
        /// 备注：
        public override IEnumerable<Type> SupportedTypes
        {
            get { return new List<Type>() { typeof(DateTime), typeof(DateTime?) }; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Deserializes the specified dictionary.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="type">The type.</param>
        /// <param name="serializer">The serializer.</param>
        /// <returns></returns>
        /// 时间：2015-12-10 14:49
        /// 备注：
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            if (dictionary.ContainsKey("DateTime"))

                return new DateTime(long.Parse(dictionary["DateTime"].ToString()), DateTimeKind.Unspecified);

            return null;
        }

        /// <summary>
        /// Serializes the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="serializer">The serializer.</param>
        /// <returns></returns>
        /// 时间：2015-12-10 14:49
        /// 备注：
        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            if (obj == null) return result;

            result["DateTime"] = ((DateTime)obj).ToString(format);

            return result;
        }

        #endregion Methods
    }
}