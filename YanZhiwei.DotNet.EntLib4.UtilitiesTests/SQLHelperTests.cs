using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using YanZhiwei.DotNet2.Utilities.Common;

namespace YanZhiwei.DotNet.EntLib4.Utilities.Tests
{
    [TestClass()]
    public class SQLHelperTests
    {
        private SQLHelper sqlHelper = null;

        [TestInitialize]
        public void Init()
        {
            sqlHelper = new SQLHelper();
            ClearProductTestDb();
        }

        private void ClearProductTestDb()
        {
            string _sql = "delete from Products  where ProductName like @ProductName";
            DbParameter[] _paramterList = new SqlParameter[1];
            _paramterList[0] = new SqlParameter("@ProductName", "%UnitTest%");
            sqlHelper.ExecuteNonQuery(_sql, _paramterList);
        }

        [TestMethod()]
        public void ExecuteDataTableTest()
        {
            string _sql = "select * from Customers where CustomerID=@CustomerID";
            DbParameter[] _paramterList = new SqlParameter[1];
            _paramterList[0] = new SqlParameter("@CustomerID", 1);
            DataTable _result = sqlHelper.ExecuteDataTable(_sql, _paramterList);
            Assert.IsNotNull(_result);
        }

        [TestMethod()]
        public void ExecuteNonQueryTest()
        {
            #region ExecuteNonQuery 普通

            string _sql = @"INSERT INTO [dbo].[Products]
		([ProductName]
		,[CategoryID]
		,[UnitPrice]
		,[LastUpdate])
	VALUES
		(@ProductName
		,@CategoryID
		,@UnitPrice
		,@LastUpdate)";

            DbParameter[] _paramterList = new SqlParameter[4];
            _paramterList[0] = new SqlParameter("@ProductName", DateTime.Now.FormatDate(12));
            _paramterList[1] = new SqlParameter("@CategoryID", 7);
            _paramterList[2] = new SqlParameter("@UnitPrice", 10);
            _paramterList[3] = new SqlParameter("@LastUpdate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            sqlHelper.ExecuteNonQuery(_sql, _paramterList);

            #endregion ExecuteNonQuery 普通

            #region ExecuteNonQuery 事物 成功

            ClearProductTestDb();
            using (LocalDbTransaction tranObj = sqlHelper.BeginTranscation())
            {
                try
                {
                    AddProduct(tranObj, string.Format("{0}_UnitTest_1", DateTime.Now.FormatDate(13)), 22);
                    AddProduct(tranObj, string.Format("{0}_UnitTest_2", DateTime.Now.FormatDate(13)), 33);
                    tranObj.CommitTransaction();
                }
                catch (Exception)
                {
                    tranObj.RollbackTransaction();
                }
            }

            _sql = "select UnitPrice from Products where ProductName=@ProductName";
            _paramterList = new SqlParameter[1];
            _paramterList[0] = new SqlParameter("@ProductName", string.Format("{0}_UnitTest_1", DateTime.Now.FormatDate(13)));
            Assert.AreEqual(22m, sqlHelper.ExecuteScalar(_sql, _paramterList));

            _paramterList = new SqlParameter[1];
            _paramterList[0] = new SqlParameter("@ProductName", string.Format("{0}_UnitTest_2", DateTime.Now.FormatDate(13)));
            Assert.AreEqual(33m, sqlHelper.ExecuteScalar(_sql, _paramterList));
            ClearProductTestDb();

            #endregion ExecuteNonQuery 事物 成功

            #region ExecuteNonQuery 事物 失败

            ClearProductTestDb();
            using (LocalDbTransaction tranObj = sqlHelper.BeginTranscation())
            {
                try
                {
                    AddProduct(tranObj, string.Format("{0}_UnitTest_1", DateTime.Now.FormatDate(13)), 22);
                    throw new Exception("test");
                    AddProduct(tranObj, string.Format("{0}_UnitTest_2", DateTime.Now.FormatDate(13)), 33);
                    tranObj.CommitTransaction();
                }
                catch (Exception)
                {
                    tranObj.RollbackTransaction();
                }
            }

            _sql = "select UnitPrice from Products where ProductName=@ProductName";
            _paramterList = new SqlParameter[1];
            _paramterList[0] = new SqlParameter("@ProductName", string.Format("{0}_UnitTest_1", DateTime.Now.FormatDate(13)));
            Assert.IsNull(sqlHelper.ExecuteScalar(_sql, _paramterList));

            _paramterList = new SqlParameter[1];
            _paramterList[0] = new SqlParameter("@ProductName", string.Format("{0}_UnitTest_2", DateTime.Now.FormatDate(13)));
            Assert.IsNull(sqlHelper.ExecuteScalar(_sql, _paramterList));

            #endregion ExecuteNonQuery 事物 失败

            #region ExecuteNonQuery 事物嵌套 成功

            ClearProductTestDb();
            using (LocalDbTransaction tranObj = sqlHelper.BeginTranscation())
            {
                try
                {
                    AddProduct(tranObj, string.Format("{0}_UnitTest_1", DateTime.Now.FormatDate(13)), 22);
                    using (LocalDbTransaction tranObjChild = sqlHelper.BeginTranscation())
                    {
                        try
                        {
                            AddProduct(tranObjChild, string.Format("{0}_UnitTest_3", DateTime.Now.FormatDate(13)), 44);
                            AddProduct(tranObjChild, string.Format("{0}_UnitTest_4", DateTime.Now.FormatDate(13)), 55);
                            tranObjChild.CommitTransaction();
                        }
                        catch (Exception)
                        {
                            tranObjChild.RollbackTransaction();
                        }
                    }
                    AddProduct(tranObj, string.Format("{0}_UnitTest_2", DateTime.Now.FormatDate(13)), 33);
                    tranObj.CommitTransaction();
                }
                catch (Exception)
                {
                    tranObj.RollbackTransaction();
                }
            }

            _sql = "select UnitPrice from Products where ProductName=@ProductName";
            _paramterList = new SqlParameter[1];
            _paramterList[0] = new SqlParameter("@ProductName", string.Format("{0}_UnitTest_1", DateTime.Now.FormatDate(13)));
            Assert.AreEqual(22m, sqlHelper.ExecuteScalar(_sql, _paramterList));

            _paramterList = new SqlParameter[1];
            _paramterList[0] = new SqlParameter("@ProductName", string.Format("{0}_UnitTest_2", DateTime.Now.FormatDate(13)));
            Assert.AreEqual(33m, sqlHelper.ExecuteScalar(_sql, _paramterList));

            _paramterList = new SqlParameter[1];
            _paramterList[0] = new SqlParameter("@ProductName", string.Format("{0}_UnitTest_3", DateTime.Now.FormatDate(13)));
            Assert.AreEqual(44m, sqlHelper.ExecuteScalar(_sql, _paramterList));

            _paramterList = new SqlParameter[1];
            _paramterList[0] = new SqlParameter("@ProductName", string.Format("{0}_UnitTest_4", DateTime.Now.FormatDate(13)));
            Assert.AreEqual(55m, sqlHelper.ExecuteScalar(_sql, _paramterList));

            #endregion ExecuteNonQuery 事物嵌套 成功

            #region ExecuteNonQuery 事物嵌套 子事物失败

            ClearProductTestDb();
            using (LocalDbTransaction tranObj = sqlHelper.BeginTranscation())
            {
                try
                {
                    AddProduct(tranObj, string.Format("{0}_UnitTest_1", DateTime.Now.FormatDate(13)), 22);
                    using (LocalDbTransaction tranObjChild = sqlHelper.BeginTranscation())
                    {
                        try
                        {
                            AddProduct(tranObjChild, string.Format("{0}_UnitTest_3", DateTime.Now.FormatDate(13)), 44);
                            throw new Exception("test");
                            AddProduct(tranObjChild, string.Format("{0}_UnitTest_4", DateTime.Now.FormatDate(13)), 55);
                            tranObjChild.CommitTransaction();
                        }
                        catch (Exception)
                        {
                            tranObjChild.RollbackTransaction();
                        }
                    }
                    AddProduct(tranObj, string.Format("{0}_UnitTest_2", DateTime.Now.FormatDate(13)), 33);
                    tranObj.CommitTransaction();
                }
                catch (Exception)
                {
                    tranObj.RollbackTransaction();
                }
            }

            _sql = "select UnitPrice from Products where ProductName=@ProductName";
            _paramterList = new SqlParameter[1];
            _paramterList[0] = new SqlParameter("@ProductName", string.Format("{0}_UnitTest_1", DateTime.Now.FormatDate(13)));
            Assert.AreEqual(22m, sqlHelper.ExecuteScalar(_sql, _paramterList));

            _paramterList = new SqlParameter[1];
            _paramterList[0] = new SqlParameter("@ProductName", string.Format("{0}_UnitTest_2", DateTime.Now.FormatDate(13)));
            Assert.AreEqual(33m, sqlHelper.ExecuteScalar(_sql, _paramterList));

            _paramterList = new SqlParameter[1];
            _paramterList[0] = new SqlParameter("@ProductName", string.Format("{0}_UnitTest_3", DateTime.Now.FormatDate(13)));
            Assert.IsNull(sqlHelper.ExecuteScalar(_sql, _paramterList));

            _paramterList = new SqlParameter[1];
            _paramterList[0] = new SqlParameter("@ProductName", string.Format("{0}_UnitTest_4", DateTime.Now.FormatDate(13)));
            Assert.IsNull(sqlHelper.ExecuteScalar(_sql, _paramterList));

            #endregion ExecuteNonQuery 事物嵌套 子事物失败

            #region ExecuteNonQuery 事物嵌套 主事物失败

            ClearProductTestDb();
            using (LocalDbTransaction tranObj = sqlHelper.BeginTranscation())
            {
                try
                {
                    AddProduct(tranObj, string.Format("{0}_UnitTest_1", DateTime.Now.FormatDate(13)), 22);
                    using (LocalDbTransaction tranObjChild = sqlHelper.BeginTranscation())
                    {
                        try
                        {
                            AddProduct(tranObjChild, string.Format("{0}_UnitTest_3", DateTime.Now.FormatDate(13)), 44);
                            AddProduct(tranObjChild, string.Format("{0}_UnitTest_4", DateTime.Now.FormatDate(13)), 55);
                            tranObjChild.CommitTransaction();
                        }
                        catch (Exception)
                        {
                            tranObjChild.RollbackTransaction();
                        }
                    }
                    throw new Exception("test");
                    AddProduct(tranObj, string.Format("{0}_UnitTest_2", DateTime.Now.FormatDate(13)), 33);
                    tranObj.CommitTransaction();
                }
                catch (Exception)
                {
                    tranObj.RollbackTransaction();
                }
            }

            _sql = "select UnitPrice from Products where ProductName=@ProductName";
            _paramterList = new SqlParameter[1];
            _paramterList[0] = new SqlParameter("@ProductName", string.Format("{0}_UnitTest_1", DateTime.Now.FormatDate(13)));
            Assert.IsNull(sqlHelper.ExecuteScalar(_sql, _paramterList));

            _paramterList = new SqlParameter[1];
            _paramterList[0] = new SqlParameter("@ProductName", string.Format("{0}_UnitTest_2", DateTime.Now.FormatDate(13)));
            Assert.IsNull(sqlHelper.ExecuteScalar(_sql, _paramterList));

            _paramterList = new SqlParameter[1];
            _paramterList[0] = new SqlParameter("@ProductName", string.Format("{0}_UnitTest_3", DateTime.Now.FormatDate(13)));
            Assert.AreEqual(44m, sqlHelper.ExecuteScalar(_sql, _paramterList));

            _paramterList = new SqlParameter[1];
            _paramterList[0] = new SqlParameter("@ProductName", string.Format("{0}_UnitTest_4", DateTime.Now.FormatDate(13)));
            Assert.AreEqual(55m, sqlHelper.ExecuteScalar(_sql, _paramterList));

            #endregion ExecuteNonQuery 事物嵌套 主事物失败
        }

        private void AddProduct(LocalDbTransaction tranObj, string key, int UnitPrice)
        {
            string _sql = @"INSERT INTO [dbo].[Products]([ProductName],[CategoryID],[UnitPrice],[LastUpdate])
	    VALUES
		(@ProductName
		,@CategoryID
		,@UnitPrice
		,@LastUpdate)";

            DbParameter[] _paramterList = new SqlParameter[4];
            _paramterList[0] = new SqlParameter("@ProductName", key);
            _paramterList[1] = new SqlParameter("@CategoryID", 7);
            _paramterList[2] = new SqlParameter("@UnitPrice", UnitPrice);
            _paramterList[3] = new SqlParameter("@LastUpdate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sqlHelper.ExecuteNonQuery(tranObj, _sql, _paramterList);
        }

        [TestMethod()]
        public void ExecuteReaderTest()
        {
            string _sql = "select Name from Customers where CustomerID=@CustomerID";
            DbParameter[] _paramterList = new SqlParameter[1];
            _paramterList[0] = new SqlParameter("@CustomerID", 1);
            using (IDataReader reader = sqlHelper.ExecuteReader(_sql, _paramterList))
            {
                while (reader.Read())
                {
                    Assert.AreEqual(reader["Name"], "Maria Anders");
                }
            }
        }

        [TestMethod()]
        public void ExecuteScalarTest()
        {
            string _sql = "select Name from Customers where CustomerID=@CustomerID";
            DbParameter[] _paramterList = new SqlParameter[1];
            _paramterList[0] = new SqlParameter("@CustomerID", 1);
            Assert.AreEqual("Maria Anders", sqlHelper.ExecuteScalar(_sql, _paramterList));
        }
    }
}