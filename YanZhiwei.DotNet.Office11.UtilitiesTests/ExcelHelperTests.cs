using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace YanZhiwei.DotNet.Office11.Utilities.Tests
{
    [TestClass()]
    public class ExcelHelperTests
    {
        private static List<Person> PersonList = null;
        private static string XlsPath = @"C:\excel.xlsx";
        [TestInitialize]
        public void InitPersonList()
        {
            PersonList = new List<Person>();
            for (int i = 0; i < 10; i++)
            {
                PersonList.Add(new Person() { Name = "YanZhiwei", Age = i });
            }
        }

        [TestMethod()]
        public void ToExcelTest()
        {
            IDictionary<string, List<Person>> _excelSource = new Dictionary<string, List<Person>>();
            _excelSource.Add("测试数据", PersonList);

            ExcelHelper.ToExcel<Person>(_excelSource, XlsPath);
            Assert.IsTrue(File.Exists(XlsPath));
        }
        [TestCleanup]
        public void Clear()
        {
            //if (File.Exists(XlsPath))
            //    File.Delete(XlsPath);
        }
    }

    public class Person
    {
        [DisplayName("姓名")]
        public string Name { get; set; }
        [DisplayName("年龄")]
        public int Age { get; set; }
    }
}