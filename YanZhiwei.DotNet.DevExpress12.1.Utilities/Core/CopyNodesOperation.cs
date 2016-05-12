using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Nodes.Operations;

namespace YanZhiwei.DotNet.DevExpress12._1.Utilities.Core
{
    /// <summary>
    /// 复制TreeList的TreeListOperation
    /// <para>eg: tvwCab.LHTree.NodesIterator.DoOperation(new CopyNodesOperation(tvwCabGpScene.LHTree));</para>
    /// </summary>
    public class CopyNodesOperation : TreeListOperation
    {
        private TreeList DestTreeList;

        public CopyNodesOperation(TreeList destTreeList)
        {
            this.DestTreeList = destTreeList;
        }

        public override void Execute(TreeListNode node)
        {
            object[] _values = new object[node.TreeList.Columns.Count];
            for (int i = 0; i < node.TreeList.Columns.Count; i++)
                _values[i] = node.GetValue(i);
            if (node.ParentNode == null)
                DestTreeList.AppendNode(_values, null);
            else DestTreeList.AppendNode(_values, node.ParentNode.Id);
        }
    }
}