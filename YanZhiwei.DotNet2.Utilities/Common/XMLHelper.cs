namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System;
    using System.Data;
    using System.IO;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// XML操作帮助类
    /// </summary>
    public class XMLHelper
    {
        #region Fields

        /// <summary>
        /// XmlDocument 对象
        /// </summary>
        private XmlDocument xmldoc;

        /// <summary>
        /// xml 路径
        /// </summary>
        private string xmlPath = null;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="version">版本</param>
        /// <param name="encoding">编码</param>
        /// <param name="path">路径</param>
        public XMLHelper(string version, Encoding encoding, string path)
        {
            xmldoc = new XmlDocument();
            xmlPath = path;
            if (!File.Exists(path))
            {
                if (FileHelper.CreatePath(path))
                {
                    XmlDeclaration _xmldecl;
                    _xmldecl = xmldoc.CreateXmlDeclaration(version, encoding.BodyName, null);
                    xmldoc.AppendChild(_xmldecl);
                }
                else
                {
                    throw new ArgumentException(string.Format("创建XML保存路径:{0}失败！", path));
                }
            }
            else
            {
                xmldoc.Load(path);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="version">版本</param>
        /// <param name="encoding">Encoding</param>
        /// <param name="path">XML保存路径</param>
        /// <param name="documentElementName">XML根元素</param>
        public XMLHelper(string version, Encoding encoding, string path, string documentElementName)
        {
            xmldoc = new XmlDocument();
            xmlPath = path;
            if (!File.Exists(path))
            {
                if (FileHelper.CreatePath(path))
                {
                    XmlDeclaration _xmldecl;
                    _xmldecl = xmldoc.CreateXmlDeclaration(version, encoding.BodyName, null);
                    xmldoc.AppendChild(_xmldecl);
                    CreateDocumentElement(documentElementName, string.Format(@"/{0}", documentElementName));
                    Save();
                }
                else
                {
                    throw new ArgumentException(string.Format("创建XML保存路径:{0}失败！", path));
                }
            }
            else
            {
                xmldoc.Load(path);
            }
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// 创建一个子元素
        /// </summary>
        /// <param name="parentElement">父元素</param>
        /// <param name="elementName">子元素名称</param>
        /// <returns>XmlElement</returns>
        public XmlElement CreateChildElement(XmlElement parentElement, string elementName)
        {
            XmlElement _childElement = xmldoc.CreateElement(elementName);
            parentElement.AppendChild(_childElement);
            return _childElement;
        }

        /// <summary>
        /// 创建一个子元素
        /// </summary>
        /// <param name="parentElement">父元素</param>
        /// <param name="elementName">子元素名称</param>
        /// <param name="innerText">文本内容</param>
        /// <returns>XmlElement</returns>
        public XmlElement CreateChildElement(XmlElement parentElement, string elementName, string innerText)
        {
            XmlElement _childElement = xmldoc.CreateElement(elementName);
            _childElement.InnerText = innerText;
            parentElement.AppendChild(_childElement);
            return _childElement;
        }

        /// <summary>
        /// 创建一个子元素
        /// </summary>
        /// <param name="parentElement">父元素</param>
        /// <param name="elementName">子元素名称</param>
        /// <param name="xpath">xpath语法</param>
        /// <param name="setAttributeHanlder">设置委托</param>
        /// <param name="updateAttributeHanlder">修改委托</param>
        /// <returns>XmlElement</returns>
        public XmlElement CreateChildElement(XmlElement parentElement, string elementName, string xpath, Action<XmlElement> setAttributeHanlder, Action<XmlElement> updateAttributeHanlder)
        {
            XmlNode _finded = xmldoc.SelectSingleNode(xpath);
            if (_finded == null)
            {
                XmlElement _childElement = xmldoc.CreateElement(elementName);
                if (setAttributeHanlder != null)
                {
                    setAttributeHanlder(_childElement);
                }

                parentElement.AppendChild(_childElement);
                return _childElement;
            }
            else
            {
                XmlElement _childElement = (XmlElement)_finded;
                if (updateAttributeHanlder != null)
                {
                    updateAttributeHanlder(_childElement);
                }

                return _childElement;
            }
        }

        /// <summary>
        /// 创建XML 文档树的根
        /// </summary>
        /// <param name="elementName">元素名称</param>
        /// <returns>XmlElement</returns>
        public XmlElement CreateDocumentElement(string elementName)
        {
            XmlElement _parentElement = xmldoc.CreateElement(string.Empty, elementName, string.Empty);
            xmldoc.AppendChild(_parentElement);
            return _parentElement;
        }

        /// <summary>
        /// 创建XML文档树的根
        /// </summary>
        /// <param name="elementName">元素名称</param>
        /// <param name="xpath">xpath语法</param>
        /// <returns>XmlElement</returns>
        public XmlElement CreateDocumentElement(string elementName, string xpath)
        {
            XmlElement _parentElement = null;
            XmlNode _findedNode = xmldoc.SelectSingleNode(xpath);
            if (_findedNode != null)
            {
                _parentElement = (XmlElement)_findedNode;
            }
            else
            {
                _parentElement = CreateDocumentElement(elementName);
            }

            return _parentElement;
        }

        /// <summary>
        /// 创建父元素
        /// </summary>
        /// <param name="xmlDocument">XML文档根</param>
        /// <param name="elementName">父元素名称</param>
        /// <param name="xpath">xpath语法</param>
        /// <param name="setAttributeHanlder">委托，当创建XmlElement时候触发。设置NULL的时候不启作用</param>
        /// <param name="updateAttributeHanlder">委托，根据Xpath找到XmlElement时候触发。设置NULL的时候不启作用</param>
        /// <returns>XmlElement</returns>
        public XmlElement CreateParentElement(XmlElement xmlDocument, string elementName, string xpath, Action<XmlElement> setAttributeHanlder, Action<XmlElement> updateAttributeHanlder)
        {
            XmlNode _finded = xmldoc.SelectSingleNode(xpath);
            if (_finded == null)
            {
                XmlElement _parentElement = xmldoc.CreateElement(string.Empty, elementName, string.Empty);
                if (setAttributeHanlder != null)
                {
                    setAttributeHanlder(_parentElement);
                }

                xmldoc.DocumentElement.AppendChild(_parentElement);
                return _parentElement;
            }
            else
            {
                XmlElement _findedElement = (XmlElement)_finded;
                if (updateAttributeHanlder != null)
                {
                    updateAttributeHanlder(_findedElement);
                }

                return _findedElement;
            }
        }

        /// <summary>
        /// 保存XML
        /// </summary>
        public void Save()
        {
            xmldoc.Save(xmlPath);
        }

        /// <summary>
        /// 节点筛选
        /// </summary>
        /// <param name="xpath">xpath语法</param>
        /// <returns>XmlNodeList</returns>
        public XmlNodeList SelectNodes(string xpath)
        {
            return xmldoc.SelectNodes(xpath);
        }

        /// <summary>
        /// 节点筛选
        /// </summary>
        /// <param name="xpath">xpath语法</param>
        /// <param name="updateNodeHanlder">委托</param>
        /// <returns>若找到节点，则返回True，否则返回False;</returns>
        public bool SelectSingleNode(string xpath, Action<XmlElement> updateNodeHanlder)
        {
            XmlElement _findedNode = (XmlElement)xmldoc.SelectSingleNode(xpath);
            if (_findedNode != null)
            {
                if (updateNodeHanlder != null)
                {
                    updateNodeHanlder(_findedNode);
                }
            }

            return _findedNode != null;
        }

        /// <summary>
        /// 节点筛选
        /// </summary>
        /// <param name="xpath">xpath语法</param>
        /// <returns>若找到节点，则返回True，否则返回False;</returns>
        public bool SelectSingleNode(string xpath)
        {
            XmlElement _findedNode = (XmlElement)xmldoc.SelectSingleNode(xpath);
            return _findedNode != null;
        }

        /// <summary>
        /// 将xml内容转换字符串输出
        /// </summary>
        /// <returns>字符串XML内容</returns>
        public string ShowXml()
        {
            if (xmldoc != null)
            {
                return xmldoc.OuterXml;
            }

            return string.Empty;
        }

        /// <summary>
        /// 将XML文件读取返回成DataSet
        /// </summary>
        /// <returns>返回DataSet，若发生异常则返回NULL</returns>
        public DataSet ToDataSet()
        {
            try
            {
                using (XmlNodeReader xmlReader = new XmlNodeReader(xmldoc))
                {
                    DataSet _dataSet = new DataSet();
                    _dataSet.ReadXml(xmlReader);
                    return _dataSet;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion Methods
    }
}