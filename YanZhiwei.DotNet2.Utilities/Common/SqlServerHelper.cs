namespace YanZhiwei.DotNet2.Utilities.Common
{
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using YanZhiwei.DotNet2.Interfaces.DataAccess;
    using YanZhiwei.DotNet2.Utilities.Core;

    /// <summary>
    /// Sql Server帮助类
    /// </summary>
    public class SqlServerHelper : ISQLHelper
    {
        #region Fields

        private static string connectionString = string.Empty;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlServerConnectString">连接字符串
        /// <example>eg:server=YANZHIWEI-PC\SQLEXPRESS;database=db;uid=sa;pwd=sasa;</example>
        /// <example>eg:Server= localhost; Database= employeedetails; Integrated Security=True;</example>
        /// </param>
        public SqlServerHelper(string sqlServerConnectString)
        {
            ValidateHelper.Begin().NotNullOrEmpty(sqlServerConnectString, "Sql Server连接字符串");
            connectionString = sqlServerConnectString;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlConnectionStringBuilder">SqlConnectionStringBuilder</param>
        /// 时间：2016-02-26 10:31
        /// 备注：
        public SqlServerHelper(SqlConnectionStringBuilder sqlConnectionStringBuilder)
        {
            ValidateHelper.Begin().NotNull(sqlConnectionStringBuilder, "SqlConnectionStringBuilder");
            connectionString = sqlConnectionStringBuilder.ConnectionString;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// 批量插入
        /// <para>eg:int _actual = SqlHelper.BatchInert("Person", _db, 300);</para>
        /// </summary>
        /// <param name="desTable">数据库目标表</param>
        /// <param name="dataTable">需要插入的表</param>
        /// <param name="batchSize">批量插入数量</param>
        /// <returns>影响行数</returns>
        public int BatchInert(string desTable, DataTable dataTable, int batchSize)
        {
            ValidateHelper.Begin().NotNullOrEmpty(desTable, "数据库目标表").NotNull(dataTable, "需要插入的表").CheckGreaterThan<int>(batchSize, "批量插入数量", 0, false);
            try
            {
                using (SqlBulkCopy sbc = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.UseInternalTransaction)
                {
                    BulkCopyTimeout = 300,
                    NotifyAfter = dataTable.Rows.Count,
                    BatchSize = batchSize,
                    DestinationTableName = desTable
                })
                {
                    foreach (DataColumn column in dataTable.Columns)
                        sbc.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                    sbc.WriteToServer(dataTable);
                }
                return dataTable.Rows.Count;
            }
            catch (SqlException ex)
            {
                string _sqlExMessage = "批量插入异常：" + ex.GetSqlExceptionMessage();
                ex.Data.Add("sqlServerConnectString", connectionString);
                ex.Data.Add("desTable", desTable);
                ex.Data.Add("batchSize", batchSize);
                throw new FrameworkException(_sqlExMessage, ex);
            }
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns>SqlServerTransaction</returns>
        public SqlServerTransaction BeginTranscation()
        {
            return new SqlServerTransaction(connectionString) { };
        }

        /// <summary>
        /// ExecuteDataSet
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataSet(string sql, DbParameter[] parameters)
        {
            try
            {
                CheckedSqlParamter(sql);

                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand(sql, sqlcon))
                    {
                        if (parameters != null)
                            sqlcmd.Parameters.AddRange(parameters);
                        using (SqlDataAdapter _sqldap = new SqlDataAdapter(sqlcmd))
                        {
                            DataSet _dataset = new DataSet();
                            _sqldap.Fill(_dataset);
                            return _dataset;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw CreateFrameworkException(sql, parameters, ex);
            }
        }

        /// <summary>
        /// ExecuteDataTable
        /// <para>eg:string _sql = "select * from dbo.Person where PName=@pname";</para>
        /// <para>DataTable _table = SqlHelper.ExecuteDataTable(_sql, new DbParameter[1] { new SqlParameter("@pname", "YanZhiwei") });</para>
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>DataTable</returns>
        public DataTable ExecuteDataTable(string sql, DbParameter[] parameters)
        {
            try
            {
                CheckedSqlParamter(sql);
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand(sql, sqlcon))
                    {
                        if (parameters != null)
                            sqlcmd.Parameters.AddRange(parameters);
                        using (SqlDataAdapter _sqldap = new SqlDataAdapter(sqlcmd))
                        {
                            DataTable _datatable = new DataTable();
                            _sqldap.Fill(_datatable);
                            return _datatable;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw CreateFrameworkException(sql, parameters, ex);
            }
        }

        /// <summary>
        /// 执行sql语句，返回影响行数
        ///<para>eg: string _sql = "insert into [Person](PName,PAge,PAddress) values(@pname,@page,@paddress)";</para>
        ///<para>int _actual = SqlHelper.ExecuteNonQuery(_sql,</para>
        ///<para>new DbParameter[3] {</para>
        ///<para>new SqlParameter("@pname","YanZhiwei"),</para>
        ///<para>new SqlParameter("@page",18),</para>
        ///<para>new SqlParameter("@paddress","zhuzhou")</para>
        ///<para>});</para>
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>影响行数</returns>
        public int ExecuteNonQuery(string sql, DbParameter[] parameters)
        {
            int _affectedRows = -1;
            try
            {
                CheckedSqlParamter(sql);
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    sqlcon.Open();
                    using (SqlCommand sqlcmd = new SqlCommand(sql, sqlcon))
                    {
                        if (parameters != null)
                            sqlcmd.Parameters.AddRange(parameters);
                        _affectedRows = sqlcmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw CreateFrameworkException(sql, parameters, ex);
            }
            return _affectedRows;
        }

        /// <summary>
        /// 执行sql语句，返回影响行数 带事务
        /// </summary>
        /// <param name="dbTranscaion">事务对象</param>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>影响行数</returns>
        /// 时间：2015-11-24 9:31
        /// 备注：
        public int ExecuteNonQuery(SqlServerTransaction dbTranscaion, string sql, DbParameter[] parameters)
        {
            int _affectedRows = -1;
            CheckedSqlParamter(sql);
            DbTransaction _sqlTranscation = dbTranscaion.TransactionObj;
            DbConnection _sqlConnection = _sqlTranscation.Connection;
            try
            {
                if (_sqlConnection.State != ConnectionState.Open)
                    _sqlConnection.Open();
                using (DbCommand sqlCmd = new SqlCommand(sql))
                {
                    sqlCmd.Connection = _sqlConnection;
                    sqlCmd.Transaction = _sqlTranscation;
                    if (parameters != null)
                        sqlCmd.Parameters.AddRange(parameters);
                    _affectedRows = sqlCmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw CreateFrameworkException(sql, parameters, ex);
            }
            return _affectedRows;
        }

        /// <summary>
        /// 获取分页数据，利用RowNumber()方式
        /// </summary>
        /// <param name="tableName">数据表名『eg:Orders』</param>
        /// <param name="fields">要读取的字段『*:所有列；或者：eg:OrderID,OrderDate,ShipName,ShipCountry』</param>
        /// <param name="orderField">依据排序的列『eg:OrderID』</param>
        /// <param name="orderBy">排序方式『升序，降序』</param>
        /// <param name="sqlWhere">筛选条件『eg:Order=1，若无筛选条件，则""』</param>
        /// <param name="pageSize">每页显示多少条数据</param>
        /// <param name="pageIndex">当前页码</param>
        /// <returns>组元[DataTable,分页总数，记录总数]</returns>
        /// 时间：2016-01-05 13:21
        /// 备注：
        public Tuple<DataTable, int, int> ExecutePageQuery(string tableName, string fields, string orderField, OrderBy orderBy, string sqlWhere, int pageSize, int pageIndex)
        {
            string _sql = SqlServerPageScript.CreateSqlByRowNumber(tableName, fields, orderField, sqlWhere, orderBy, pageSize, pageIndex);
            int _totalPage = 0, _totalCount = 0;
            try
            {
                DataSet _result = ExecuteDataSet(_sql, null);
                if (!_result.IsNullOrEmpty())
                {
                    if (!_result.Tables[0].IsNullOrEmpty() && !_result.Tables[1].IsNullOrEmpty())
                    {
                        _totalCount = _result.Tables[1].Rows[0][0].ToIntOrDefault(0);
                        _totalPage = (int)Math.Ceiling(_totalCount / (double)pageSize);
                        return Tuple.Create(_result.Tables[0], _totalPage, _totalCount);
                    }
                }
            }
            catch (SqlException ex)
            {
                string _sqlExMessage = ex.GetSqlExceptionMessage();
                ex.Data.Add("sqlServerConnectString", connectionString);
                ex.Data.Add("sql", _sql);
                ex.Data.Add("tableName", tableName);
                ex.Data.Add("orderField", orderField);
                ex.Data.Add("orderField", orderField);
                ex.Data.Add("orderBy", orderBy.ToString());
                ex.Data.Add("sqlWhere", sqlWhere);
                ex.Data.Add("pageSize", pageSize);
                ex.Data.Add("pageIndex", pageIndex);
                throw new FrameworkException(_sqlExMessage, ex);
            }
            return Tuple.Create(new DataTable(), _totalPage, _totalCount);
        }

        /// <summary>
        /// ExecuteReader
        /// <para>eg:string _sql = "select * from dbo.Person where PName=@pname";</para>
        /// <para>IDataReader _reader = SqlHelper.ExecuteReader(_sql, new DbParameter[1] { new SqlParameter("@pname", "YanZhiwei") });</para>
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>IDataReader</returns>
        public IDataReader ExecuteReader(string sql, DbParameter[] parameters)
        {
            try
            {
                CheckedSqlParamter(sql);
                SqlConnection sqlcon = new SqlConnection(connectionString);
                using (SqlCommand sqlcmd = new SqlCommand(sql, sqlcon))
                {
                    if (parameters != null)
                        sqlcmd.Parameters.AddRange(parameters);
                    sqlcon.Open();
                    return sqlcmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
            catch (SqlException ex)
            {
                throw CreateFrameworkException(sql, parameters, ex);
            }
        }

        /// <summary>
        /// ExecuteReader
        /// <para>eg:string _sql = "select * from dbo.Person where PName=@pname";</para>
        /// <para>List _products = SqlHelper.ExecuteReader(_sql, new DbParameter[1] { new SqlParameter("@pname", "YanZhiwei") });</para>
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>集合</returns>
        public List<T> ExecuteReader<T>(string sql, DbParameter[] parameters)
            where T : class
        {
            try
            {
                CheckedSqlParamter(sql);
                List<T> _result = new List<T>();

                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand(sql, sqlcon))
                    {
                        if (parameters != null)
                            sqlcmd.Parameters.AddRange(parameters);
                        sqlcon.Open();
                        using (IDataReader reader = sqlcmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            DynamicBuilder.Build _buildFrom = DynamicBuilder.CreateBuilder(reader, typeof(T), DBTypeName.SqlServer);
                            while (reader.Read())
                            {
                                _result.Add((T)_buildFrom(reader));
                            }
                        }
                    }
                }
                return _result;
            }
            catch (SqlException ex)
            {
                throw CreateFrameworkException(sql, parameters, ex);
            }
        }

        /// <summary>
        /// 通过ExecuteReader方式获取实体类
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>对象</returns>
        /// 时间：2016-02-15 15:26
        /// 备注：
        public T ExecuteReaderSingle<T>(string sql, DbParameter[] parameters)
            where T : class
        {
            T _result = default(T);
            try
            {
                CheckedSqlParamter(sql);
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand(sql, sqlcon))
                    {
                        if (parameters != null)
                            sqlcmd.Parameters.AddRange(parameters);
                        sqlcon.Open();
                        using (IDataReader reader = sqlcmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            DynamicBuilder.Build _buildFrom = DynamicBuilder.CreateBuilder(reader, typeof(T), DBTypeName.SqlServer);
                            while (reader.Read())
                            {
                                _result = (T)_buildFrom(reader);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw CreateFrameworkException(sql, parameters, ex);
            }
            return _result;
        }

        /// <summary>
        /// ExecuteScalar
        /// <para>eg:string _sql = "select PAge from dbo.Person where PName=@pname";</para>
        /// <para>object _value = SqlHelper.ExecuteScalar(_sql, new DbParameter[1] { new SqlParameter("@pname", "YanZhiwei") });</para>
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>Object</returns>
        public Object ExecuteScalar(string sql, DbParameter[] parameters)
        {
            try
            {
                CheckedSqlParamter(sql);
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand(sql, sqlcon))
                    {
                        if (parameters != null)
                            sqlcmd.Parameters.AddRange(parameters);
                        sqlcon.Open();
                        return sqlcmd.ExecuteScalar();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw CreateFrameworkException(sql, parameters, ex);
            }
        }

        /// <summary>
        /// ExecuteReader——存储过程
        /// <para>eg:SqlParameter _parameter = new SqlParameter("@pName", SqlDbType.NVarChar);</para>>
        /// <para>   _parameter.Direction = ParameterDirection.Input;</para>
        /// <para>   _parameter.Value = "YanZhiwei";</para>
        /// <para>   DbParameter[] _parameterList = new DbParameter[1] { _parameter };</para>
        /// <para>   IDataReader _reader = SqlHelper.StoreExecuteDataReader("PROC_FilterPerson", _parameterList);</para>
        /// </summary>
        /// <param name="proName">存储过程名称</param>
        /// <param name="parameters">参数</param>
        /// <returns>IDataReader</returns>
        public IDataReader StoreExecuteDataReader(string proName, DbParameter[] parameters)
        {
            try
            {
                CheckedStoreNameParameter(proName);

                SqlConnection _sqlcon = new SqlConnection(connectionString);
                using (SqlCommand sqlcmd = new SqlCommand())
                {
                    sqlcmd.Connection = _sqlcon;
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.CommandText = proName;
                    if (parameters != null)
                        sqlcmd.Parameters.AddRange(parameters);
                    _sqlcon.Open();
                    return sqlcmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
            catch (SqlException ex)
            {
                throw CreateFrameworkException(proName, parameters, ex);
            }
        }

        /// <summary>
        /// ExecuteDataTable——存储过程
        /// </summary>
        /// <param name="proName">存储过程名称</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="outputParamterFacotry">存储过程输出参数</param>
        /// <returns>DataTable</returns>
        /// 时间：2015-11-03 8:58
        /// 备注：
        public DataTable StoreExecuteDataTable(string proName, DbParameter[] parameters, Action<SqlParameterCollection> outputParamterFacotry)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand())
                    {
                        sqlcmd.Connection = sqlcon;
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlcmd.CommandText = proName;
                        if (parameters != null)
                            sqlcmd.Parameters.AddRange(parameters);
                        using (SqlDataAdapter sqldap = new SqlDataAdapter(sqlcmd))
                        {
                            DataTable _table = new DataTable();
                            sqldap.Fill(_table);
                            if (outputParamterFacotry != null)
                                outputParamterFacotry(sqlcmd.Parameters);
                            return _table;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw CreateFrameworkException(proName, parameters, ex);
            }
        }

        /// <summary>
        /// 执行sql语句，返回影响行数——存储过程
        /// </summary>
        /// <param name="proName">存储过程名称</param>
        /// <param name="parameters">参数</param>
        /// <returns>影响行数</returns>
        /// 时间：2016-02-24 15:35
        /// 备注：
        public int StoreExecuteNonQuery(string proName, DbParameter[] parameters)
        {
            try
            {
                int _affectedRows = -1;
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    sqlcon.Open();
                    using (SqlCommand sqlcmd = new SqlCommand())
                    {
                        sqlcmd.Connection = sqlcon;
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlcmd.CommandText = proName;
                        if (parameters != null)
                            sqlcmd.Parameters.AddRange(parameters);
                        _affectedRows = sqlcmd.ExecuteNonQuery();
                    }
                }
                return _affectedRows;
            }
            catch (SqlException ex)
            {
                throw CreateFrameworkException(proName, parameters, ex);
            }
        }

        /// <summary>
        /// 存储过程数据分页
        /// </summary>
        /// <param name="tableName">数据表名</param>
        /// <param name="fields">要读取的字段</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="sqlWhere">查询条件</param>
        /// <param name="pageSize">每页显示多少条数据</param>
        /// <param name="pageIndex">当前页码</param>
        /// <returns>组元[DataTable,分页总数，记录总数]</returns>
        public Tuple<DataTable, int, int> StoreExecutePageQuery(string tableName, string fields, string orderField, string sqlWhere, int pageSize, int pageIndex)
        {
            DbParameter[] _paramter = new DbParameter[8];
            _paramter[0] = new SqlParameter("@tableName", SqlDbType.NVarChar, 100);
            _paramter[0].Value = tableName;

            _paramter[1] = new SqlParameter("@fields", SqlDbType.NVarChar, 100);
            _paramter[1].Value = fields;

            _paramter[2] = new SqlParameter("@orderField", SqlDbType.NVarChar, 100);
            _paramter[2].Value = orderField;

            _paramter[3] = new SqlParameter("@sqlWhere", SqlDbType.NVarChar, 100);
            _paramter[3].Value = sqlWhere;

            _paramter[4] = new SqlParameter("@pageSize", SqlDbType.Int);
            _paramter[4].Value = pageSize;

            _paramter[5] = new SqlParameter("@pageIndex", SqlDbType.Int);
            _paramter[5].Value = pageIndex;

            _paramter[6] = new SqlParameter("@totalPage", SqlDbType.Int);
            _paramter[6].Direction = ParameterDirection.Output;

            _paramter[7] = new SqlParameter("@return", SqlDbType.Int);
            _paramter[7].Direction = ParameterDirection.ReturnValue;

            int _totalPage = 0, _totalCount = 0;
            DataTable _table = StoreExecuteDataTable("proc_DataPage", _paramter, outparamter =>
             {
                 _totalPage = (int)outparamter[6].Value;
                 _totalCount = (int)outparamter[7].Value;
             });
            return Tuple.Create(_table, _totalPage, _totalCount);
        }

        private void CheckedSqlParamter(string sql)
        {
            ValidateHelper.Begin().NotNullOrEmpty(sql, "sql语句");
        }

        private void CheckedStoreNameParameter(string proName)
        {
            ValidateHelper.Begin().NotNullOrEmpty(proName, "存储过程名称");
        }

        private FrameworkException CreateFrameworkException(string sql, DbParameter[] parameters, SqlException ex)
        {
            string _sqlExMessage = ex.GetSqlExceptionMessage();
            ex.Data.Add("sqlServerConnectString", connectionString);
            ex.Data.Add("sql", sql);
            if (parameters != null)
            {
                foreach (DbParameter paramter in parameters)
                    ex.Data.Add(paramter.ParameterName, paramter.Value);
            }
            return new FrameworkException(_sqlExMessage, ex);
        }

        #endregion Methods
    }
}