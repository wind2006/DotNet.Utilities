using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using YanZhiwei.DotNet.Dapper.Utilities;

namespace YanZhiwei.DotNet.Dapper.UtilitiesTests
{
    [TestClass()]
    public class DapperSqlServerHelperTests
    {
        private DapperHelper sqlHelper = null;

        [TestInitialize()]
        public void Init()
        {
            sqlHelper = new DapperSqlServerHelper(@"Server=YANZHIWEI-IT-PC\SQLEXPRESS;Database=Sample;uid=sa;pwd=sasa;");
        }

        [TestMethod()]
        public void ExecuteDataTableTest()
        {
            DataTable _table = sqlHelper.ExecuteDataTable<Person>("SELECT  [id],[username],[password],[age],[registerDate],[address] FROM [Person] where id=@id", new Person() { id = 5 });
            Assert.AreEqual(_table.Rows[0][0], 5);
        }

        [TestMethod()]
        public void ExecuteNonQueryTest()
        {
            sqlHelper.ExecuteNonQuery<Person>("INSERT INTO [Person]([username],[password],[age],[registerDate],[address]) VALUES(@username,@password, @age,@registerDate, @address)", new Person()
            {
                username = "YanZhiwei",
                address = "ZhuZhou",
                age = 10,
                password = "123",
                registerDate = DateTime.Now
            });
            DataTable _table = sqlHelper.ExecuteDataTable<Person>("SELECT [username] FROM [Person] where username=@userName", new Person() { username = "YanZhiwei" });
            Assert.AreEqual(_table.Rows[0][0], "YanZhiwei");
            sqlHelper.ExecuteNonQuery<Person>("DELETE FROM [Person] where username=@username", new Person() { username = "YanZhiwei" });
        }

        [TestMethod()]
        public void ExecuteReaderTest()
        {
            using (IDataReader reader = sqlHelper.ExecuteReader("SELECT  [id],[username],[password],[age],[registerDate],[address] FROM [Person] where id=@id", new Person() { id = 5 }))
            {
                while (reader.Read())
                {
                    Assert.AreEqual(5, reader.GetInt32(0));
                }
            }
        }

        [TestMethod()]
        public void QueryListTest()
        {
            List<Person> _personList = sqlHelper.QueryList<Person>("SELECT  [id],[username],[password],[age],[registerDate],[address] FROM [Person]", null);
            Assert.IsNotNull(_personList);
        }

        [TestMethod()]
        public void QueryTest()
        {
            Person _person = sqlHelper.Query<Person>("SELECT  [id],[username],[password],[age],[registerDate],[address] FROM [Person] where id=@id", new Person() { id = 5 });
            Assert.IsNotNull(_person);
        }

        [TestMethod()]
        public void ExecuteScalarTest()
        {
            var _userName = sqlHelper.ExecuteScalar("SELECT [username] FROM [Person] where id=@id", new Person() { id = 5 });
            Assert.AreEqual("1", _userName);
        }

        //[TestMethod()]
        //public void ExecuteStoredProcedureTest()
        //{
        //    List<Person> _person = sqlHelper.ExecuteStoredProcedure<Person>("getPerson", new Person() { id = 5,registerDate=DateTime.Now });
        //    Assert.IsNotNull(_person);
        //}
    }
}