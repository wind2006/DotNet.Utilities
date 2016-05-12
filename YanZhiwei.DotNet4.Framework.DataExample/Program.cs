using System;
using System.Collections.Generic;
using YanZhiwei.DotNet.Framework.Contract;
using YanZhiwei.DotNet4.Framework.Data;
using YanZhiwei.DotNet4.Framework.Data.Example.BLL;
using YanZhiwei.DotNet4.Framework.Data.Example.Contract;
using YanZhiwei.DotNet4.Framework.Data.Example.Services;

namespace YanZhiwei.DotNet4.Framework.Data.Example
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                NorthwindService _service = new NorthwindService();
                IEnumerable<Customer> _finded = _service.GetArticleList(new CustomerRequest() { CustomerID = "ALFKI" });
                ServiceCallContext.Current.Operater = new Operater()
                {
                    IP = "127.0.0.1",
                    Name = "test",
                    Time = DateTime.Now,
                    Token = Guid.NewGuid(),
                    UserId = 1
                };
                _service.SaveArticle(new Customer()
                {
                    CreateTime = DateTime.Now,
                    Address = "西藏北路",
                    City = "上海",
                    CompanyName = "LH",
                    ContactName = "YanZhiwei",
                    ContactTitle = "programmer",
                    Country = "China",
                    CustomerID = "YanZ88",
                    Phone = "(5) 555-4729"
                });
                //CustomerService _serviceHelper = new CustomerService(new EfRepository<Customer>(new PortalDb()), null);
                //CustomerService _serviceHelper = new CustomerService(new EfRepository<Customer>(new PortalDbEx()), null);
                //var _finded = _serviceHelper.GetCustomerById(1);
                //Console.WriteLine(_finded == null ? "Not Finded." : _finded.ID.ToString());
                //_serviceHelper.InsertCustomer(new Customer()
                //{
                //    CreateTime = DateTime.Now,
                //    Address = "西藏北路",
                //    City = "上海",
                //    CompanyName = "LH",
                //    ContactName = "YanZhiwei" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                //    ContactTitle = "programmer",
                //    Country = "China",
                //    CustomerID = "Yan10",
                //    Phone = "(5) 555-4729"
                //});
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}