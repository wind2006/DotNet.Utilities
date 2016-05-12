using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace YanZhiwei.DotNet2.Utilities.Common.Tests
{
    [TestClass()]
    public class NetWorkHelperTests
    {
        [TestMethod()]
        public void GetHostNameTest()
        {
            Assert.AreEqual("YanZhiwei-PC", NetWorkHelper.GetHostName());
        }

        [TestMethod()]
        public void PortInUseTest()
        {
            HttpListener _httpListner = new HttpListener();
            _httpListner.Prefixes.Add("http://*:8080/");
            _httpListner.Start();
            Assert.IsTrue(NetWorkHelper.PortInUse(8080));
            _httpListner.Close();
        }
    }
}