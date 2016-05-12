using System.Collections.Generic;

namespace YanZhiwei.DotNet3._5.UtilitiesTests.Model
{
    public class NodeData
    {
        public string Text { get; set; }

        public IEnumerable<NodeData> Children { get; set; }
    }
}