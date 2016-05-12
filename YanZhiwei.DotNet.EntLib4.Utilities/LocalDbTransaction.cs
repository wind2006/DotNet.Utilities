using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data.Common;

namespace YanZhiwei.DotNet.EntLib4.Utilities
{
    /// <summary>
    /// Local DbTransaction
    /// </summary>
    public class LocalDbTransaction : IDisposable
    {
        /// <summary>
        /// Gets or sets the data base object.
        /// </summary>
        /// <value>
        /// The data base object.
        /// </value>
        public readonly Database DataBaseObj;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalDbTransaction"/> class.
        /// </summary>
        public LocalDbTransaction(Database db)
        {
            if (db != null)
            {
                DataBaseObj = db;
                DbConnection _dbConnection = DataBaseObj.CreateConnection();
                _dbConnection.Open();
                TransactionObj = _dbConnection.BeginTransaction();
            }
        }

        /// <summary>
        /// Gets or sets the transaction object.
        /// </summary>
        /// <value>
        /// The transaction object.
        /// </value>
        public DbTransaction TransactionObj { get; set; }
        /// <summary>
        /// Commits the transaction.
        /// </summary>
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
        /// Rollbacks the transaction.
        /// </summary>
        public void RollbackTransaction()
        {
            if (TransactionObj != null)
            {
                TransactionObj.Rollback();
            }
        }
    }
}