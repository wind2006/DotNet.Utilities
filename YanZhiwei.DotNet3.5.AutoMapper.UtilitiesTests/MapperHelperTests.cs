using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using YanZhiwei.DotNet2.Utilities.Common;

namespace YanZhiwei.DotNet3._5.AutoMapper.Utilities.Tests
{
    [TestClass()]
    public class MapperHelperTests
    {
        private SqlServerHelper sqlHelper = null;

        [TestInitialize]
        public void Init()
        {
            sqlHelper = new SqlServerHelper(@"Server=YANZHIWEI-IT-PC\SQLEXPRESS;DataBase=Northwind;User Id=sa;Password=sasa;");
        }

        [TestMethod()]
        public void GetEntitysTest()
        {
            string _sql = "SELECT [CustomerID],[CompanyName],[ContactName],[ContactTitle],[Address],[City],[Region],[PostalCode],[Country],[Phone],[Fax] FROM [Customers]";
            //IDataReader _reader = sqlHelper.ExecuteReader(_sql, null);
            //IEnumerable<Customers> _customers = _reader.GetEntitys<Customers>();
            IEnumerable<Customers> _customers = sqlHelper.ExecuteReader<Customers>(_sql, null);
            Assert.IsNotNull(_customers);
        }
    }
}