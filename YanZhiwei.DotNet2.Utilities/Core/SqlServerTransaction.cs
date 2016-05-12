namespace YanZhiwei.DotNet2.Utilities.Core
{
    using System;
    using System.Data.Common;
    using System.Data.SqlClient;

    /// <summary>
    /// Sql Server事务对象
    /// </summary>
    /// 时间：2015-11-24 9:22
    /// 备注：
    public class SqlServerTransaction : IDisposable
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">链接字符串</param>
        /// 时间：2015-11-24 9:21
        /// 备注：
        /// <exception cref="System.ArgumentNullException">未能正确开启事物，因为链接字符串为空！</exception>
        public SqlServerTransaction(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("未能正确开启事物，因为链接字符串为空！");

            DbConnection _dbConnection = new SqlConnection(connectionString);
            _dbConnection.Open();
            TransactionObj = _dbConnection.BeginTransaction();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 事务对象
        /// </summary>
        /// 时间：2015-11-24 9:09
        /// 备注：
        public DbTransaction TransactionObj
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 提交事务
        /// </summary>
        /// 时间：2015-11-24 9:10
        /// 备注：
        public void CommitTransaction()
        {
            if (TransactionObj != null)
            {
                TransactionObj.Commit();
            }
        }

        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务。
        /// </summary>
        public void Dispose()
        {
            if (TransactionObj != null)
            {
                TransactionObj.Dispose();
                TransactionObj = null;
            }
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTransaction()
        {
            if (TransactionObj != null)
            {
                TransactionObj.Rollback();
            }
        }

        #endregion Methods
    }
}