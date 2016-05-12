namespace YanZhiwei.DotNet.DevExpress12._1.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    using DevExpress.Utils;
    using DevExpress.Utils.Drawing;
    using DevExpress.XtraBars;
    using DevExpress.XtraEditors.ViewInfo;
    using DevExpress.XtraTreeList;
    using DevExpress.XtraTreeList.Nodes;
    using DevExpress.XtraTreeList.ViewInfo;

    using YanZhiwei.DotNet.DevExpress12._1.Utilities.Core;
    using YanZhiwei.DotNet2.Utilities.Common;

    /// <summary>
    /// Devexpress-TreeList帮助类
    /// </summary>
    public static class TreeListHelper
    {
        #region Methods

        /// <summary>
        /// 为TreeList附加右键菜单
        /// </summary>
        /// <param name="tree">The tree.</param>
        /// <param name="menu">The menu.</param>
        /// <param name="attachMenuHanlder">The attach menu hanlder.</param>
        /// 创建时间:2015-05-26 17:45
        /// 备注说明:<c>null</c>
        public static void AttachMenu(this TreeList tree, PopupMenu menu, Func<TreeListNode, bool> attachMenuHanlder)
        {
            tree.MouseClick += (sender, e) =>
            {
                TreeList _curTree = sender as TreeList;
                if (e.Button == MouseButtons.Right && Control.ModifierKeys == Keys.None && _curTree.State == TreeListState.Regular)
                {
                    Point _point = new Point(Cursor.Position.X, Cursor.Position.Y);
                    TreeListHitInfo _hitInfo = _curTree.CalcHitInfo(e.Location);
                    if (_hitInfo.HitInfoType == HitInfoType.Cell)
                        _curTree.SetFocusedNode(_hitInfo.Node);
                    if (attachMenuHanlder(_curTree.FocusedNode))
                        menu.ShowPopup(_point);
                }
            };
        }

        /// <summary>
        /// 正对节点的检查逻辑
        /// </summary>
        /// <param name="fucusedNode">TreeListNode</param>
        /// <param name="checkNodeHanlder">检查逻辑代码[委托]</param>
        /// <returns>TreeListNode</returns>
        public static TreeListNode Check(this TreeListNode fucusedNode, Func<TreeListNode, bool> checkNodeHanlder)
        {
            if (fucusedNode != null)
                return checkNodeHanlder(fucusedNode) == true ? fucusedNode : null;
            return null;
        }

        /// <summary>
        /// 节点为null检查
        /// </summary>
        /// <param name="fucusedNode">需要判断的节点</param>
        /// <param name="checkNodeHanlder">若为NULL,处理逻辑</param>
        /// <returns>TreeListNode</returns>
        public static TreeListNode CheckNull(this TreeListNode fucusedNode, Func<bool> checkNodeHanlder)
        {
            if (fucusedNode == null)
            {
                checkNodeHanlder();
                return null;
            }
            return fucusedNode;
        }

        /// <summary>
        /// 节点为null检查
        /// </summary>
        /// <param name="fucusedNode">需要判断的节点</param>
        /// <param name="checkNodeHanlder">委托</param>
        /// <returns>TreeListNode</returns>
        public static TreeListNode CheckNull(this TreeListNode fucusedNode, Action checkNodeHanlder)
        {
            if (fucusedNode == null)
            {
                checkNodeHanlder();
                return null;
            }
            return fucusedNode;
        }

        /// <summary>
        /// TreeList的克隆
        /// 说明：务必设置KeyFieldName属性
        /// 当sourceTree的focusedNode等于NULL的时候，无法同步展开节点以及选中节点状态
        /// </summary>
        /// <param name="sourceTree">需要克隆的TREE</param>
        /// <param name="targetTree">克隆到的TREE</param>
        public static void CloneTo(this TreeList sourceTree, TreeList targetTree)
        {
            string _pathDB = FileHelper.ChangeFileType("xml");
            sourceTree.ExportToXml(_pathDB);
            if (File.Exists(_pathDB))
            {
                TreeListViewState _treeState = new TreeListViewState(sourceTree);
                _treeState.SaveState();
                targetTree.ImportFromXml(_pathDB);
                _treeState.LoadState(targetTree);
            }
        }

        /// <summary>
        /// TreeList的克隆
        /// 说明：务必设置KeyFieldName属性
        /// 当sourceTree的focusedNode等于NULL的时候，无法同步展开节点以及选中节点状态
        /// </summary>
        /// <param name="sourceTree">The source tree.</param>
        /// <param name="focusedNode">The focused node.</param>
        /// <param name="targetTree">The target tree.</param>
        /// 创建时间:2015-05-26 17:47
        /// 备注说明:<c>null</c>
        public static void CloneTo(this TreeList sourceTree, TreeListNode focusedNode, TreeList targetTree)
        {
            string _pathDB = FileHelper.ChangeFileType("xml");
            sourceTree.ExportToXml(_pathDB);
            if (File.Exists(_pathDB))
            {
                TreeListViewState _treeState = new TreeListViewState(sourceTree, focusedNode);
                _treeState.SaveState();
                targetTree.ImportFromXml(_pathDB);
                _treeState.LoadState(targetTree);
            }
        }

        /// <summary>
        /// TreeList的克隆
        /// 说明：务必设置KeyFieldName属性
        /// 当sourceTree的focusedNode等于NULL的时候，无法同步展开节点以及选中节点状态
        /// </summary>
        /// <param name="sourceTree">需要克隆的TREE</param>
        /// <param name="autoFocusedNode"> 当TreeList没有选中的节点的时候，并且autoFocusedNode==True的时候，会默认找到一个节点并且选中
        ///                                当TreeList有选中节点的时候，并且autoFocusedNode==True的时候，继续使用TreeList有选中节点
        ///</param>
        /// <param name="targetTree">克隆到的TREE</param>
        public static void CloneTo(this TreeList sourceTree, bool autoFocusedNode, TreeList targetTree)
        {
            string _pathDB = FileHelper.ChangeFileType("xml");
            sourceTree.ExportToXml(_pathDB);
            if (File.Exists(_pathDB))
            {
                TreeListViewState _treeState = new TreeListViewState(sourceTree, autoFocusedNode);
                _treeState.SaveState();
                targetTree.ImportFromXml(_pathDB);
                _treeState.LoadState(targetTree);
            }
        }

        /// <summary>
        /// 复制节点
        /// </summary>
        /// <param name="node">需要复制的节点</param>
        /// <returns>数组</returns>
        public static object[] CopyNode(this TreeListNode node)
        {
            if (node != null)
            {
                object[] _values = new object[node.TreeList.Columns.Count];
                for (int i = 0; i < node.TreeList.Columns.Count; i++)
                    _values[i] = node.GetValue(i);
                return _values;
            }
            return null;
        }

        /// <summary>
        /// 设置图片节点的背景色
        /// 说明：在CustomDrawNodeImages事件中使用
        /// </summary>
        /// <param name="tree">TreeList</param>
        /// <param name="e">CustomDrawNodeImagesEventArgs</param>
        /// <param name="builderBackColorHandler">委托</param>
        public static void CustomImageNodeBackColor(this TreeList tree, CustomDrawNodeImagesEventArgs e, Func<TreeListNode, Color> builderBackColorHandler)
        {
            TreeListNode _node = e.Node;
            Color _backColor = builderBackColorHandler(_node);
            e.Graphics.FillRectangle(new SolidBrush(_backColor), e.Bounds);
        }

        /// <summary>
        /// 设置节点背景色
        /// 说明：在NodeCellStyle事件中使用
        /// </summary>
        /// <param name="tree">TreeList</param>
        /// <param name="e">GetCustomNodeCellStyleEventArgs</param>
        /// <param name="builderBackColorHandler">委托</param>
        public static void CustomNodeBackColor(this TreeList tree, GetCustomNodeCellStyleEventArgs e, Func<TreeListNode, Color> builderBackColorHandler)
        {
            TreeListNode _node = e.Node;
            e.Appearance.BackColor = builderBackColorHandler(_node);
        }

        /// <summary>
        /// 为节点提供Tooltip
        /// 说明：
        /// 1.设置tree.ToolTipController属性
        /// 2.ToolTipController的GetActiveObjectInfo事件中使用
        /// 3.举例
        /// tlLHData.CustomNodeTooltip(e, node =>
        ///{
        ///    string _cabId = node.GetKeyID();
        ///    CCabInfo _cabinfo = LHDBHelper.GetCabInfo(_cabId);
        ///    if (_cabinfo != null)
        ///    {
        ///        return string.Format("核对时间:{0}\r\n在线情况:{1}\r\n最后一次活跃时间:{2}\r\n",
        ///                              _cabinfo.ChkDataTime,
        ///                              _cabinfo.CtuOnlineStatus == 1 ? "在线" : "未上线",
        ///                              _cabinfo.LastAliveTime);
        ///    }
        ///    return string.Empty;
        ///});
        /// </summary>
        /// <param name="tree">TreeList</param>
        /// <param name="e">ToolTipControllerGetActiveObjectInfoEventArgs</param>
        /// <param name="builderNodeTooltipHandler">委托</param>
        public static void CustomNodeTooltip(this TreeList tree, ToolTipControllerGetActiveObjectInfoEventArgs e, Func<TreeListNode, string> builderNodeTooltipHandler)
        {
            if (e.SelectedControl is DevExpress.XtraTreeList.TreeList)
            {
                TreeList _tree = (TreeList)e.SelectedControl;
                TreeListHitInfo _hit = _tree.CalcHitInfo(e.ControlMousePosition);
                if (_hit.HitInfoType == HitInfoType.Cell)
                {
                    TreeListViewInfo _viewInfo = _tree.ViewInfo;
                    RowInfo _rowInfo = _viewInfo.GetRowInfoByPoint(e.ControlMousePosition);
                    CellInfo _cellInfo = _rowInfo.Cells[_hit.Column.VisibleIndex] as CellInfo;
                    EditHitInfo _editHitInfo = _cellInfo.EditorViewInfo.CalcHitInfo(e.ControlMousePosition);
                    if (_editHitInfo.HitTest == EditHitTest.MaskBox)
                    {
                        string _toolTip = builderNodeTooltipHandler(_hit.Node);
                        if (!string.IsNullOrEmpty(_toolTip))
                            e.Info = new ToolTipControlInfo(_cellInfo, _toolTip);
                    }
                }
            }
        }

        /// <summary>
        /// 禁用CheckBox
        /// 说明
        /// 在CustomDrawNodeCheckBox事件中使用
        /// </summary>
        /// <param name="tree">TreeList</param>
        /// <param name="conditionHanlder">委托</param>
        /// <param name="e">CustomDrawNodeCheckBoxEventArgs</param>
        public static void DisabledCheckBox(this TreeListNode tree, Predicate<TreeListNode> conditionHanlder, CustomDrawNodeCheckBoxEventArgs e)
        {
            if (conditionHanlder(e.Node))
            {
                e.ObjectArgs.State = ObjectState.Disabled;
            }
        }

        /// <summary>
        /// 禁止操作节点CheckBox
        /// 说明
        /// 在BeforeCheckNode事件中使用
        /// </summary>
        /// <param name="tree">TreeListNode</param>
        /// <param name="conditionHanlder">委托</param>
        /// <param name="e">CheckNodeEventArgs</param>
        public static void DisabledSetCheckBox(this TreeListNode tree, Predicate<TreeListNode> conditionHanlder, CheckNodeEventArgs e)
        {
            if (conditionHanlder(e.Node))
            {
                e.CanCheck = false;
            }
        }

        /// <summary>
        /// 向下递归TreeListNode节点
        /// </summary>
        /// <param name="node">需要向下递归的节点</param>
        /// <param name="conditionHanlder">委托</param>
        public static void DownRecursiveNode(this TreeListNode node, Action<TreeListNode> conditionHanlder)
        {
            foreach (TreeListNode _childNode in node.Nodes)
            {
                conditionHanlder(_childNode);
                DownRecursiveNode(_childNode, conditionHanlder);
            }
        }

        /// <summary>
        /// 向下递归TreeListNode,当opreateRule返回false停止循环
        /// </summary>
        /// <param name="node">需要向下递归的节点</param>
        /// <param name="conditionHanlder">委托</param>
        public static void DownRecursiveNode_Break(this TreeListNode node, Func<TreeListNode, bool> conditionHanlder)
        {
            foreach (TreeListNode _childNode in node.Nodes)
            {
                if (!conditionHanlder(_childNode))
                    break;
                DownRecursiveNode_Break(_childNode, conditionHanlder);
            }
        }

        /// <summary>
        /// 向下递归遍历TreeListNode,当opreateRule返回false跳出循环，直接进入下次循环
        /// </summary>
        /// <param name="node">需要向下递归的节点</param>
        /// <param name="conditionHanlder">委托</param>
        public static void DownRecursiveNode_Continue(this TreeListNode node, Func<TreeListNode, bool> conditionHanlder)
        {
            foreach (TreeListNode _childNode in node.Nodes)
            {
                if (!conditionHanlder(_childNode))
                    continue;
                DownRecursiveNode_Continue(_childNode, conditionHanlder);
            }
        }

        /// <summary>
        /// 向下递归遍历树节点
        /// </summary>
        /// <param name="tree">需要向下递归的TREE</param>
        /// <param name="conditionHanlder">委托</param>
        public static void DownRecursiveTree(this TreeList tree, Action<TreeListNode> conditionHanlder)
        {
            foreach (TreeListNode node in tree.Nodes)
            {
                conditionHanlder(node);
                if (node.Nodes.Count > 0)
                {
                    DownRecursiveNode(node, conditionHanlder);
                }
            }
        }

        /// <summary>
        ///数据筛选
        ///eg:
        ///FilterCondition _curFc = new FilterCondition(FilterConditionEnum.NotContains, treeList1.Columns["Name"], _curPerson.Name);
        ///treeList1.Filter(_curFc);
        /// </summary>
        /// <param name="tree">TreeList</param>
        /// <param name="fc">FilterCondition</param>
        public static void Filter(this TreeList tree, FilterCondition fc)
        {
            if (tree != null && fc != null)
            {
                if (!tree.OptionsBehavior.EnableFiltering)
                    tree.OptionsBehavior.EnableFiltering = true;
                tree.FilterConditions.Clear();
                tree.FilterConditions.Add(fc);
            }
        }

        /// <summary>
        /// 获取筛选节点到根节点的所有信息
        /// </summary>
        /// <param name="focusedNode">TreeListNode</param>
        /// <param name="columnID">列名称</param>
        /// <param name="compareNodeRule">规则委托</param>
        /// <param name="buildPathRule">规则委托</param>
        /// <returns>路径信息</returns>
        public static string FilterPathInfo(this TreeListNode focusedNode, string columnID, Func<TreeListNode, bool> compareNodeRule, Func<string, string, string> buildPathRule)
        {
            string _fullPathInfo = string.Empty;
            _fullPathInfo = focusedNode.GetDisplayText(columnID);
            while (focusedNode.ParentNode != null)
            {
                focusedNode = focusedNode.ParentNode;
                if (compareNodeRule(focusedNode))
                {
                    string _nodeText = focusedNode.GetDisplayText(columnID).Trim();
                    _fullPathInfo = buildPathRule(_nodeText, _fullPathInfo);
                }
            }
            return _fullPathInfo;
        }

        /// <summary>
        /// 获取选中节点到根节点的所有信息
        /// </summary>
        /// <param name="focusedNode">TreeListNode</param>
        /// <param name="columnID">列名称</param>
        /// <param name="buildPathRule">规则委托</param>
        /// <returns>路径信息</returns>
        public static string FullPathInfo(this TreeListNode focusedNode, string columnID, Func<string, string, string> buildPathRule)
        {
            string _fullPathInfo = string.Empty;
            _fullPathInfo = focusedNode.GetDisplayText(columnID);
            while (focusedNode.ParentNode != null)
            {
                focusedNode = focusedNode.ParentNode;
                string _nodeText = focusedNode.GetDisplayText(columnID).Trim();
                _fullPathInfo = buildPathRule(_nodeText, _fullPathInfo);
            }
            return _fullPathInfo;
        }

        /// <summary>
        /// 向下递归根据CheckState获取节点
        /// <para>eg:tvCabTree.LHTree.GetDownRecursiveNodeListByCheckState(n => n.GetNodeType() == NodeType.Cab, new CheckState[2] { CheckState.Checked, CheckState.Indeterminate });</para>
        /// </summary>
        /// <param name="tree">TreeList</param>
        /// <param name="getRule">委托</param>
        /// <param name="checkstate">CheckState</param>
        /// <returns>可能返回NULL;</returns>
        public static List<TreeListNode> GetDownRecursiveNodeListByCheckState(this TreeList tree, Predicate<TreeListNode> getRule, CheckState[] checkstate)
        {
            if (tree != null)
            {
                TreeListNode _rootNode = tree.Nodes[0];
                if (_rootNode != null)
                {
                    List<TreeListNode> _checkedNodeList = new List<TreeListNode>();
                    _rootNode.DownRecursiveNode(n =>
                    {
                        if (getRule(n))
                        {
                            if (checkstate.Contains<CheckState>(n.CheckState))
                                _checkedNodeList.Add(n);
                        }
                    });
                    return _checkedNodeList;
                }
            }
            return null;
        }

        /// <summary>
        /// 向下递归根据条件获取节点集合
        /// </summary>
        /// <param name="tree">TreeList</param>
        /// <param name="firstConditionHanlder">第一次条件委托</param>
        /// <param name="secondConditionHanlder">第二次条件委托</param>
        /// <returns>节点集合</returns>
        public static List<TreeListNode> GetNodesBy(this TreeList tree, Predicate<TreeListNode> firstConditionHanlder, Predicate<TreeListNode> secondConditionHanlder)
        {
            List<TreeListNode> _checkNodes = new List<TreeListNode>();
            tree.DownRecursiveTree((TreeListNode node) =>
            {
                if (firstConditionHanlder(node))
                {
                    if (secondConditionHanlder(node))
                        _checkNodes.Add(node);
                }
            });
            return _checkNodes;
        }

        /// <summary>
        /// 根据CheckState获取TreeListNode
        /// </summary>
        /// <param name="tree">TreeList</param>
        /// <param name="state">CheckState</param>
        /// <param name="GetNodesByStateRule">返回True的时候继续</param>
        /// <returns>TreeListNode集合</returns>
        public static List<TreeListNode> GetNodesByState(this TreeList tree, CheckState state, Func<TreeListNode, bool> GetNodesByStateRule)
        {
            List<TreeListNode> _checkNodes = new List<TreeListNode>();
            tree.DownRecursiveTree((TreeListNode node) =>
            {
                if (GetNodesByStateRule(node))
                {
                    if (node.CheckState == state)
                        _checkNodes.Add(node);
                }
            });
            return _checkNodes;
        }

        /// <summary>
        /// 向上递归，获取符合条件的父节点
        /// </summary>
        /// <param name="node">需要向上递归的节点</param>
        /// <param name="conditionHanlder">判断条件【委托】</param>
        /// <returns>符合条件的节点【TreeListNode】</returns>
        public static TreeListNode GetParentNode(this TreeListNode node, Predicate<TreeListNode> conditionHanlder)
        {
            TreeListNode _parentNode = node.ParentNode;//获取上一级父节点
            TreeListNode _conditonNode = null;
            if (_parentNode != null)
            {
                if (conditionHanlder(_parentNode))//判断上一级父节点是否符合要求
                {
                    _conditonNode = _parentNode;
                }
                if (_conditonNode == null)//若没有找到符合要求的节点，递归继续
                    _conditonNode = GetParentNode(_parentNode, conditionHanlder);
            }
            return _conditonNode;
        }

        /// <summary>
        /// 向上递归，获取符合条件的节点的公共父节点
        /// </summary>
        /// <param name="node">操作节点</param>
        /// <param name="checkHanlder">委托</param>
        /// <returns>符合条件的节点</returns>
        public static TreeListNode GetPublicParentNode(this TreeListNode node, Predicate<TreeListNode> checkHanlder)
        {
            TreeListNode _publicPNode = null;
            TreeListNode _findNode = node.GetParentNode(checkHanlder);//先获取到条件判断的自身父节点
            if (_findNode != null)
            {
                //开始向上递归
                UpwardRecursiveNode(_findNode, n =>
                {
                    TreeListNode _curpublicNode = n.ParentNode;//获取当前向上递归的父节点
                    if (_curpublicNode != null)
                    {
                        if (_curpublicNode.Nodes.Count > 1)//若有多个子节点，则是公共父节点
                        {
                            _publicPNode = _curpublicNode;
                            return false;//跳出递归
                        }
                    }
                    return true;//继续递归
                });
            }
            return _publicPNode;
        }

        /// <summary>
        /// 获取节点下可视区域子节点集合
        /// </summary>
        /// <param name="node">需要获取可见子节点的节点</param>
        /// <param name="conditonHanlder">条件委托</param>
        /// <returns>可见子节点集合</returns>
        public static List<TreeListNode> GetVisibleChildNodes(this TreeListNode node, Predicate<TreeListNode> conditonHanlder)
        {
            List<TreeListNode> _visibleChildNodes = new List<TreeListNode>();
            TreeList _tree = node.TreeList;
            DownRecursiveNode(node, n =>
            {
                RowInfo _rowInfo = _tree.ViewInfo.RowsInfo[n];
                if (_rowInfo != null)
                {
                    if (conditonHanlder(n))
                    {
                        _visibleChildNodes.Add(n);
                    }
                }
            });
            return _visibleChildNodes;
        }

        /// <summary>
        /// 获取节点下可视区域子节点集合
        /// </summary>
        /// <param name="node">需要获取可见子节点的节点</param>
        /// <returns>可见子节点集合</returns>
        public static List<TreeListNode> GetVisibleChildNodes(this TreeListNode node)
        {
            return GetVisibleChildNodes(node, n => 1 == 1);
        }

        /// <summary>
        /// 获取可视区域节点
        /// </summary>
        /// <param name="treeList">TreeList</param>
        /// <param name="conditonHanlder">条件委托</param>
        /// <returns>可视区域节点集合</returns>
        public static List<TreeListNode> GetVisibleNodes(this TreeList treeList, Predicate<TreeListNode> conditonHanlder)
        {
            List<TreeListNode> _visibleNodes = new List<TreeListNode>();
            RowsInfo _rowsInfo = treeList.ViewInfo.RowsInfo;
            foreach (RowInfo ri in _rowsInfo.Rows)
            {
                TreeListNode _curNode = ri.Node;
                if (conditonHanlder(_curNode))
                {
                    _visibleNodes.Add(_curNode);
                }
            }
            return _visibleNodes;
        }

        /// <summary>
        ///  获取可视区域节点
        /// </summary>
        /// <param name="treeList">TreeList</param>
        /// <returns>可视区域节点集合</returns>
        public static List<TreeListNode> GetVisibleNodes(this TreeList treeList)
        {
            return GetVisibleNodes(treeList, n => 1 == 1);
        }

        /// <summary>
        /// 隐藏CheckBox
        /// 说明
        /// 在CustomDrawNodeCheckBox事件中使用
        /// eg:
        /// TreeList _curTree = (TreeList)sender;
        /// _curTree.HideCheckBox(n => n.GetNodeType() == NodeType.Area || n.GetNodeType() == NodeType.CabsGroupRoot, e);
        /// </summary>
        /// <param name="tree">TreeList</param>
        /// <param name="conditionHanlder">委托</param>
        /// <param name="e">CustomDrawNodeCheckBoxEventArgs</param>
        public static void HideCheckBox(this TreeListNode tree, Predicate<TreeListNode> conditionHanlder, CustomDrawNodeCheckBoxEventArgs e)
        {
            if (conditionHanlder(e.Node))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 水平滚动条
        /// </summary>
        /// <param name="tree">TreeList</param>
        public static void HorzScroll(this TreeList tree)
        {
            tree.OptionsView.AutoWidth = false;
            tree.BestFitColumns();
            tree.HorzScrollVisibility = ScrollVisibility.Always;
        }

        /// <summary>
        /// 递归遍历节点
        /// </summary>
        /// <param name="tree">TreeList</param>
        /// <param name="opreateRule">委托</param>
        public static void LoopTreeNodes(this TreeList tree, Action<TreeListNode> opreateRule)
        {
            foreach (TreeListNode node in tree.Nodes)
            {
                opreateRule(node);
                if (node.Nodes.Count > 0)
                {
                    LoopTreeNodes(node, opreateRule);
                }
            }
        }

        /// <summary>
        /// 递归按需选中节点
        /// </summary>
        /// <param name="tree">TreeList</param>
        /// <param name="focusedNodeHanlder">委托</param>
        public static void SetFocusedNode(this TreeList tree, Predicate<TreeListNode> focusedNodeHanlder)
        {
            if (tree != null && tree.Nodes.Count > 0)
            {
                TreeListNode _root = tree.Nodes[0];
                _root.DownRecursiveNode_Break(n => !focusedNodeHanlder(n));
            }
        }

        /// <summary>
        /// 设置FocusedNode的背景色
        /// 说明：
        /// 在CustomDrawNodeCell事件中使用
        /// 示例：
        /// _curTree.SetFocusedNodeBackColor(Color.Green, Color.PeachPuff, Color.Black, e);
        /// </summary>
        /// <param name="tree">TreeList</param>
        /// <param name="backColor1">起始背景颜色</param>
        /// <param name="backColor2">结束背景颜色</param>
        /// <param name="foreBrush">字体颜色</param>
        /// <param name="e">CustomDrawNodeCellEventArgs</param>
        public static void SetFocusedNodeBackColor(this TreeList tree, Color backColor1, Color backColor2, Color foreBrush, CustomDrawNodeCellEventArgs e)
        {
            if (e.Node == tree.FocusedNode)
            {
                Brush _backBrush, _foreBrush;
                _backBrush = new LinearGradientBrush(e.Bounds, backColor1, backColor2, LinearGradientMode.Horizontal);
                _foreBrush = new SolidBrush(foreBrush);
                e.Graphics.FillRectangle(_backBrush, e.Bounds);
                e.Graphics.DrawString(e.CellText, e.Appearance.Font, _foreBrush, e.Bounds, e.Appearance.GetStringFormat());
                e.Handled = true;
            }
        }

        /// <summary>
        /// 节点互斥同步
        /// 说明
        /// eg:
        ///TreeListNode _node = e.Node;
        ///_node.SyncMutexNodeCheckState(_node.CheckState, n => n.GetNodeType() == NodeType.Cab);
        /// </summary>
        /// <param name="node">需要互斥同步的节点</param>
        /// <param name="checkState">节点状态</param>
        /// <param name="checkHanlder">互斥条件【委托】</param>
        public static void SyncMutexNodeCheckState(this TreeListNode node, CheckState checkState, Predicate<TreeListNode> checkHanlder)
        {
            TreeList _tree = node.TreeList;
            if (checkHanlder(node))//当前节点符合互斥条件时候
            {
                _tree.DownRecursiveTree(n => n.CheckState = CheckState.Unchecked);
            }
            else
            {
                TreeListNode _curParentNode = node.GetParentNode(checkHanlder);//获取符合互斥条件的父节点
                if (_curParentNode == null) return;
                TreeListNode _thePubleNode = node.GetPublicParentNode(checkHanlder);//获取符合互斥条件的公共父节点
                if (_thePubleNode == null) return;
                foreach (TreeListNode n in _thePubleNode.Nodes)
                {
                    foreach (TreeListNode nc in n.Nodes)
                    {
                        if (nc != _curParentNode)
                        {
                            nc.CheckState = CheckState.Unchecked;
                            nc.DownRecursiveNode(nr => nr.CheckState = CheckState.Unchecked);
                        }
                    }
                }
            }
            node.SyncNodeCheckState(checkState);
            node.CheckState = checkState;
        }

        /// <summary>
        ///同步父子节点勾选状态
        ///说明
        ///在AfterCheckNode事件中使用代码
        ///eg:e.Node.SyncNodeCheckState(e.Node.CheckState);
        /// </summary>
        /// <param name="node">需要同步的节点</param>
        /// <param name="check">节点当前勾选状态</param>
        public static void SyncNodeCheckState(this TreeListNode node, CheckState check)
        {
            SyncNodeCheckState_Child(node, check);
            SyncNodeCheckState_Parent(node, check);
        }

        /// <summary>
        /// 向上递归节点
        /// </summary>
        /// <param name="node">需要向上递归的节点</param>
        /// <param name="conditionHanlder">委托，返回fasle跳出递归；返回true继续递归；</param>
        public static void UpwardRecursiveNode(this TreeListNode node, Predicate<TreeListNode> conditionHanlder)
        {
            TreeListNode _parentNode = node.ParentNode;
            if (_parentNode != null)
            {
                if (conditionHanlder(_parentNode))
                {
                    UpwardRecursiveNode(_parentNode, conditionHanlder);
                }
            }
        }

        private static void LoopTreeNodes(TreeListNode node, Action<TreeListNode> opreateRule)
        {
            foreach (TreeListNode _childNode in node.Nodes)
            {
                opreateRule(_childNode);
                LoopTreeNodes(_childNode, opreateRule);
            }
        }

        private static void SyncNodeCheckState_Child(TreeListNode node, CheckState check)
        {
            if (node != null)
            {
                node.DownRecursiveNode(n => n.CheckState = check);
            }
        }

        private static void SyncNodeCheckState_Parent(TreeListNode node, CheckState check)
        {
            if (node.ParentNode != null)
            {
                bool _cked = false;
                CheckState _ckState;
                foreach (TreeListNode cn in node.ParentNode.Nodes)
                {
                    _ckState = cn.CheckState;
                    if (check != _ckState)
                    {
                        _cked = !_cked;
                        break;
                    }
                }
                node.ParentNode.CheckState = _cked ? CheckState.Indeterminate : check;
                SyncNodeCheckState_Parent(node.ParentNode, check);
            }
        }

        #endregion Methods
    }
}