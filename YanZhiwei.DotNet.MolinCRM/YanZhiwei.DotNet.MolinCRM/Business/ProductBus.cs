using Molin_CRM.DAL;
using Molin_CRM.Model;
using System;
using System.Collections.Generic;
using System.Data;
using YanZhiwei.DotNet2.Utilities.Common;

namespace Molin_CRM.Business
{
    public class ProductBus
    {
        private static ProductBus instance = null;

        public static ProductBus Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductBus();
                }
                return instance;
            }
        }

        /// <summary>
        /// 根据Name获取产品信息
        /// </summary>
        public Product Get(string name)
        {
            Product _product = null;
            try
            {
                string _sql = string.Format("select * from Product where Name='{0}'", name);
                using (IDataReader reader = DataAccess.Instance.SQLHelper.ExecuteReader(_sql, null))
                {
                    while (reader.Read())
                    {
                        if (_product == null)
                            _product = new Product();
                        _product.Name = reader["Name"].ToStringOrDefault("--");
                        _product.Number = reader["Number"].ToIntOrDefault(0);
                        _product.OptTime = reader["OptTime"].ToDateOrDefault(default(DateTime));
                        _product.Price = reader["Price"].ToDecimalOrDefault(0);
                    }
                }
            }
            catch (Exception)
            {
                _product = null;
            }
            return _product;
        }

        /// <summary>
        /// 更新产品信息
        /// </summary>
        /// <param name="name">产品名称</param>
        /// <param name="number">数量</param>
        /// <param name="price">单价</param>
        /// <returns>是否更新成功</returns>
        public bool Update(string name, int number, decimal price)
        {
            bool _result = false;
            try
            {
                string _sql = string.Format("update Product set Number={0},Price={1} where name='{2}'", number, price, name);
                int _actual = DataAccess.Instance.SQLHelper.ExecuteNonQuery(_sql, null);
                _result = _actual >= 1;
            }
            catch (Exception)
            {
                _result = false;
            }
            return _result;
        }

        /// <summary>
        /// 添加产品信息
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public bool Add(Product product)
        {
            bool _result = false;
            try
            {
                if (product != null)
                {
                    string _sql = string.Format("insert into Product values('" + product.Name + "'," + product.Number + "," + product.Price + ",'" + DateTime.Now.ToString("s") + "')");
                    int _actual = DataAccess.Instance.SQLHelper.ExecuteNonQuery(_sql, null);
                    _result = _actual >= 1;
                }
            }
            catch (Exception)
            {
                _result = false;
            }
            return _result;
        }

        /// <summary>
        /// 查询所有产品信息
        /// </summary>
        /// <returns></returns>
        public List<Product> GetAllProduct(string productName)
        {
            List<Product> _productList = new List<Product>();
            try
            {
                string _sql = string.IsNullOrWhiteSpace(productName) == true ? "select * from Product" :
                    string.Format("select * from Product where Name like '%{0}%'", productName);
                using (IDataReader reader = DataAccess.Instance.SQLHelper.ExecuteReader(_sql, null))
                {
                    while (reader.Read())
                    {
                        Product _product = new Product();
                        _product.Name = reader["Name"].ToStringOrDefault("--");
                        _product.Number = reader["Number"].ToIntOrDefault(0);
                        _product.OptTime = reader["OptTime"].ToDateOrDefault(default(DateTime));
                        _product.Price = reader["Price"].ToDecimalOrDefault(0);
                        _productList.Add(_product);
                    }
                }
            }
            catch (Exception)
            {
                _productList = null;
            }
            return _productList;
        }
    }
}