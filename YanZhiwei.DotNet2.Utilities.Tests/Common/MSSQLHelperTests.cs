using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using YanZhiwei.DotNet2.Utilities.Core;

namespace YanZhiwei.DotNet2.Utilities.Common.Tests
{
    [TestClass()]
    public class MSSQLHelperTests
    {
        private SqlServerHelper SqlHelper = null;

        [TestInitialize]
        public void InitConnection()
        {
            string _sqlConnectString = @"server=YANZHIWEI-IT-PC\SQLEXPRESS;database=Sample;uid=sa;pwd=sasa;";
            SqlHelper = new SqlServerHelper(_sqlConnectString);
        }

        [TestCleanup]
        public void CleatTestData()
        {
            //string _sql = "delete from Person";
            //int _actual = SqlHelper.ExecuteNonQuery(_sql, null);
        }

        [TestMethod()]
        public void ExecuteNonQueryTest()
        {
            string _sql = "insert into [Person](PName,PAge,PAddress) values(@pname,@page,@paddress)";
            int _actual = SqlHelper.ExecuteNonQuery(_sql,
                                      new DbParameter[3] {
                                          new SqlParameter("@pname","YanZhiwei"),
                                          new SqlParameter("@page",18),
                                          new SqlParameter("@paddress","zhuzhou")
                                      });
            Assert.IsTrue(_actual >= 1);
        }

        [TestMethod()]
        public void ExecuteReaderTest()
        {
            ExecuteNonQueryTest();
            string _sql = "select * from dbo.Person where PName=@pname";
            IDataReader _reader = SqlHelper.ExecuteReader(_sql, new DbParameter[1] { new SqlParameter("@pname", "YanZhiwei") });
            Assert.IsTrue(_reader.Read());
        }

        [TestMethod()]
        public void ExecuteDataTableTest()
        {
            ExecuteNonQueryTest();
            string _sql = "select * from dbo.Person where PName=@pname";
            DataTable _table = SqlHelper.ExecuteDataTable(_sql, new DbParameter[1] { new SqlParameter("@pname", "YanZhiwei") });
            Assert.IsTrue(_table.Rows.Count > 0);
        }

        [TestMethod()]
        public void ExecuteScalarTest()
        {
            ExecuteNonQueryTest();
            string _sql = "select PAge from dbo.Person where PName=@pname";
            object _value = SqlHelper.ExecuteScalar(_sql, new DbParameter[1] { new SqlParameter("@pname", "YanZhiwei") });
            int _actual = Convert.ToInt32(_value);
            Assert.AreEqual(_actual, 18);
        }

        [TestMethod()]
        public void BatchInertTest()
        {
            DataTable _db = DataHelper.CreateTable("PName,PAge|int,PAddress");
            Random _rd = new Random();
            for (int i = 0; i < 300; i++)
            {
                DataRow _row = _db.NewRow();
                _row["PName"] = "YanZhiwei" + i;
                _row["Page"] = _rd.Next(0, 100);
                _row["PAddress"] = "shanghai" + i;
                _db.Rows.Add(_row);
            }
            int _actual = SqlHelper.BatchInert("Person", _db, 300);
            Assert.AreEqual(_actual, 300);
        }

        [TestMethod()]
        public void StoreExecuteDataReaderTest()
        {
            ExecuteNonQueryTest();
            SqlParameter _parameter = new SqlParameter("@pName", SqlDbType.NVarChar);
            _parameter.Direction = ParameterDirection.Input;
            _parameter.Value = "YanZhiwei";
            DbParameter[] _parameterList = new DbParameter[1] { _parameter };
            IDataReader _reader = SqlHelper.StoreExecuteDataReader("PROC_FilterPerson", _parameterList);
            Assert.IsTrue(_reader.Read());
        }

        [TestMethod()]
        public void ExecuteNonQueryTest1()
        {
            using (SqlServerTransaction tranObj = SqlHelper.BeginTranscation())
            {
                try
                {
                    string _sql = "insert into [Person](PName,PAge,PAddress) values(@pname,@page,@paddress)";
                    SqlHelper.ExecuteNonQuery(tranObj, _sql,
                                               new DbParameter[3] {
                                          new SqlParameter("@pname","YanZhiwei"),
                                          new SqlParameter("@page",18),
                                          new SqlParameter("@paddress","zhuzhou")
                                               });
                    throw new Exception("test");

                    SqlHelper.ExecuteNonQuery(tranObj, _sql,
                                              new DbParameter[3] {
                                          new SqlParameter("@pname","YanZhiwei2"),
                                          new SqlParameter("@page",19),
                                          new SqlParameter("@paddress","zhuzhou2")
                                              });
                    tranObj.CommitTransaction();
                }
                catch (Exception ex)
                {
                    tranObj.RollbackTransaction();
                }
            }
        }
    }
}