using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;

namespace YanZhiwei.DotNet2.Utilities.Common.Tests
{
    [TestClass()]
    public class CSVHelperTests
    {
        private DataTable TestTable;

        [TestMethod()]
        public void ToCSVTest()
        {
            for (Int16 i = 18; i < 28; i++)
            {
                DataRow _person = TestTable.NewRow();
                _person["Name"] = "YanZhiwei" + i;
                _person["Age"] = i;
                TestTable.Rows.Add(_person);
            }
            bool _expected = true;
            bool _actual = CSVHelper.ToCSV(TestTable, @"C:\Users\YanZh_000\Downloads\person.csv", "用户信息表", "名称,年龄");
            Assert.AreEqual(_expected, _actual);
        }

        [TestInitialize]
        public void InitTestTable()
        {
            TestTable = new DataTable();
            TestTable.Columns.Add(new DataColumn("Name", typeof(string)));
            TestTable.Columns.Add(new DataColumn("Age", typeof(int)));
        }

        [TestMethod()]
        public void ImportToTableTest()
        {
            DataTable _personInfoView = TestTable.Clone();
            DataTable _expected = TestTable.Clone();
            for (Int16 i = 18; i < 28; i++)
            {
                DataRow _person = _expected.NewRow();
                _person["Name"] = "YanZhiwei" + i;
                _person["Age"] = i;
                _expected.Rows.Add(_person);
            }
            DataTable _actual = CSVHelper.ImportToTable(_personInfoView, @"C:\Users\YanZh_000\Downloads\person.csv", 2);
            Assert.IsTrue(ResultSetComparer.AreIdenticalResultSets(_expected, _actual));
        }

        [TestCleanup]
        public void ResetTable()
        {
            TestTable = null;
        }
    }
}