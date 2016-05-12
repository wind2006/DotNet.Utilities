using System.Data.Entity;
using YanZhiwei.DotNet4.Framework.Data;
using YanZhiwei.DotNet4.Framework.Data.Example.Contract;
using YanZhiwei.Framework.DataAc.Example;

namespace YanZhiwei.DotNet4.Framework.Data.Example
{
    public class PortalDbEx : EfContextBase
    {
        public PortalDbEx() : base(@"Data Source=YANZHIWEI-IT-PC\SQLEXPRESS;Initial Catalog=Northwind;Persist Security Info=True;User ID=sa;Password=sasa")
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