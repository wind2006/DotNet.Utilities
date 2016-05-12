using System.Data.Entity;
using YanZhiwei.BookShop.Domain.Entities;

namespace YanZhiwei.BookShop.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        /*
        ORM(Object Relation Mapping)工具，顾名思义，它的角色就是为了解决“关系”和“面向对象”之间的“失配”，它可以使得开发人员不用过多关心持久层而可以花更多的时间专注于业务。

        Entity Framework(EF)是微软以ADO.NET为基础所发展出来的ORM解决方案，以Entity Data Model(EDM) 为主。EF利用了抽象化数据结构的方式，将每个数据库对象都转换成应用程序中的类对象(Entity)，而数据字段都转换为属性 (Property)，关系则转换为结合属性 (Association)，让数据库的 E/R 模型完全的转成对象模型，如此让开发人员就能用熟悉的面向对象编程语言来调用访问。EF 4.0 以后支持Database First、Model First、Code First三种生成模式，Code First模式用的人比较多。

        使用EF Code First第一步就是创建一个继承自System.Data.Entity.DbContext的类，这个类将为数据库中的每个表定义一个属性，属性的名称代表数据库中的表名。DbSet作为返回类型，它是用于生成CRUD(Create、Read、Update和Delete)操作的装置，映射数据库表的行。

        EF从数据库中读取整个Books表的数据到内存，然后返回给调用者(上面代码中的repository.Books)用Linq语句过滤用户想要的前10条数据，如果Books表中有几百万条数据，那内存岂不是完蛋了，EF不会这么傻吧？EF会不会根据Linq查询语句智能地生成SQL文本再到数据库中去查询数据呢？这里就要讲讲IQueryable和IEnumerable了。

        为什么用IQueryable而不用IEnumerable作为返回类型？答案是：使用IQueryable，EF会根据调用者的Linq表达式先生成相应的SQL查询语句，然后到数据库中执行查询，查询出来的数据即是用户想要的数据；而使用IEnumerable，Linq表达式的过滤、排序等操作都是在内存中发生的，即EF会先从数据库中把整个表的数据查询出来放在内存中，然后由调用者使用Linq语句进行过滤、排序等操作。是不是这样呢？我们来监视一下两种情况EF生成的SQL语句就知道了。

        IQueryable虽然可以很智能地根据Linq表达式生成相应的SQL语句，但毕竟有一个分析Linq表达式的过程，相对来说性能比IEnumerable要差。那么我们什么时候用IEnumerable，什么时候用IQueryable呢？我想，对于少量的数据（比如从数据库中读取应用程序相关的系统信息）和不需要对数据进行过滤操作的情况，用IEnumerable比较适合；对于数据量较大需要对数据进行过滤（比如分页查询）的情况，则用IQueryable比较合适。
        */

        public EFDbContext() : base("EFDbContext")
        {
        }

        public DbSet<Book> Books
        {
            get; set;
        }
    }
}