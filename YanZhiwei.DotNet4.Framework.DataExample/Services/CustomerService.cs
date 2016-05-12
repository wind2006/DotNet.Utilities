using System;
using System.Collections.Generic;
using System.Linq;
using YanZhiwei.DotNet4.Framework.Data.Example.Contract;

namespace YanZhiwei.DotNet4.Framework.Data.Example.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _useRepository;
        private readonly ICacheManager _cacheManager;

        public CustomerService(IRepository<Customer> useRepository, ICacheManager cacheManager)
        {
            _useRepository = useRepository;
            _cacheManager = cacheManager;
        }

        public void DeleteCustomer(Customer user)
        {
            if (user == null) throw new ArgumentNullException("user");

            UpdateCustomer(user);
        }

        public Customer GetCustomerById(int userId)
        {
            if (userId == 0)
                return null;
            return _useRepository.GetById(userId);
        }

        public IList<Customer> GetCustomerByIds(int[] userIds)
        {
            if (userIds == null || userIds.Length == 0)
                return new List<Customer>();

            var quey = _useRepository.Table.Where(n => userIds.Contains(n.ID));
            var users = quey.ToList();
            return userIds.Select(id => users.Find(x => x.ID == id)).Where(user => user != null).ToList();
        }

        public void InsertCustomer(Customer user)
        {
            if (user == null) throw new ArgumentNullException("user");

            _useRepository.Insert(user);
        }

        public void UpdateCustomer(Customer user)
        {
            throw new NotImplementedException(); if (user == null)
                throw new ArgumentNullException("Customer");
            _useRepository.Update(user);
            //还触发了事件通知！
            //_eventPublisher.EntityUpdated(customer);
        }
    }
}