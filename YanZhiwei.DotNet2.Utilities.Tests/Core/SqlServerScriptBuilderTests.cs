using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace YanZhiwei.DotNet2.Utilities.Core.Tests
{
    [TestClass()]
    public class SqlServerScriptBuilderTests
    {
        private SqlServerScriptBuilder sqlScriptBuilder = null;

        [TestInitialize]
        public void Init()
        {
            sqlScriptBuilder = new SqlServerScriptBuilder("Products", "ProductID");
        }

        [TestMethod()]
        public void SelectAllColumnsTest()
        {
            string _sql = sqlScriptBuilder.SelectAllColumns();
            Assert.AreEqual("select * from Products", _sql);

            Hashtable _sqlWhere = new Hashtable();
            _sqlWhere.Add("ProductID", 1);
            _sqlWhere.Add("ProductName", "Aniseed Syrup");
            _sql = sqlScriptBuilder.SelectAllColumns(_sqlWhere);
            Assert.AreEqual("select * from Products where (productname=@productname and productid=@productid  )", _sql);
        }

        [TestMethod()]
        public void SelectTest()
        {
            string _sql = sqlScriptBuilder.Select("[ProductID],[ProductName],[SupplierID],[CategoryID]");
            Assert.AreEqual("select [ProductID],[ProductName],[SupplierID],[CategoryID] from Products", _sql);
        }

        [TestMethod()]
        public void SelectWhereTest()
        {
            Hashtable _sqlWhere = new Hashtable();
            _sqlWhere.Add("ProductID", 1);
            _sqlWhere.Add("ProductName", "Aniseed Syrup");
            string _sql = sqlScriptBuilder.SelectWhere("[ProductID],[ProductName],[SupplierID],[CategoryID]", _sqlWhere);
            Assert.AreEqual("select [ProductID],[ProductName],[SupplierID],[CategoryID] from Products where (productname=@productname and productid=@productid  )", _sql);
        }

        [TestMethod()]
        public void InsertTest()
        {
            Hashtable _sqlWhere = new Hashtable();
            _sqlWhere.Add("ProductID", 1);
            _sqlWhere.Add("ProductName", "Aniseed Syrup");
            string _sql = sqlScriptBuilder.Insert(_sqlWhere);
            Assert.AreEqual("INSERT INTO Products (productname,productid) VALUES (@productname,@productid)", _sql);
            // Assert.Inconclusive(_sql);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Hashtable _sqlWhere = new Hashtable();
            _sqlWhere.Add("ProductID", 1);
            _sqlWhere.Add("ProductName", "Aniseed Syrup");
            string _sql = sqlScriptBuilder.Update(_sqlWhere, _sqlWhere);
            Assert.AreEqual("UPDATE Products SET productname=@productname, productid=@productid  WHERE productname=@productname and productid=@productid", _sql);
            //  Assert.Inconclusive(_sql);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            string _sql = sqlScriptBuilder.Delete();
            Assert.AreEqual("DELETE FROM Products WHERE productid=@productid", _sql);

            Hashtable _sqlWhere = new Hashtable();
            _sqlWhere.Add("ProductID", 1);
            _sqlWhere.Add("ProductName", "Aniseed Syrup");
            _sql = sqlScriptBuilder.Delete(_sqlWhere);
            Assert.AreEqual("DELETE FROM Products WHERE productname=@productname and productid=@productid", _sql);

        }
    }
}