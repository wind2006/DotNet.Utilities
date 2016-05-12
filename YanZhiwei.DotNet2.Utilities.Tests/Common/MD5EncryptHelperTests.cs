using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace YanZhiwei.DotNet2.Utilities.Common.Tests
{
    [TestClass()]
    public class MD5EncryptHelperTests
    {
        public void EqualsRandomMD5Test()
        {
            string _data = "yanzhiwei";
            Assert.IsTrue(_data.EqualsRandomMD5(_data.ToRandomMD5()));
        }
    }
}