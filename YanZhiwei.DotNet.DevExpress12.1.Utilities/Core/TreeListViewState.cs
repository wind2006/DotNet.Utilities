using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Nodes.Operations;
using System.Collections;

namespace YanZhiwei.DotNet.DevExpress12._1.Utilities.Core
{
    /// <summary>
    /// TreeList的展开状态，选中节点同步帮助类
    /// 说明：务必设置KeyFieldName属性
    /// 当TreeList的focusedNode等于NULL的时候，无法同步展开节点以及选中节点状态
    /// 参考：https://www.devexpress.com/Support/Center/Example/Details/E864
    /// </summary>
    public class TreeListViewState
    {
        private ArrayList expanded;
        private ArrayList selected;
        private object focused;
        private int topIndex;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TreeListViewState()
            : this(null)
        {
        }

        /// <summary>
        /// 构造函数带参数
        /// </summary>
        /// <param name="tree">TreeList</param>
        /// <param name="focusedNode">需要选中的节点</param>
        public TreeListViewState(TreeList tree, TreeListNode focusedNode)
        {
            this.treeList = tree;
            this.treeList.FocusedNode = focusedNode;
            expanded = new ArrayList();
            selected = new ArrayList();
        }

        /// <summary>
        /// 构造函数带参数
        /// </summary>
        /// <param name="tree"></param>
        public TreeListViewState(TreeList tree)
        {
            this.treeList = tree;
            expanded = new ArrayList();
            selected = new ArrayList();
        }

        /// <summary>
        ///带参数的构造函数，
        ///当TreeList没有选中的节点的时候，并且autoFocusedNode==True的时候，会默认找到一个节点并且选中
        ///当TreeList有选中节点的时候，并且autoFocusedNode==True的时候，继续使用TreeList有选中节点
        /// </summary>
        /// <param name="tree">TreeList</param>
        /// <param name="autoFocusedNode">是否自动选中节点</param>
        public TreeListViewState(TreeList tree, bool autoFocusedNode)
        {
            this.treeList = tree;
            expanded = new ArrayList();
            selected = new ArrayList();
            if (autoFocusedNode && this.treeList.FocusedNode == null)
            {
                if (this.treeList.Nodes.Count > 0)
                {
                    this.treeList.FocusedNode = treeList.Nodes[0];
                }
            }
        }

        /// <summary>
        /// 清除TREELIST的展开，选中节点的保存信息
        /// </summary>
        public void Clear()
        {
            expanded.Clear();
            selected.Clear();
            focused = null;
            topIndex = 0;
        }

        private ArrayList GetExpanded()
        {
            OperationSaveExpanded op = new OperationSaveExpanded();
            TreeList.NodesIterator.DoOperation(op);
            return op.Nodes;
        }

        private ArrayList GetSelected()
        {
            ArrayList al = new ArrayList();
            foreach (TreeListNode node in TreeList.Selection)
                al.Add(node.GetValue(TreeList.KeyFieldName));
            return al;
        }

        /// <summary>
        /// 同步的展开树节点，选中节点信息
        /// </summary>
        /// <param name="targetTree">TreeList</param>
        public void LoadState(TreeList targetTree)
        {
            targetTree.BeginUpdate();
            try
            {
                targetTree.CollapseAll();
                TreeListNode node;
                foreach (object key in expanded)
                {
                    node = targetTree.FindNodeByKeyID(key);
                    if (node != null)
                        node.Expanded = true;
                }
                foreach (object key in selected)
                {
                    node = targetTree.FindNodeByKeyID(key);
                    if (node != null)
                    {
                        targetTree.Selection.Add(node);
                    }
                }
                targetTree.FocusedNode = targetTree.FindNodeByKeyID(focused);
            }
            finally
            {
                targetTree.EndUpdate();
                TreeListNode _tagFocusedNode = targetTree.FocusedNode;
                if (_tagFocusedNode != null)
                {
                    targetTree.TopVisibleNodeIndex = targetTree.GetVisibleIndexByNode(_tagFocusedNode);
                    targetTree.MakeNodeVisible(_tagFocusedNode);
                }
            }
        }

        /// <summary>
        /// 同步的展开树节点，选中节点信息
        /// </summary>
        public void LoadState()
        {
            TreeList.BeginUpdate();
            try
            {
                TreeList.CollapseAll();
                TreeListNode node;
                foreach (object key in expanded)
                {
                    node = TreeList.FindNodeByKeyID(key);
                    if (node != null)
                        node.Expanded = true;
                }
                foreach (object key in selected)
                {
                    node = TreeList.FindNodeByKeyID(key);
                    if (node != null)
                        TreeList.Selection.Add(node);
                }
                TreeList.FocusedNode = TreeList.FindNodeByKeyID(focused);
            }
            finally
            {
                TreeList.EndUpdate();
                TreeList.TopVisibleNodeIndex = TreeList.GetVisibleIndexByNode(TreeList.FocusedNode) - topIndex;
            }
        }

        /// <summary>
        /// 保存树节点的展开信息以及选中节点
        /// </summary>
        public void SaveState()
        {
            if (TreeList.FocusedNode != null)
            {
                expanded = GetExpanded();
                selected = GetSelected();
                focused = TreeList.FocusedNode[TreeList.KeyFieldName];
                topIndex = TreeList.GetVisibleIndexByNode(TreeList.FocusedNode) - TreeList.TopVisibleNodeIndex;
            }
            else
                Clear();
        }

        private TreeList treeList;

        /// <summary>
        /// 树
        /// </summary>
        public TreeList TreeList
        {
            get
            {
                return treeList;
            }
            set
            {
                treeList = value;
                Clear();
            }
        }

        private class OperationSaveExpanded : TreeListOperation
        {
            private ArrayList al = new ArrayList();

            public override void Execute(TreeListNode node)
            {
                if (node.HasChildren && node.Expanded)
                    al.Add(node.GetValue(node.TreeList.KeyFieldName));
            }

            public ArrayList Nodes { get { return al; } }
        }
    }
}