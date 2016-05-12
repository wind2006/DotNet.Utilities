using System;
using YanZhiwei.DotNet.Framework.Contract;
using YanZhiwei.DotNet.Framework.Data;

namespace YanZhiwei.DotNet.Framework.DataExample
{
    public class PubDbContext : DbContextBase
    {
        public PubDbContext() : base(@"Data Source=YANZHIWEI-IT-PC\SQLEXPRESS;Initial Catalog=pubs;Persist Security Info=True;User ID=sa;Password=sasa", new SqlAuditable())
        {

        }

        public Employee employee { get; set; }
    }

    public class SqlAuditable : ISqlAuditable
    {
        public void WriteLog(int userId, string tableName, string sql, DateTime optTime)
        {
            Console.WriteLine(string.Format("userId:{0}\r\ntableName:{1}\r\nsql:{2}\r\noptTime:{3}\r\n===========================\r\n", userId, tableName, sql, optTime));
        }
    }
}