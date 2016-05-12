using System.Collections.Generic;
using System.Linq;
using YanZhiwei.BookShop.Domain.Abstract;
using YanZhiwei.BookShop.Domain.Entities;

namespace YanZhiwei.BookShop.Domain.Concrete
{
    public class BookRepository : IBookRepository
    {
        /*
         在MVC中我们一般会用仓储模式（Repository Pattern）把数据相关的逻辑和领域实体模型分离，这样对于使用者来说，通过调用仓储对象，使用者可以直接拿到自己想要的数据，而完全不必关心数据具体是如何来的。我们可以把仓储比喻成一个超市，超市已经为消费者供备好了商品，消费者只管去超市选购自己需要的商品，而完全不必关心这些商品是从哪些供应商怎么样运输到超市的。但对于仓储本身，必须要实现读取数据的“渠道”。
        */
        private EFDbContext context = new EFDbContext();
        public IQueryable<Book> Books
        {
            get
            {
                return context.Books;
                //return GetBooks().AsQueryable();
            }
        }

        private static List<Book> GetBooks()
        {
            //为了演示，这里手工造一些数据，后面会介绍使用EF从数据库中读取。
            List<Book> books = new List<Book>{
            new Book { ID = 1, Title = "ASP.NET MVC 4 编程", Price = 52},
            new Book { ID = 2, Title = "CLR Via C#", Price = 46},
            new Book { ID = 3, Title = "平凡的世界", Price = 37}
            };
            return books;
        }
    }
}