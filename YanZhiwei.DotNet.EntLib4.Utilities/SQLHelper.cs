using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using System.Data.Common;

namespace YanZhiwei.DotNet.EntLib4.Utilities
{
    /// <summary>
    /// 企业库 4.1 数据访问帮助类
    /// </summary>
    public class SQLHelper
    {
        #region 变量

        private static readonly object syncRoot = new object();
        private readonly Database db;

        #endregion 变量

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLHelper"/> class.
        /// </summary>
        /// <param name="cfgDataBaseName">Name of the CFG data base.</param>
        public SQLHelper(string cfgDataBaseName)
        {
            db = DatabaseFactory.CreateDatabase(cfgDataBaseName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLHelper"/> class.
        /// </summary>
        public SQLHelper()
        {
            db = DatabaseFactory.CreateDatabase();
        }

        /// <summary>
        /// Gets the data base.
        /// </summary>
        /// <value>
        /// The data base.
        /// </value>
        public Database dataBase
        {
            get
            {
                lock (syncRoot)
                {
                    return db;
                }
            }
        }

        /// <summary>
        /// Begins the transcation.
        /// </summary>
        /// <returns></returns>
        public LocalDbTransaction BeginTranscation()
        {
            return new LocalDbTransaction(dataBase) { };
        }

        /// <summary>
        /// Executes the data set.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string sql, DbParameter[] parameters)
        {
            return ExecuteBase<DataSet>(sql, parameters, (db, dbCmd) => db.ExecuteDataSet(dbCmd));
        }

        /// <summary>
        /// Executes the data table.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string sql, DbParameter[] parameters)
        {
            DataSet _result = ExecuteDataSet(sql, parameters);
            if (_result != null && _result.Tables.Count > 0)
                return _result.Tables[0];
            return null;
        }

        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, DbParameter[] parameters)
        {
            return ExecuteBase<int>(sql, parameters, (db, dbCmd) => db.ExecuteNonQuery(dbCmd));
        }

        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <param name="localTranscation">The local transcation.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public int ExecuteNonQuery(LocalDbTransaction localTranscation, string sql, DbParameter[] parameters)
        {
            int _result = 0;
            DbTransaction _dbTranscation = localTranscation.TransactionObj;
            DbConnection _curConnection = _dbTranscation.Connection;
            Database _dataBase = localTranscation.DataBaseObj;

            if (_curConnection.State != ConnectionState.Open)
                _curConnection.Open();
            using (DbCommand dbCommand = _dataBase.GetSqlStringCommand(sql))
            {
                if (parameters != null)
                    dbCommand.Parameters.AddRange(parameters);
                _result = _dataBase.ExecuteNonQuery(dbCommand, _dbTranscation);
            }
            return _result;
        }

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public IDataReader ExecuteReader(string sql, DbParameter[] parameters)
        {
            return ExecuteBase<IDataReader>(sql, parameters, (db, dbCmd) => db.ExecuteReader(dbCmd));
        }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public object ExecuteScalar(string sql, DbParameter[] parameters)
        {
            return ExecuteBase<object>(sql, parameters, (db, dbCmd) => db.ExecuteScalar(dbCmd));
        }

        #region 私有方法

        private T ExecuteBase<T>(string sql, DbParameter[] parameters, Func<Database, DbCommand, T> executeHanlder)
        {
            using (DbCommand dbCommand = dataBase.GetSqlStringCommand(sql))
            {
                if (parameters != null)
                    dbCommand.Parameters.AddRange(parameters);
                return executeHanlder(dataBase, dbCommand);
            }
        }

        #endregion 私有方法
    }
}