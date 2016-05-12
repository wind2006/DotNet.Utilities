using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using YanZhiwei.DotNet3._5.UtilitiesTests.Model;

namespace YanZhiwei.DotNet3._5.Utilities.Common.Tests
{
    [TestClass()]
    public class RecursiveJoinHelperTests
    {
        [TestMethod()]
        public void RecursiveJoinTest()
        {
            FlatData[] _elements = new FlatData[]{
                    new FlatData {Id = 1, Text = "A"},
                    new FlatData {Id = 2, Text = "B"},
                    new FlatData {Id = 3, ParentId = 1, Text = "C"},
                    new FlatData {Id = 4, ParentId = 1, Text = "D"},
                    new FlatData {Id = 5, ParentId = 2, Text = "E"}
                };

            IEnumerable<NodeData> _nodes = _elements.RecursiveJoin(element => element.Id,
               element => element.ParentId,
               (FlatData element, IEnumerable<NodeData> children) => new NodeData()
               {
                   Text = element.Text,
                   Children = children
               });
            Assert.AreEqual(2, _nodes.Count());
            IEnumerable<DeepNodeData> _nodes2 = _elements.RecursiveJoin(element => element.Id, element => element.ParentId, (FlatData element, int index, int depth, IEnumerable<DeepNodeData> children) =>
            {
                return new DeepNodeData()
                {
                    Text = element.Text,
                    Children = children,
                    Depth = depth
                };
            });
            Assert.AreEqual(2, _nodes2.Count());
        }
    }
}