using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using YanZhiwei.DotNet.EntLib6.UtilitiesTests.Model;
using YanZhiwei.DotNet4.Interfaces.Validations;

namespace YanZhiwei.DotNet.EntLib6.Utilities.Tests
{
    [TestClass()]
    public class ValidateHelperTests
    {
        /// <summary>
        /// 测试
        /// </summary>
        private Person _person;

        /// <summary>
        /// 验证操作
        /// </summary>
        private IValidation _validation;

        /// <summary>
        /// 测试初始化
        /// </summary>
        [TestInitialize]
        public void TestInit()
        {
            _person = new Person();
            _validation = new ValidateHelper();
        }

        /// <summary>
        /// 验证姓名为必填项
        /// </summary>
        [TestMethod]
        public void TestRequired()
        {
            var result = _validation.Validate(_person);
            Assert.AreEqual("姓名不能为空", result.First().ErrorMessage);
        }

        /// <summary>
        /// 验证姓名为必填项及描述过长
        /// </summary>
        [TestMethod]
        public void TestRequired_StringLength()
        {
            _person.Description = "123456";
            var result = _validation.Validate(_person);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("描述不能超过5位", result.Last().ErrorMessage);
        }
    }
}