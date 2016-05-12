using System;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using WCF.TransaccationScope.Model;
using WCF.TransaccationScope.Service;
using YanZhiwei.DotNet2.Utilities.Common;

namespace WCF.TransaccationScope.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Service1”。
    public class Seller : ISeller
    {
        private SqlServerHelper SqlHelper = new SqlServerHelper(@"Server=YANZHIWEI-IT-PC\SQLEXPRESS;database=WCFTS;pwd=sasa;uid=sa;");

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public bool Add(Shop shop, User user)
        {
            string _userId = string.Empty;
            if (Add(user, out _userId))
            {
                throw new Exception("ccc");
                return Add(shop, _userId);
            }
            return false;
        }

        public bool Add(Shop shop, string userId)
        {
            string _sql = @"INSERT INTO [WCFTS].[dbo].[Shop]
                         ([UserID]
                         ,[ShopName]
                         ,[ShopUrl])
                         VALUES
                         (@UserID
                         ,@ShopName
                         ,@ShopUrl)";
            SqlParameter[] _paramters = new SqlParameter[3];
            _paramters[0] = new SqlParameter("@UserID", userId);
            _paramters[1] = new SqlParameter("@ShopName", "TestShop");
            _paramters[2] = new SqlParameter("@ShopUrl", "http://www.yanzhiwe.com");
            return SqlHelper.ExecuteNonQuery(_sql, _paramters) > 0;
        }

        public bool Add(User user, out string userId)
        {
            string _sql = @"INSERT INTO [WCFTS].[dbo].[User]
                          ([UserName]
                          ,[Password])
                          VALUES
                          (@UserName
                          ,@Password);select @@identity as Id";
            SqlParameter[] _paramters = new SqlParameter[2];
            _paramters[0] = new SqlParameter("@UserName", "YanZhiwei");
            _paramters[1] = new SqlParameter("@Password", "12345");
            userId = "1";
            using (IDataReader reader = SqlHelper.ExecuteReader(_sql, _paramters))
            {
                while (reader.Read())
                {
                    userId = reader["Id"].ToStringOrDefault("");
                }
            }
            return !string.IsNullOrEmpty(userId);
        }
    }
}