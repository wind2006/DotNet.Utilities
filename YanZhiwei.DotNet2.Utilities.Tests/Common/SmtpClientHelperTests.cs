using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using YanZhiwei.DotNet2.Utilities.Models;

namespace YanZhiwei.DotNet2.Utilities.Common.Tests
{
    [TestClass()]
    public class SmtpClientHelperTests
    {
        [TestMethod()]
        public void SendTest()
        {
            SmtpServer _server = new SmtpServer("smtp.163.com", "18501600184@163.com", "******");
            SmtpClientHelper _client = new SmtpClientHelper(_server, "楚人游子", DateTime.Now.FormatDate(1) + "测试", DateTime.Now.FormatDate(1) + "单元测试", new string[] { "churenyouzi@outlook.com" });
            Tuple<bool, string, Exception> _result = _client.Send();
            if (!_result.Item1)
                Assert.Inconclusive(_result.Item2 + _result.Item3.Message);
            Assert.IsTrue(_result.Item1);
        }
    }
}