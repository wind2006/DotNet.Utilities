using System;
using System.ServiceModel;
using WCF.TransaccationScope.Service;

namespace WCF.TransaccationScope.Host
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                ServiceHost _host = new ServiceHost(typeof(Seller));
                _host.Open();
                Console.WriteLine("WCF 服务已经开启！");
                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.Read();
            }
        }
    }
}