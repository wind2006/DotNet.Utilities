using Molin_CRM.DAL;
using Molin_CRM.Model;
using System;
using System.Collections.Generic;
using System.Data;
using YanZhiwei.DotNet2.Utilities.Common;

namespace Molin_CRM.Business
{
    public class CustomerBus
    {
        private static CustomerBus instance = null;

        public static CustomerBus Instance
        {
            get
            {
                if (instance == null)
                    instance = new CustomerBus();
                return instance;
            }
        }

        public bool Delete(Customer customer)
        {
            bool _result = false;
            try
            {
                string _sql = string.Format("Delete from Customer where ID='{0}'", customer.ID);
                _result = DataAccess.Instance.SQLHelper.ExecuteNonQuery(_sql, null) >= 1;
            }
            catch (Exception)
            {
                _result = false;
            }
            return _result;
        }

        public bool Add(Customer customer)
        {
            bool _result = false;
            try
            {
                string _sql = string.Format("insert into Customer values('" + customer.Name + "','" + customer.Phone + "','" + customer.Address + "','" + customer.Remark + "','" + Guid.NewGuid().ToString() + "')");
                _result = DataAccess.Instance.SQLHelper.ExecuteNonQuery(_sql, null) >= 1;
            }
            catch (Exception)
            {
                _result = false;
            }
            return _result;
        }

        public bool Update(Customer customer)
        {
            bool _result = false;
            try
            {
                string _sql = string.Format("update Customer set Name='{0}',Phone='{1}',Address='{2}',Remark='{3}' where ID='{4}'", customer.Name, customer.Phone, customer.Address, customer.Remark, customer.ID);
                int _actual = DataAccess.Instance.SQLHelper.ExecuteNonQuery(_sql, null);
                _result = _actual >= 1;
            }
            catch (Exception)
            {
                _result = false;
            }
            return _result;
        }

        public Customer Get(string name)
        {
            Customer _customer = null;
            try
            {
                string _sql = string.Format("select * from customer where Name='{0}'", name);
                using (IDataReader reader = DataAccess.Instance.SQLHelper.ExecuteReader(_sql, null))
                {
                    while (reader.Read())
                    {
                        if (_customer == null)
                            _customer = new Customer();
                        _customer.Name = reader["Name"].ToStringOrDefault("");
                        _customer.Phone = reader["Phone"].ToStringOrDefault("");
                        _customer.Address = reader["Address"].ToStringOrDefault("");
                        _customer.Remark = reader["Remark"].ToStringOrDefault("");
                        _customer.ID = reader["ID"].ToString();
                    }
                }
            }
            catch (Exception)
            {
                _customer = null;
            }
            return _customer;
        }

        public List<Customer> GetAll()
        {
            List<Customer> _customerList = new List<Customer>();
            try
            {
                string _sql = "select * from Customer";
                using (IDataReader reader = DataAccess.Instance.SQLHelper.ExecuteReader(_sql, null))
                {
                    while (reader.Read())
                    {
                        Customer _customer = new Customer();
                        _customer.Name = reader["Name"].ToStringOrDefault("");
                        _customer.Phone = reader["Phone"].ToStringOrDefault("");
                        _customer.Address = reader["Address"].ToStringOrDefault("");
                        _customer.Remark = reader["Remark"].ToStringOrDefault("");
                        _customer.ID = reader["ID"].ToString();
                        _customerList.Add(_customer);
                    }
                }
            }
            catch (Exception)
            {
                _customerList = null;
            }
            return _customerList;
        }
    }
}