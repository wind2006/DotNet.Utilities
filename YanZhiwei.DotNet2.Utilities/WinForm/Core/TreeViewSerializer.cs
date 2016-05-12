namespace YanZhiwei.DotNet2.Utilities.WinForm.Core
{
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    /// <summary>
    /// TreeView 序列化与反序列化
    /// </summary>
    public class TreeViewSerializer
    {
        #region Fields

        private const string XmlNodeImageIndexAtt = "imageindex";
        private const string XmlNodeTag = "node";
        private const string XmlNodeTagAtt = "tag";
        private const string XmlNodeTextAtt = "text";

        private string XmlSaveFullPath = string.Empty;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="path">XML序列化或者反序列化路径;eg:string.Format(@"{0}\Config\CtuPtuTreeView.xml", Application.StartupPath)</param>
        public TreeViewSerializer(string path)
        {
            XmlSaveFullPath = path;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// 反序列化TreeView
        /// </summary>
        /// <param name="treeView">TreeView</param>
        public void DeserializeTreeView(TreeView treeView)
        {
            XmlTextReader _reader = null;
            try
            {
                treeView.BeginUpdate();
                _reader = new XmlTextReader(XmlSaveFullPath);

                TreeNode _parentNode = null;

                while (_reader.Read())
                {
                    if (_reader.NodeType == XmlNodeType.Element)
                    {
                        if (_reader.Name == XmlNodeTag)
                        {
                            TreeNode _newNode = new TreeNode();
                            bool _isEmptyElement = _reader.IsEmptyElement;

                            int _attributeCount = _reader.AttributeCount;
                            if (_attributeCount > 0)
                            {
                                for (int i = 0; i < _attributeCount; i++)
                                {
                                    _reader.MoveToAttribute(i);
                                    SetAttributeValue(_newNode, _reader.Name, _reader.Value);
                                }
                            }

                            if (_parentNode != null)
                                _parentNode.Nodes.Add(_newNode);
                            else
                                treeView.Nodes.Add(_newNode);

                            if (!_isEmptyElement)
                            {
                                _parentNode = _newNode;
                            }
                        }
                    }
                    else if (_reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (_reader.Name == XmlNodeTag)
                        {
                            _parentNode = _parentNode.Parent;
                        }
                    }
                    else if (_reader.NodeType == XmlNodeType.XmlDeclaration)
                    {
                    }
                    else if (_reader.NodeType == XmlNodeType.None)
                    {
                        return;
                    }
                    else if (_reader.NodeType == XmlNodeType.Text)
                    {
                        _parentNode.Nodes.Add(_reader.Value);
                    }
                }
            }
            finally
            {
                treeView.EndUpdate();
                _reader.Close();
            }
        }

        /// <summary>
        /// 将XML文件配置节点信息映射到TreeView
        /// </summary>
        /// <param name="treeView">TreeView</param>
        /// <param name="fileName">XML路径</param>
        public void LoadXmlFileInTreeView(TreeView treeView, string fileName)
        {
            XmlTextReader _reader = null;
            try
            {
                treeView.BeginUpdate();
                _reader = new XmlTextReader(fileName);

                TreeNode _n = new TreeNode(fileName);
                treeView.Nodes.Add(_n);
                while (_reader.Read())
                {
                    if (_reader.NodeType == XmlNodeType.Element)
                    {
                        bool _isEmptyElement = _reader.IsEmptyElement;
                        StringBuilder _text = new StringBuilder();
                        _text.Append(_reader.Name);
                        int _attributeCount = _reader.AttributeCount;
                        if (_attributeCount > 0)
                        {
                            _text.Append(" ( ");
                            for (int i = 0; i < _attributeCount; i++)
                            {
                                if (i != 0) _text.Append(", ");
                                _reader.MoveToAttribute(i);
                                _text.Append(_reader.Name);
                                _text.Append(" = ");
                                _text.Append(_reader.Value);
                            }
                            _text.Append(" ) ");
                        }

                        if (_isEmptyElement)
                        {
                            _n.Nodes.Add(_text.ToString());
                        }
                        else
                        {
                            _n = _n.Nodes.Add(_text.ToString());
                        }
                    }
                    else if (_reader.NodeType == XmlNodeType.EndElement)
                    {
                        _n = _n.Parent;
                    }
                    else if (_reader.NodeType == XmlNodeType.XmlDeclaration)
                    {
                    }
                    else if (_reader.NodeType == XmlNodeType.None)
                    {
                        return;
                    }
                    else if (_reader.NodeType == XmlNodeType.Text)
                    {
                        _n.Nodes.Add(_reader.Value);
                    }
                }
            }
            finally
            {
                treeView.EndUpdate();
                _reader.Close();
            }
        }

        /// <summary>
        /// 序列化TreeView
        /// </summary>
        /// <param name="treeView">TreeView</param>
        public void SerializeTreeView(TreeView treeView)
        {
            XmlTextWriter _textWriter = new XmlTextWriter(XmlSaveFullPath, Encoding.UTF8);
            _textWriter.WriteStartDocument();
            _textWriter.WriteStartElement("TreeView");
            SaveNodes(treeView.Nodes, _textWriter);
            _textWriter.WriteEndElement();
            _textWriter.Close();
        }

        /// <summary>
        /// 保存节点
        /// </summary>
        /// <param name="nodesCollection">TreeNodeCollection</param>
        /// <param name="textWriter">XmlTextWriter</param>
        private void SaveNodes(TreeNodeCollection nodesCollection,
            XmlTextWriter textWriter)
        {
            for (int i = 0; i < nodesCollection.Count; i++)
            {
                TreeNode node = nodesCollection[i];
                textWriter.WriteStartElement(XmlNodeTag);
                textWriter.WriteAttributeString(XmlNodeTextAtt, node.Text);
                textWriter.WriteAttributeString(XmlNodeImageIndexAtt, node.ImageIndex.ToString());
                if (node.Tag != null)
                    textWriter.WriteAttributeString(XmlNodeTagAtt, node.Tag.ToString());

                if (node.Nodes.Count > 0)
                {
                    SaveNodes(node.Nodes, textWriter);
                }
                textWriter.Formatting = Formatting.Indented;
                textWriter.WriteEndElement();
            }
        }

        /// <summary>
        /// 反序列化TreeView时候，设置节点属性
        /// </summary>
        /// <param name="node">TreeNode</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="value">属性数值</param>
        private void SetAttributeValue(TreeNode node, string propertyName, string value)
        {
            if (propertyName == XmlNodeTextAtt)
            {
                node.Text = value;
            }
            else if (propertyName == XmlNodeImageIndexAtt)
            {
                node.ImageIndex = int.Parse(value);
            }
            else if (propertyName == XmlNodeTagAtt)
            {
                node.Tag = value;
            }
        }

        #endregion Methods
    }
}