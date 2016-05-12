using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using YanZhiwei.DotNet.Mvc.Learn.Models;

namespace YanZhiwei.DotNet.Mvc.Learn.DAL
{
    public class CompanyContext : DbContext
    {
        /*
         * 1.在<connectionStrings>元素内加入如下<add>元素。
  <configuration>
  <connectionStrings>
    <add name="CompanyContext" connectionString="Data Source=(LocalDb)\v11.0;Initial Catalog=Company;Integrated Security=SSPI;AttachDBFilename=|DataDirectory|\Company.mdf" providerName="System.Data.SqlClient" />
  </connectionStrings>
<configuration>
         *
         * 2.要在Web.config中声明数据库上下文。在<configuration>中找到<entityFramework>元素。在<entityFramework>元素中找到<contexts>元素。在<contexts>元素中写入如下<context>元素。
   <entityFramework>
    <contexts>
      <context type="YanZhiwei.DotNet.Mvc.Learn.DAL.CompanyContext, YanZhiwei.DotNet.Mvc.Learn">
      </context>
    </contexts>
   </entityFramework>
         * type="YanZhiwei.DotNet.Mvc.Learn.DAL.CompanyContext, YanZhiwei.DotNet.Mvc.Learn"中的YanZhiwei.DotNet.Mvc.Learn.DAL.CompanyContext表示这个CompanyContext的NameSpace和类名。逗号后面的YanZhiwei.DotNet.Mvc.Learn表示这个CompanyContext在YanZhiwei.DotNet.Mvc.Learn.dll中。
         * 这样我们就把CompanyContext添加到了Entity Framework的上下文中。
         */

        public CompanyContext()
            : base("CompanyContext")
        {
        }

        public DbSet<Worker> Workers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*
             * OnModelCreating，这个事件在我们使用的Code First方法在数据库中创建数据表时触发。'  modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();'的代码的作用是使我们创建出来的数据库表名字都是单数，不是复数。也就是说将来的数据库表名是Worker而不是Workers。
             */
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}