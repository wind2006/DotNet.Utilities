namespace YanZhiwei.DotNet.Dapper.Utilities
{
    /// <summary>
    ///  Dapper Sql Server数据库操作帮助类
    /// </summary>
    /// 时间：2016-01-19 16:33
    /// 备注：
    public class DapperSqlServerHelper : DapperHelper
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlConnectString">连接字符串</param>
        /// 时间：2016-01-19 16:33
        /// 备注：
        public DapperSqlServerHelper(string sqlConnectString) : base(sqlConnectString)
        {
        }
    }
}