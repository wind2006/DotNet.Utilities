namespace YanZhiwei.DotNet3._5.Utilities.Common
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.Script.Serialization;
    using System.Xml.Serialization;

    /// <summary>
    /// JavaScriptSerializer 帮助类
    /// </summary>
    public static class SerializationHelper
    {
        #region Methods

        /// <summary>
        /// 将使用二进制格式保存的byte数组反序列化成对象
        /// </summary>
        /// <param name="buffer">byte数组</param>
        /// <returns>对象</returns>
        public static object BinaryDeserialize(byte[] buffer)
        {
            object _result = null;

            if (buffer != null)
            {
                using (MemoryStream stream = new MemoryStream(buffer))
                {
                    BinaryFormatter _binarySerializer = new BinaryFormatter();
                    _result = _binarySerializer.Deserialize(stream);
                }
            }
            return _result;
        }

        /// <summary>
        /// 将对象使用二进制格式序列化成byte数组
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>byte数组</returns>
        public static byte[] BinarySerialize(object obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter _binarySerializer = new BinaryFormatter();
                _binarySerializer.Serialize(stream, obj);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// 利用DataContractSerializer反序列化
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="serializerString">字符串</param>
        /// <returns>object</returns>
        /// 时间：2015-12-31 10:00
        /// 备注：
        public static object DataContractDeserialize(Type type, string serializerString)
        {
            serializerString = serializerString.Trim();
            if (string.IsNullOrEmpty(serializerString))
            {
                return null;
            }

            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(serializerString)))
            {
                DataContractSerializer _dataContractSerializer = new DataContractSerializer(type);
                return _dataContractSerializer.ReadObject(stream);
            }
        }

        /// <summary>
        /// 利用DataContractSerializer对象序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>字符串</returns>
        /// 时间：2015-12-31 9:59
        /// 备注：
        public static string DataContractSerialize(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            using (MemoryStream stream = new MemoryStream())
            {
                DataContractSerializer _dataContractSerializer = new DataContractSerializer(obj.GetType());
                _dataContractSerializer.WriteObject(stream, obj);

                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        /// <summary>
        /// 利用JavaScriptSerializer将json字符串反序列化
        /// </summary>
        /// <param name="jsonString">Json字符串</param>
        /// <returns>object</returns>
        /// 时间：2015-09-09 14:35
        /// 备注：
        public static object JsonDeserialize(this string jsonString)
        {
            JavaScriptSerializer _jsonHelper = new JavaScriptSerializer();
            return _jsonHelper.DeserializeObject(jsonString);
        }

        /// <summary>
        /// 利用JavaScriptSerializer将json字符串反序列化.
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="jsonString">Json字符串</param>
        /// <returns>object</returns>
        /// 时间：2016-01-14 11:24
        /// 备注：
        public static T JsonDeserialize<T>(this string jsonString) where T : class
        {
            JavaScriptSerializer _jsonHelper = new JavaScriptSerializer();
            return _jsonHelper.Deserialize<T>(jsonString);
        }

        /// <summary>
        /// 利用JavaScriptSerializer将对象序列化成JSON字符串
        /// <para>eg:ScriptSerializerHelper.ToJson(_personList);</para>
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="entityList">集合</param>
        /// <param name="scriptConverters">JavaScriptConverter</param>
        /// <returns>Json字符串</returns>
        /// 时间：2015-09-09 14:34
        /// 备注：
        public static string JsonSerialize<T>(this IEnumerable<T> entityList, params JavaScriptConverter[] scriptConverters)
            where T : class
        {
            string _jsonString = string.Empty;
            if (entityList != null)
            {
                JavaScriptSerializer _jsonHelper = new JavaScriptSerializer();
                if (scriptConverters != null)
                {
                    _jsonHelper.RegisterConverters(scriptConverters);
                }

                _jsonHelper.MaxJsonLength = int.MaxValue;
                _jsonString = _jsonHelper.Serialize(entityList);
            }

            return _jsonString;
        }

        /// <summary>
        /// 将对象序列化Json字符串
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="entity">对象</param>
        /// <returns>json</returns>
        public static string JsonSerialize<T>(this T entity)
        {
            string _jsonString = string.Empty;
            if (entity != null)
            {
                JavaScriptSerializer _jsonHelper = new JavaScriptSerializer();
                _jsonHelper.MaxJsonLength = int.MaxValue;
                _jsonString = _jsonHelper.Serialize(entity);
            }

            return _jsonString;
        }

        /// <summary>
        /// 处理JsonString的时间格式问题【时间格式：yyyy-MM-dd HH:mm:ss】
        /// <para>参考：http://www.cnphp6.com/archives/35773 </para>
        /// </summary>
        /// <param name="jsonString">Json字符串</param>
        /// <returns>处理好的Json字符串</returns>
        public static string ParseJsonDateTime(this string jsonString)
        {
            return ParseJsonDateTime(jsonString, "yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 处理JsonString的时间格式问题
        /// <para>eg:ScriptSerializerHelper.ConvertTimeJson(@"[{'getTime':'\/Date(1419564257428)\/'}]", "yyyyMMdd hh:mm:ss");==>[{'getTime':'20141226 11:24:17'}]</para>
        /// <para>参考：http://www.cnphp6.com/archives/35773 </para>
        /// </summary>
        /// <param name="jsonString">Json字符串</param>
        /// <param name="formart">时间格式化类型</param>
        /// <returns>处理好的Json字符串</returns>
        public static string ParseJsonDateTime(this string jsonString, string formart)
        {
            if (!string.IsNullOrEmpty(jsonString))
            {
                jsonString = Regex.Replace(
                    jsonString,
                    @"\\/Date\((\d+)\)\\/",
                    match =>
                    {
                        DateTime _dateTime = new DateTime(1970, 1, 1);
                        _dateTime = _dateTime.AddMilliseconds(long.Parse(match.Groups[1].Value));
                        _dateTime = _dateTime.ToLocalTime();
                        return _dateTime.ToString(formart);
                    });
            }

            return jsonString;
        }

        /// <summary>
        /// XML字符串反序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="serializerString">XML字符串</param>
        /// <returns>object</returns>
        /// 时间：2015-12-31 9:53
        /// 备注：
        public static object XmlDeserialize(Type type, string serializerString)
        {
            serializerString = serializerString.Trim();
            if (string.IsNullOrEmpty(serializerString))
                return null;
            XmlSerializer _xmlSerializer = new XmlSerializer(type);
            StringReader _writer = new StringReader(serializerString);
            return _xmlSerializer.Deserialize(_writer);
        }

        /// <summary>
        /// 序列化，使用标准的XmlSerializer
        /// 不能序列化IDictionary接口.
        /// </summary>
        /// <param name="entity">对象</param>
        /// <param name="filename">文件路径</param>
        public static void XmlSerialize<T>(T entity, string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                XmlSerializer _xmlSerializer = new XmlSerializer(entity.GetType());
                _xmlSerializer.Serialize(fs, entity);
            }
        }

        /// <summary>
        /// 序列化，使用标准的XmlSerializer
        /// 不能序列化IDictionary接口.
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>字符串</returns>
        /// 时间：2015-12-31 10:14
        /// 备注：
        public static string XmlSerialize(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            XmlSerializer _xmlSerializer = new XmlSerializer(obj.GetType());
            StringWriter _writer = new StringWriter();
            _xmlSerializer.Serialize(_writer, obj);
            return _writer.ToString();
        }

        /// <summary>
        /// 序列化，使用标准的XmlSerializer
        /// 不能序列化IDictionary接口.
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="ecoding">Encoding</param>
        /// <returns>字符串</returns>
        /// 时间：2015-12-31 10:16
        /// 备注：
        public static string XmlSerialize(object obj, Encoding ecoding)
        {
            if (obj == null)
            {
                return null;
            }
            using (MemoryStream stream = new MemoryStream())
            {
                XmlSerializer _xmlSerializer = new XmlSerializer(obj.GetType());

                StreamWriter _writer = new StreamWriter(stream, ecoding);
                XmlSerializerNamespaces _xsn = new XmlSerializerNamespaces();
                _xsn.Add(String.Empty, String.Empty);

                _xmlSerializer.Serialize(_writer, obj, _xsn);

                return ecoding.GetString(stream.ToArray());
            }
        }

        #endregion Methods
    }
}