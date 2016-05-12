using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YanZhiwei.DotNet3._5.UtilitiesTests.Model
{
    public class DeepNodeData
    {
        public int Depth { get; set; }
        public string Text { get; set; }
        public IEnumerable<DeepNodeData> Children { get; set; }
    }
}
