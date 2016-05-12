using System;
using System.Data;
using System.Data.Common;

namespace YanZhiwei.DotNet2.Interfaces.DataAccess
{
    public interface ISQLHelper
    {
        int ExecuteNonQuery(string sql, DbParameter[] parameters);
        IDataReader ExecuteReader(string sql, DbParameter[] parameters);
        DataTable ExecuteDataTable(string sql, DbParameter[] parameters);
        Object ExecuteScalar(string sql, DbParameter[] parameters);
    }
}
