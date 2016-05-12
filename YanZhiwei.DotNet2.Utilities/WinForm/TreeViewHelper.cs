namespace YanZhiwei.DotNet2.Utilities.WinForm
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// TreeView帮助类
    /// </summary>
    public static class TreeViewHelper
    {
        #region Methods

        /// <summary>
        /// 选中节点高亮
        /// <para>eg: treeView1.ApplyNodeHighLight(Color.Red);</para>
        /// </summary>
        /// <param name="treeView">TreeView</param>
        /// <param name="highLightColor">高亮的颜色</param>
        public static void ApplyNodeHighLight(this TreeView treeView, Brush highLightColor)
        {
            if (treeView.DrawMode != TreeViewDrawMode.OwnerDrawText)
            {
                treeView.DrawMode = TreeViewDrawMode.OwnerDrawText;
            }

            if (treeView.HideSelection)
            {
                treeView.HideSelection = false;
            }

            treeView.DrawNode += (sender, e) =>
            {
                TreeView _curTreeView = sender as TreeView;

                e.Graphics.FillRectangle(Brushes.White, e.Node.Bounds);
                if (e.State == TreeNodeStates.Selected)
                {
                    e.Graphics.FillRectangle(highLightColor, new Rectangle(e.Node.Bounds.Left, e.Node.Bounds.Top, e.Node.Bounds.Width, e.Node.Bounds.Height));
                    e.Graphics.DrawString(e.Node.Text, treeView.Font, Brushes.White, e.Bounds);
                }
                else
                {
                    e.DrawDefault = true;
                }
            };
        }

        /// <summary>
        /// 添加右键菜单
        /// <para>eg: treeF18.AttachMenu(contextMenuTree, n => n != null);</para>
        /// </summary>
        /// <param name="treeView">TreeView</param>
        /// <param name="contextMenu">ContextMenuStrip</param>
        /// <param name="showContextMenuHanlder">显示ContextMenuStrip规则委托</param>
        public static void AttachMenu(this TreeView treeView, ContextMenuStrip contextMenu, Predicate<TreeNode> showContextMenuHanlder)
        {
            treeView.MouseDown += (sender, e) =>
            {
                TreeView _curTree = sender as TreeView;
                if (e.Button == MouseButtons.Right)
                {
                    Point _clickPoint = new Point(e.X, e.Y);
                    TreeNode _curNode = _curTree.GetNodeAt(_clickPoint);
                    if (showContextMenuHanlder != null)
                    {
                        if (showContextMenuHanlder(_curNode))
                        {
                            _curTree.SelectedNode = _curNode;
                            _curNode.ContextMenuStrip = contextMenu;
                        }
                    }
                }
            };
        }

        /// <summary>
        /// 检查节点文本是否存在
        /// </summary>
        /// <param name="tree">TreeView</param>
        /// <param name="key">节点文本</param>
        /// <returns>是否存在</returns>
        public static bool CheckNodeExist(this TreeView tree, string key)
        {
            bool _exists = false;
            for (int i = 0; i < tree.Nodes.Count; i++)
            {
                TreeNode _curNode = tree.Nodes[i];
                if (string.Compare(_curNode.Text, key, true) == 0)
                {
                    _exists = true;
                }
                else
                {
                    _exists = CheckNodeExist(tree.Nodes[i], key);
                }
            }

            return _exists;
        }

        /// <summary>
        /// 检查节点是否存在
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="key">The key.</param>
        /// <returns>是否存在</returns>
        /// 日期：2015-10-13 13:46
        /// 备注：
        private static bool CheckNodeExist(TreeNode node, string key)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                TreeNode _node = node.Nodes[i];
                if (string.Compare(_node.Text.Trim(), key, true) == 0)
                {
                    return true;
                }

                if (_node.Nodes.Count > 0)
                {
                    CheckNodeExist(_node, key);
                }
            }

            return false;
        }

        #endregion Methods
    }
}