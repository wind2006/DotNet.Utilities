using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using WCF.TransaccationScope.Service;

namespace WCF.TransaccationScope.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var user = new Model.User()
                {
                    UserName = "huangxincheng520",
                    Password = "i can fly"
                };
                var shop = new Model.Shop()
                {
                    ShopName = "shopex",
                    ShopUrl = "http://www.shopex.cn"
                };
                ChannelFactory<ISeller> _factory = new ChannelFactory<ISeller>(new WSHttpBinding(),
                                     new EndpointAddress("http://localhost:8734/Seller"));
                var _client = _factory.CreateChannel();
                Console.WriteLine(_client.Add(shop, user) == true ? "成功" : "失败");
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
