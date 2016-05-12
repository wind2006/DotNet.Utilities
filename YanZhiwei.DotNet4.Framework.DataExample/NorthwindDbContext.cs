using System.Data.Entity;
using YanZhiwei.DotNet.Core.Log.Examples;
using YanZhiwei.DotNet4.Framework.Data;
using YanZhiwei.DotNet4.Framework.Data.Example.Contract;

namespace YanZhiwei.Framework.DataAc.Example
{
    public class NorthwindDbContext : DbContextBase
    {
        public NorthwindDbContext() : base(@"Data Source=YANZHIWEI-IT-PC\SQLEXPRESS;Initial Catalog=Northwind;Persist Security Info=True;User ID=sa;Password=sasa", new LogDbContext())
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<NorthwindDbContext>(null);//从不创建数据库

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Customer> Customers { get; set; }
    }
}