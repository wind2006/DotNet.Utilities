using YanZhiwei.DotNet2.Utilities.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace YanZhiwei.DotNet2.Utilities.Common.Tests
{
    [TestClass()]
    public class StringBuilderHelperTests
    {
        [TestMethod()]
        public void ClearTest()
        {
            StringBuilder _builder = new StringBuilder();
            _builder.Append(DateTime.Now);
            StringBuilderHelper.Clear(_builder);
            Assert.AreEqual(0, _builder.Length);
        }

        [TestMethod()]
        public void NullOrCreateTest()
        {
            StringBuilder _builder = null;
            _builder = StringBuilderHelper.NullOrCreate(_builder);
            Assert.AreNotEqual(null, _builder);
        }

        [TestMethod()]
        public void RemoveLastTest()
        {
            StringBuilder _builder = new StringBuilder();
            _builder.Append("Hello World;");
            _builder = _builder.RemoveLast(";");
            Assert.AreEqual("Hello World", _builder.ToString());
        }
    }
}