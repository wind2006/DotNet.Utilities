namespace YanZhiwei.DotNet2.Utilities.Core
{
    using YanZhiwei.DotNet2.Utilities.Enums;

    /// <summary>
    /// Sql Server数据库分页脚本
    /// </summary>
    /// 创建时间:2015-05-22 11:48
    /// 备注说明:<c>null</c>
    public class SqlServerPageScript
    {
        #region Methods

        /// <summary>
        /// 利用[ROW_NUMBER() over]分页，生成sql语句
        /// </summary>
        /// <param name="tableName">表名称『eg:Orders』</param>
        /// <param name="columns">需要显示列『*:所有列；或者：eg:OrderID,OrderDate,ShipName,ShipCountry』</param>
        /// <param name="orderColumn">依据排序的列『eg:OrderID』</param>
        /// <param name="sqlWhere">筛选条件『eg:Order=1』</param>
        /// <param name="orderType">升序降序『1：desc;其他:asc』</param>
        /// <param name="pSize">每页页数『需大于零』</param>
        /// <param name="pIndex">页数『从壹开始算』</param>
        /// <returns>生成分页sql脚本</returns>
        public static string CreateSqlByRowNumber(string tableName, string columns, string orderColumn, string sqlWhere, OrderBy orderType, int pSize, int pIndex)
        {
            int _pageStart = pSize * (pIndex - 1) + 1;
            int _pageEnd = pSize * pIndex + 1;

            string _sql = string.Format("select * from  (select (ROW_NUMBER() over(order by {2} {3})) as ROWNUMBER,{1}  from {0})as tp where ROWNUMBER >= {4} and ROWNUMBER< {5} ",
                                         tableName,
                                         columns,
                                         orderColumn,
                                         orderType == OrderBy.Desc ? "desc" : "asc",
                                         _pageStart,
                                         _pageEnd);

            _sql = CreateQueryWhereSql(_sql, sqlWhere);
            _sql = CreateQueryTotalSql(_sql, tableName);
            return _sql;
        }

        /// <summary>
        /// 利用[Top Max]分页，生成sql语句
        /// </summary>
        /// <param name="tableName">表名称『eg:Orders』</param>
        /// <param name="columns">需要显示列『*:所有列；或者：eg:OrderID,OrderDate,ShipName,ShipCountry』</param>
        /// <param name="orderColumn">依据排序的列『eg:OrderID』</param>
        /// <param name="sqlWhere">筛选条件『eg:Order=1』</param>
        /// <param name="orderType">升序降序『1：desc;其他:asc』</param>
        /// <param name="pSize">每页页数『需大于零』</param>
        /// <param name="pIndex">页数『从壹开始算』</param>
        /// <returns>生成分页sql脚本</returns>
        public static string CreateSqlByTopMax(string tableName, string columns, string orderColumn, string sqlWhere, OrderBy orderType, int pSize, int pIndex)
        {
            /*
             *eg:
             *1=>select top 30 orderID from Orders order by orderID asc
             *2=>(select max (orderID) from (select top 30 orderID from Orders order by orderID asc) as T) //查询前一页数据
             *3=> select top 15 OrderID,OrderDate,ShipName,ShipCountry from Orders where orderID>
                  ISNULL((select max (orderID) from (select top 30 orderID from Orders order by orderID asc) as T),0)
                  order by orderID asc
             */
            string _sql = string.Format("select top {4} {1} from {0} where {2}> ISNULL((select max ({2}) from (select top {5} {2} from {0} order by {2} {3}) as T),0) order by {2} {3}",
                                         tableName,
                                         columns,
                                         orderColumn,
                                         orderType == OrderBy.Desc ? "desc" : "asc",
                                         pSize,
                                         (pIndex - 1) * pSize);
            _sql = CreateQueryWhereSql(_sql, sqlWhere);
            _sql = CreateQueryTotalSql(_sql, tableName);
            return _sql;
        }

        /// <summary>
        /// 利用[Top NotIn]分页，生成sql语句
        /// </summary>
        /// <param name="tableName">表名称『eg:Orders』</param>
        /// <param name="columns">需要显示列『*:所有列；或者：eg:OrderID,OrderDate,ShipName,ShipCountry』</param>
        /// <param name="orderColumn">依据排序的列『eg:OrderID』</param>
        /// <param name="sqlWhere">筛选条件『eg:Order=1』</param>
        /// <param name="orderType">升序降序『1：desc;其他:asc』</param>
        /// <param name="pSize">每页页数『需大于零』</param>
        /// <param name="pIndex">页数『从壹开始算』</param>
        /// <returns>生成分页sql脚本</returns>
        public static string CreateSqlByTopNotIn(string tableName, string columns, string orderColumn, string sqlWhere, OrderBy orderType, int pSize, int pIndex)
        {
            /*
             *eg:
             *1=>SELECT orderID FROM Orders ORDER BY orderID
             *2=>SELECT TOP 20 orderID FROM Orders ORDER BY orderID //查询前一页数据
             *3=> SELECT TOP 10 * FROM Orders WHERE (orderID NOT IN (SELECT TOP 20 orderID FROM Orders ORDER BY orderID)) ORDER BY orderID //在所有数据中，截去掉上一页数据(not in)，然后select top 10 即当前页数据
             */
            string _sql = string.Format("SELECT TOP {4} {1} FROM {0} WHERE ({2} NOT IN (SELECT TOP {5} {2} FROM {0} ORDER BY {2} {3})) ORDER BY {2} {3}",
                                         tableName,
                                         columns,
                                         orderColumn,
                                         orderType == OrderBy.Desc ? "desc" : "asc",
                                         pSize,
                                         (pIndex - 1) * pSize);
            _sql = CreateQueryWhereSql(_sql, sqlWhere);
            _sql = CreateQueryTotalSql(_sql, tableName);
            return _sql;
        }

        /// <summary>
        /// 查询记录总数
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="tableName">表名</param>
        /// <returns>Sql</returns>
        /// 时间：2016-01-05 13:11
        /// 备注：
        private static string CreateQueryTotalSql(string sql, string tableName)
        {
            string _sqltotalCount = string.Format("select count(*) from {0}", tableName);
            return string.Format("{0};{1}", sql, _sqltotalCount);
        }

        /// <summary>
        /// 添加筛选条件
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="sqlWhere">筛选条件</param>
        /// <returns>sql</returns>
        /// 时间：2016-01-06 15:19
        /// 备注：
        private static string CreateQueryWhereSql(string sql, string sqlWhere)
        {
            if (!string.IsNullOrEmpty(sqlWhere))
                sql = string.Format("{0} and ( {1} )", sql, sqlWhere);
            return sql;
        }

        #endregion Methods
    }
}