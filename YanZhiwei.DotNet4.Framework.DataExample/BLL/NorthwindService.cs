using System.Collections.Generic;
using System.Linq;
using YanZhiwei.DotNet2.Utilities.Core;
using YanZhiwei.DotNet3._5.Utilities.Common;
using YanZhiwei.DotNet4.Framework.Data.Example.Contract;
using YanZhiwei.Framework.DataAc.Example;

namespace YanZhiwei.DotNet4.Framework.Data.Example.BLL
{
    public class NorthwindService : INorthwindService
    {
        public void DeleteArticle(List<int> ids)
        {
            using (var dbContext = new NorthwindDbContext())
            {
                dbContext.Customers.Where(u => ids.Contains(u.ID)).ToList()
                    .ForEach
                    (a =>
                    {
                        dbContext.Customers.Remove(a);
                    });
                dbContext.SaveChanges();
            }
        }

        public Customer GetArticle(int id)
        {
            using (var dbContext = new NorthwindDbContext())
            {
                return dbContext.Customers.FirstOrDefault(a => a.ID == id);
            }
        }

        public IEnumerable<Customer> GetArticleList(CustomerRequest request = null)
        {
            request = request ?? new CustomerRequest();
            using (var dbContext = new NorthwindDbContext())
            {
                IQueryable<Customer> articles = dbContext.Customers;
                if (!string.IsNullOrEmpty(request.ContactName))
                    articles = articles.Where(u => u.ContactName.Contains(request.ContactName));
                if (!string.IsNullOrEmpty(request.CustomerID))
                    articles = articles.Where(u => u.CustomerID == request.CustomerID);

                return articles.OrderByDescending(u => u.ID).ToPagedList(request.PageIndex, request.PageSize);
            }
        }

        public void SaveArticle(Customer customer)
        {
            using (var dbContext = new NorthwindDbContext())
            {
                if (customer.ID > 0)
                {
                    if (dbContext.Customers.Any(c => c.CustomerID == customer.CustomerID))
                        throw new BusinessException("CustomerID", "已存在此编号的客户！");

                    dbContext.Update<Customer>(customer);
                }
                else
                {
                    if (dbContext.Customers.Any(c => c.CustomerID == customer.CustomerID))
                        throw new BusinessException("CustomerID", "已存在此编号的客户！");

                    dbContext.Insert<Customer>(customer);
                }
            }
        }
    }
}