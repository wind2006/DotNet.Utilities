using System;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Text;

namespace YanZhiwei.DotNet.SQLite.Utilities
{
    /// <summary>
    /// SQLite帮助类
    /// </summary>
    public class SQLiteHelper
    {
        #region 构造函数以及变量

        private string connectionString = string.Empty;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbpath">db路径</param>
        public SQLiteHelper(string dbpath)
        {
            connectionString = string.Format(@"Data Source={0}", dbpath);
        }

        #endregion 构造函数以及变量

        #region 执行sql语句，返回影响行数

        /// <summary>
        /// 执行sql语句，返回影响行数
        ///<para>eg: string sql = "INSERT INTO Test(Name,TypeName)values(@Name,@TypeName)";   </para>
        ///<para>SQLiteParameter[] parameters = new SQLiteParameter[]{   new SQLiteParameter("@Name",c+i.ToString()),  new SQLiteParameter("@TypeName",c.ToString())} </para>
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>影响行数</returns>
        public int ExecuteNonQuery(string sql, SQLiteParameter[] parameters)
        {
            int _affectedRows = -1;
            using (SQLiteConnection sqlcon = new SQLiteConnection(connectionString))
            {
                sqlcon.Open();
                using (SQLiteCommand sqlcmd = new SQLiteCommand(sql, sqlcon))
                {
                    if (parameters != null)
                        sqlcmd.Parameters.AddRange(parameters);
                    _affectedRows = sqlcmd.ExecuteNonQuery();
                }
            }
            return _affectedRows;
        }

        /// <summary>
        /// 执行sql语句，返回影响行数 带事物
        /// <para>eg:DataAccess.Instance.SQLHelper.ExecuteNonQueryWithTrans(new string[2] { _addSell, _updateProduct }) </para>
        /// </summary>
        /// <param name="sqlList">SQL语句集合</param>
        /// <returns>影响行数</returns>
        public int ExecuteNonQueryWithTrans(string[] sqlList)
        {
            int _affectedRows = -1;
            using (SQLiteConnection sqlcon = new SQLiteConnection(connectionString))
            {
                sqlcon.Open();
                DbTransaction _sqlTrans = sqlcon.BeginTransaction();
                try
                {
                    _affectedRows = 0;
                    foreach (string sql in sqlList)
                    {
                        using (SQLiteCommand sqlcmd = new SQLiteCommand(sql, sqlcon))
                        {
                            _affectedRows += sqlcmd.ExecuteNonQuery();
                        }
                    }
                    _sqlTrans.Commit();
                }
                catch (Exception)
                {
                    _sqlTrans.Rollback();
                    _affectedRows = -1;
                }
            }
            return _affectedRows;
        }

        #endregion 执行sql语句，返回影响行数

        #region ExecuteReader

        /// <summary>
        /// ExecuteReader
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>IDataReader</returns>
        public IDataReader ExecuteReader(string sql, SQLiteParameter[] parameters)
        {
            SQLiteConnection _sqlcon = new SQLiteConnection(connectionString);
            using (SQLiteCommand _sqlcmd = new SQLiteCommand(sql, _sqlcon))
            {
                if (parameters != null)
                    _sqlcmd.Parameters.AddRange(parameters);
                _sqlcon.Open();
                return _sqlcmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }

        #endregion ExecuteReader

        #region ExecuteDataTable

        /// <summary>
        /// ExecuteDataTable
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>DataTable</returns>
        public DataTable ExecuteDataTable(string sql, SQLiteParameter[] parameters)
        {
            using (SQLiteConnection _sqlcon = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand _sqlcmd = new SQLiteCommand(sql, _sqlcon))
                {
                    if (parameters != null)
                        _sqlcmd.Parameters.AddRange(parameters);
                    using (SQLiteDataAdapter _sqldap = new SQLiteDataAdapter(_sqlcmd))
                    {
                        DataTable _dt = new DataTable();
                        _sqldap.Fill(_dt);
                        return _dt;
                    }
                }
            }
        }

