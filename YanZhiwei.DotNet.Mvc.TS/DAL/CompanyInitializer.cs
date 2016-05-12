using System.Collections.Generic;
using System.Data.Entity;
using YanZhiwei.DotNet.Mvc.Learn.Models;

namespace YanZhiwei.DotNet.Mvc.Learn.DAL
{
    public class CompanyInitializer : DropCreateDatabaseAlways<CompanyContext>//DropCreateDatabaseAlways:每次程序运行时都会删除并重新创建数据库，这样方便我们测试。
    {
        /*
         * 1.要在Web.config文件中配置这个初始化器。在刚才配置过的<context>中写入如下所示的databaseInitializer即可。
    <contexts>
      <context type="YanZhiwei.DotNet.Mvc.Learn.DAL.CompanyContext, YanZhiwei.DotNet.Mvc.Learn">
        <databaseInitializer type="YanZhiwei.DotNet.Mvc.Learn.DAL.CompanyInitializer, YanZhiwei.DotNet.Mvc.Learn">
        </databaseInitializer>
      </context>
    </contexts>
         */

        protected override void Seed(CompanyContext context)
        {
            var _person = new List<Worker>
            {
                new Worker{FirstName="Andy",LastName="George",Gender = Sex.Male},
                new Worker{FirstName="Laura",LastName="Smith",Gender = Sex.Female},
                new Worker{FirstName="Jason",LastName="Black",Gender = Sex.Male},
                new Worker{FirstName="Linda",LastName="Queen",Gender = Sex.Female},
                new Worker{FirstName="James",LastName="Brown", Gender = Sex.Male}
             };
            _person.ForEach(s => context.Workers.Add(s));
            context.SaveChanges();
        }
    }
}