
using System;
using System.ServiceModel;
using WCF.MSMQ.Serivce;

namespace WCF.MSMQ.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                ChannelFactory<IOrder> _factory = new ChannelFactory<IOrder>(new NetMsmqBinding(NetMsmqSecurityMode.None),
                                                        new EndpointAddress("net.msmq://localhost/private/Order"));

                var _client = _factory.CreateChannel();
                for (int i = 0; i < 100; i++)
                {
                    _client.Add(string.Format("{2}--MSMQ:{0}{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Environment.NewLine, i));
                }
                Console.WriteLine("完成.");
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