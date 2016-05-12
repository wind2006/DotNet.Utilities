namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    using YanZhiwei.DotNet2.Utilities.Core;

    /// <summary>
    /// 序列化帮助类
    /// </summary>
    /// 创建时间:2015-05-22 15:07
    /// 备注说明:<c>null</c>
    public static class SerializeHelper
    {
        #region Methods

        /// <summary>
        /// 将binay文件反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        /// 创建时间:2015-05-22 15:10
        /// 备注说明:<c>null</c>
        public static List<T> ParseBinaryFile<T>(string path)
        {
            IFormatter _serFormatter = new BinaryFormatter();
            _serFormatter.Binder = new UBinder();
            using (Stream _stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                return (List<T>)_serFormatter.Deserialize(_stream);
            }
        }

        /// <summary>
        /// 将Binary文件反序列化成datatable
        /// </summary>
        /// <param name="path">Binary文件路径</param>
        /// <returns>DataTable</returns>
        public static DataTable ParseBinaryFileToTable(string path)
        {
            IFormatter _serFormatter = new BinaryFormatter();
            _serFormatter.Binder = new UBinder();
            using (Stream _stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                return (DataTable)_serFormatter.Deserialize(_stream);
            }
        }

        /// <summary>
        /// 将对象序列化成字符串
        /// </summary>
        /// <param name="model">object</param>
        /// <returns>string</returns>
        public static string ParseModel<T>(this T model)
            where T : class
        {
            if (null == model)
                return string.Empty;
            Type _type = model.GetType();
            FieldInfo[] _fields = _type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            StringBuilder _objString = new StringBuilder();
            foreach (FieldInfo field in _fields)
            {
                object _value = field.GetValue(model);
                _objString.Append(field.Name + ":" + _value + ";");
            }
            return _objString.ToString();
        }

        /// <summary>
        /// 将XML文件反序列化成集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        /// 创建时间:2015-05-22 15:09
        /// 备注说明:<c>null</c>
        public static List<T> ParseXMLFile<T>(string path)
        {
            using (Stream _stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                XmlSerializer _xmlSerializer = new XmlSerializer(typeof(List<T>));
                return (List<T>)_xmlSerializer.Deserialize(_stream);
            }
        }

        /// <summary>
        /// 将xml文件转换string类型
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>string</returns>
        public static string ParseXMLFile(string path)
        {
            XmlDocument _xmlDoc = new XmlDocument();
            _xmlDoc.Load(path);
            StringBuilder _xmlBuilder = new StringBuilder();
            StringWriter _xmlSw = new StringWriter(_xmlBuilder);
            XmlTextWriter _xmlWriter = new XmlTextWriter(_xmlSw);
            _xmlWriter.Formatting = Formatting.Indented;
            _xmlDoc.WriteContentTo(_xmlWriter);
            return _xmlBuilder.ToString();
        }

        /// <summary>
        /// 将实体类集合序列化成Binary文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <param name="path">The save path.</param>
        /// 创建时间:2015-05-22 15:14
        /// 备注说明:<c>null</c>
        public static void ToBinaryFile<T>(this List<T> data, string path)
            where T : class
        {
            IFormatter _serFormatter = new BinaryFormatter();
            using (Stream _stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                _serFormatter.Serialize(_stream, data);
            }
        }

        /// <summary>
        /// 将datatable序列化Binary文件
        /// </summary>
        /// <param name="datatable">The datatable.</param>
        /// <param name="path">The save path.</param>
        /// 创建时间:2015-05-22 15:14
        /// 备注说明:<c>null</c>
        public static void ToBinaryFile(this DataTable datatable, string path)
        {
            IFormatter _serFormatter = new BinaryFormatter();
            using (Stream _stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                _serFormatter.Serialize(_stream, datatable);
            }
        }

        /// <summary>
        /// 将集合序列化成xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <param name="savePath">The save path.</param>
        /// 创建时间:2015-05-22 15:08
        /// 备注说明:<c>null</c>
        public static void ToXMLFile<T>(this List<T> data, string savePath)
        {
            using (Stream _stream = new FileStream(savePath, FileMode.Create, FileAccess.Write))
            {
                XmlTextWriter _xmlTextWriter = new XmlTextWriter(_stream, new UTF8Encoding(false));
                _xmlTextWriter.Formatting = Formatting.Indented;
                XmlSerializer _xmlSerializer = new XmlSerializer(data.GetType());
                _xmlSerializer.Serialize(_xmlTextWriter, data);
                _xmlTextWriter.Flush();
                _xmlTextWriter.Close();
            }
        }

        /// <summary>
        ///  将实体类序列化成xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">The save path.</param>
        /// <param name="model">The model.</param>
        /// 创建时间:2015-05-22 15:12
        /// 备注说明:<c>null</c>
        public static void ToXMLFile<T>(this T model, string path)
            where T : class
        {
            using (Stream _stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                XmlTextWriter _xmlTextWriter = new XmlTextWriter(_stream, new UTF8Encoding(false));
                _xmlTextWriter.Formatting = Formatting.Indented;
                XmlSerializer _xmlSerializer = new XmlSerializer(model.GetType());
                _xmlSerializer.Serialize(_xmlTextWriter, model);
                _xmlTextWriter.Flush();
                _xmlTextWriter.Close();
            }
        }

        

        #endregion Methods
    }
}