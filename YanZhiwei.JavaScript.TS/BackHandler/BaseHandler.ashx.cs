using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web;
using System.Web.Script.Serialization;
using YanZhiwei.DotNet2.Utilities.Common;
using YanZhiwei.DotNet2.Utilities.Models;
using YanZhiwei.DotNet3._5.Utilities.Common;

namespace YanZhiwei.JavaScript.Learn.BackHandler
{
    /// <summary>
    /// BaseHandler 的摘要说明
    /// </summary>
    public class BaseHandler : IHttpHandler
    {
        private SqlServerHelper SqlHelper = new SqlServerHelper(@"Server=YANZHIWEI-IT-PC\SQLEXPRESS;database=Northwind;uid=sa;pwd=sasa;");

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string _action = context.Request.Params.Get("action");
            HanlderDataTable(context, _action);
            HanlderJsTree(context, _action);
        }

        #region jquery DataTable

        public void HanlderDataTable(HttpContext context, string action)
        {
            if (string.Compare(action, "initDataTable", true) == 0)
            {
                DataTable _productTable = SqlHelper.ExecuteDataTable("select ProductID,ProductName,UnitPrice from dbo.Products", null);
                if (_productTable != null && _productTable.Rows.Count > 0)
                {
          
                    string _jsonString = _productTable.ToJson("data");
                    context.Response.Write(_jsonString);
                }
            }
        }

        #endregion jquery DataTable

        #region jquery JsTree

        public void HanlderJsTree(HttpContext context, string action)
        {
            HanlderFullJsTree(context, action);
            HanlderLazyJsTree(context, action);
        }

        #region 以Lazy方式生成jsTree

        private void HanlderLazyJsTree(HttpContext context, string action)
        {
            int _parentId = context.Request.Params.Get("parentId").ToIntOrDefault(-1);
            if (string.Compare(action, "optJsTree", true) == 0)
            {
                HanlderLazyJsBaseTree(context, _parentId);
                HanlderLazyJsChildNode(context, _parentId);
            }
        }

        private void HanlderLazyJsChildNode(HttpContext context, int parentId)
        {
            if (parentId != -1)
            {
                List<JsChildNode> _childNodeList = HanlderJsTreeChildNode(parentId);
                JavaScriptSerializer _jsonHelper = new JavaScriptSerializer();
                string _json = SerializationHelper.JsonSerialize(_childNodeList);
                context.Response.Write(_json);
            }
        }

        private void HanlderLazyJsBaseTree(HttpContext context, int parentId)
        {
            if (parentId == -1)
            {
                IList<JsTreeNode> _treeNodeList = new List<JsTreeNode>();
                DataTable _categoryTable = SqlHelper.ExecuteDataTable("select CategoryID,CategoryName from dbo.Categories", null);
                if (_categoryTable != null && _categoryTable.Rows.Count > 0)
                {
                    JsTreeNode _root = new JsTreeNode();
                    _root.id = 0;
                    _root.text = "Product List";
                    _root.icon = "../../icon/ie.png";
                    _root.children = new List<JsChildNode>();
                    foreach (DataRow row in _categoryTable.Rows)
                    {
                        int _parnetId = row["CategoryID"].ToIntOrDefault(-1);
                        string _parentName = row["CategoryName"].ToStringOrDefault("");
                        JsChildNode _parentNode = new JsChildNode() { id = _parnetId, text = _parentName, icon = "../../icon/chrome.png", children = true };
                        _root.children.Add(_parentNode);
                    }
                    _treeNodeList.Add(_root);
                    string _json = SerializationHelper.JsonSerialize(_treeNodeList);
                    context.Response.Write(_json);
                }
            }
        }

        #endregion 以Lazy方式生成jsTree

        #region 生成整个jsTree

        private void HanlderFullJsTree(HttpContext context, string action)
        {
            if (string.Compare(action, "initJsTree", true) == 0)
            {
                IList<JsTreeNode> _treeNodeList = new List<JsTreeNode>();
                DataTable _categoryTable = SqlHelper.ExecuteDataTable("select CategoryID,CategoryName from dbo.Categories", null);
                if (_categoryTable != null && _categoryTable.Rows.Count > 0)
                {
                    foreach (DataRow row in _categoryTable.Rows)
                    {
                        int _parnetId = row["CategoryID"].ToIntOrDefault(-1);
                        string _parentName = row["CategoryName"].ToStringOrDefault("");
                        JsTreeNode _parentNode = new JsTreeNode() { id = _parnetId, text = _parentName };
                        _parentNode.children = HanlderJsTreeChildNode(_parentNode.id);
                        _treeNodeList.Add(_parentNode);
                    }
                    string _json = SerializationHelper.JsonSerialize(_treeNodeList);
                    context.Response.Write(_json);
                }
            }
        }

        #endregion 生成整个jsTree

        #region JsTree 根据父节点添加子节点

        private List<JsChildNode> HanlderJsTreeChildNode(int parentID)
        {
            List<JsChildNode> _childNodeList = new List<JsChildNode>();
            using (IDataReader reader = SqlHelper.ExecuteReader("select ProductID,ProductName from dbo.Products where CategoryID=@id", new DbParameter[1] { new SqlParameter("@id", parentID) }))
            {
                while (reader.Read())
                {
                    int _childId = reader["ProductID"].ToIntOrDefault(-1);
                    string _childName = reader["ProductName"].ToStringOrDefault("");
                    JsChildNode _childNode = new JsChildNode() { id = _childId, text = _childName, children = false };
                    _childNodeList.Add(_childNode);
                }
            }
            return _childNodeList;
        }

        #endregion JsTree 根据父节点添加子节点

        #endregion jquery JsTree

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}