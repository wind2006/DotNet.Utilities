using System.Linq;
using YanZhiwei.BookShop.Domain.Entities;

namespace YanZhiwei.BookShop.Domain.Abstract
{
    public interface IBookRepository
    {
        /*
         在这我们不防为数据的使用者（这里指Controller）提供一个IBookRepository接口，在这个接口中声明一个IQueryable<Book>类型的属性Books。这样，通过该接口使用依赖注入，使用者就可以拿到Books数据集合，而不用关心数据是如何得到的。
        */
        IQueryable<Book> Books { get; }
    }
}