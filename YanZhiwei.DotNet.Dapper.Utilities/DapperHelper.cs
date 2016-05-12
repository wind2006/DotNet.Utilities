namespace YanZhiwei.DotNet.Dapper.Utilities
{
    using DotNet2.Utilities.Common;
    using global::Dapper;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    /// <summary>
    /// Dapper 数据库操作帮助类，默认是sql Server
    /// </summary>
    /// 时间：2016-01-19 16:21
    /// 备注：
    public abstract class DapperHelper
    {
        #region Fields

        private string ConnectString = string.Empty;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectString">连接字符串</param>
        /// 时间：2016-01-19 16:21
        /// 备注：
        public DapperHelper(string connectString)
        {
            ValidateHelper.Begin().NotNullOrEmpty(connectString, "连接字符串不能为空！");
            ConnectString = connectString;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// 创建SqlConnection连接对象，需要打开
        /// </summary>
        /// <returns>IDbConnection</returns>
        /// 时间：2016-01-19 16:22
        /// 备注：
        public virtual IDbConnection CreateConnection()
        {
            IDbConnection sqlConnection = new SqlConnection(ConnectString);
            if (sqlConnection.State != ConnectionState.Open)
                sqlConnection.Open();
            return sqlConnection;
        }

        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="dbConnection">IDbConnection</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>DataTable</returns>
        /// 时间：2016-01-19 16:22
        /// 备注：
        public virtual DataTable ExecuteDataTable<T>(IDbConnection dbConnection, string sql, T parameters)
            where T : class
        {
            if (dbConnection == null)
                dbConnection = CreateConnection();
            using (IDbConnection sqlConnection = dbConnection)
            {
                DataTable _table = new DataTable();
                _table.Load(sqlConnection.ExecuteReader(sql, parameters));
                return _table;
            }
        }

        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>DataTable</returns>
        /// 时间：2016-01-19 16:23
        /// 备注：
        public virtual DataTable ExecuteDataTable<T>(string sql, T parameters)
            where T : class
        {
            return ExecuteDataTable<T>(null, sql, parameters);
        }

        /// <summary>
        /// 执行操作
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="dbConnection">IDbConnection</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>影响行数</returns>
        /// 时间：2016-01-19 16:23
        /// 备注：
        public virtual int ExecuteNonQuery<T>(IDbConnection dbConnection, string sql, T parameters)
            where T : class
        {
            if (dbConnection == null)
                dbConnection = CreateConnection();
            using (IDbConnection sqlConnection = dbConnection)
            {
                return sqlConnection.Execute(sql, parameters);
            }
        }

        /// <summary>
        /// 执行操作
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>影响行数</returns>
        /// 时间：2016-01-19 16:23
        /// 备注：
        public virtual int ExecuteNonQuery<T>(string sql, T parameters)
            where T : class
        {
            return ExecuteNonQuery<T>(null, sql, parameters);
        }

        /// <summary>
        /// 返回IDataReader
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="dbConnection">IDbConnection</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>IDataReader</returns>
        /// 时间：2016-01-19 16:24
        /// 备注：
        public virtual IDataReader ExecuteReader<T>(IDbConnection dbConnection, string sql, T parameters)
            where T : class
        {
            if (dbConnection == null)
                dbConnection = CreateConnection();
            IDbConnection sqlConnection = dbConnection;

            return sqlConnection.ExecuteReader(sql, parameters);
        }

        /// <summary>
        /// 返回IDataReader
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>IDataReader</returns>
        /// 时间：2016-01-19 16:24
        /// 备注：
        public virtual IDataReader ExecuteReader<T>(string sql, T parameters)
            where T : class
        {
            return ExecuteReader<T>(null, sql, parameters);
        }

        /// <summary>
        /// 返回对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="dbConnection">IDbConnection</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>object</returns>
        /// 时间：2016-01-19 16:25
        /// 备注：
        public virtual object ExecuteScalar<T>(IDbConnection dbConnection, string sql, T parameters)
            where T : class
        {
            if (dbConnection == null)
                dbConnection = CreateConnection();
            using (IDbConnection sqlConnection = dbConnection)
            {
                return sqlConnection.ExecuteScalar(sql, parameters, null, null, null);
            }
        }

        /// <summary>
        /// 返回对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>object</returns>
        /// 时间：2016-01-19 16:25
        /// 备注：
        public virtual object ExecuteScalar<T>(string sql, T parameters)
            where T : class
        {
            return ExecuteScalar<T>(null, sql, parameters);
        }

        /// <summary>
        /// 返回实体类
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="dbConnection">IDbConnection</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>实体类</returns>
        /// 时间：2016-01-19 16:25
        /// 备注：
        public virtual T Query<T>(IDbConnection dbConnection, string sql, T parameters)
            where T : class
        {
            if (dbConnection == null)
                dbConnection = CreateConnection();
            T _result = null;
            using (IDbConnection sqlConnection = dbConnection)
            {
                _result = sqlConnection.Query<T>(sql, parameters).FirstOrDefault();
            }

            return _result;
        }

        /// <summary>
        /// 返回实体类
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>实体类</returns>
        /// 时间：2016-01-19 16:25
        /// 备注：
        public virtual T Query<T>(string sql, T parameters)
            where T : class
        {
            return Query<T>(null, sql, parameters);
        }

        /// <summary>
        /// 返回集合
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="dbConnection">IDbConnection</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>集合</returns>
        /// 时间：2016-01-19 16:25
        /// 备注：
        public virtual List<T> QueryList<T>(IDbConnection dbConnection, string sql, T parameters)
            where T : class
        {
            if (dbConnection == null)
                dbConnection = CreateConnection();
            List<T> _result = null;
            using (IDbConnection sqlConnection = dbConnection)
            {
                _result = sqlConnection.Query<T>(sql, parameters).ToList();
            }

            return _result;
        }

        /// <summary>
        /// 返回集合，默认Sql Server数据库
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>集合</returns>
        /// 时间：2016-01-19 16:25
        /// 备注：
        public virtual List<T> QueryList<T>(string sql, T parameters)
            where T : class
        {
            return QueryList<T>(null, sql, parameters);
        }

        #endregion Methods
    }
}