        #endregion ExecuteDataTable

        #region ExecuteScalar

        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>Object</returns>
        public Object ExecuteScalar(string sql, SQLiteParameter[] parameters)
        {
            using (SQLiteConnection _sqlcon = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand _sqlcmd = new SQLiteCommand(sql, _sqlcon))
                {
                    if (parameters != null)
                        _sqlcmd.Parameters.AddRange(parameters);
                    _sqlcon.Open();
                    return _sqlcmd.ExecuteScalar(CommandBehavior.CloseConnection);
                }
            }
        }

        #endregion ExecuteScalar

        #region InsertRow

        /// <summary>
        /// Inserts the row.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns></returns>
        public int InsertRow(DataRow row)
        {
            int _affectedRows = -1;
            using (SQLiteConnection sqlcon = new SQLiteConnection(connectionString))
            {
                try
                {
                    sqlcon.Open();
                    using (SQLiteCommand sqlcmd = CreateInsertCommand(row))
                    {
                        sqlcmd.Connection = sqlcon;
                        sqlcmd.CommandType = CommandType.Text;
                        _affectedRows = sqlcmd.ExecuteNonQuery();
                    }
                }
                finally
                {
                    if (sqlcon.State != ConnectionState.Closed)
                    {
                        sqlcon.Close();
                        sqlcon.Dispose();
                    }
                }
            }
            return _affectedRows;
        }

        private string BuildInsertSQL(DataTable table)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO " + table.TableName + " (");
            StringBuilder values = new StringBuilder("VALUES (");
            bool bFirst = true;
            bool bIdentity = false;
            string identityType = null;

            foreach (DataColumn column in table.Columns)
            {
                if (column.AutoIncrement)
                {
                    bIdentity = true;

                    switch (column.DataType.Name)
                    {
                        case "Int16":
                            identityType = "smallint";
                            break;

                        case "SByte":
                            identityType = "tinyint";
                            break;

                        case "Int64":
                            identityType = "bigint";
                            break;

                        case "Decimal":
                            identityType = "decimal";
                            break;

                        default:
                            identityType = "int";
                            break;
                    }
                }
                else
                {
                    if (bFirst)
                        bFirst = false;
                    else
                    {
                        sql.Append(", ");
                        values.Append(", ");
                    }

                    sql.Append(column.ColumnName);
                    values.Append("@");
                    values.Append(column.ColumnName);
                }
            }
            sql.Append(") ");
            sql.Append(values.ToString());
            sql.Append(")");

            if (bIdentity)
            {
                sql.Append("; SELECT CAST(scope_identity() AS ");
                sql.Append(identityType);
                sql.Append(")");
            }

            return sql.ToString(); ;
        }

        private SQLiteCommand CreateInsertCommand(DataRow row)
        {
            DataTable table = row.Table;
            string sql = BuildInsertSQL(table);
            SQLiteCommand command = new SQLiteCommand(sql);
            command.CommandType = System.Data.CommandType.Text;

            foreach (DataColumn column in table.Columns)
            {
                if (!column.AutoIncrement)
                {
                    string parameterName = "@" + column.ColumnName;
                    InsertParameter(command, parameterName, column.ColumnName, row[column.ColumnName]);
                }
            }
            return command;
        }

        private void InsertParameter(SQLiteCommand command,
                                         string parameterName,
                                          string sourceColumn,
                                          object value)
        {
            SQLiteParameter parameter = new SQLiteParameter(parameterName, value);

            parameter.Direction = ParameterDirection.Input;
            parameter.ParameterName = parameterName;
            parameter.SourceColumn = sourceColumn;
            parameter.SourceVersion = DataRowVersion.Current;

            command.Parameters.Add(parameter);
        }

        #endregion InsertRow
    }
}