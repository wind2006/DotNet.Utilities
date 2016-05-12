using System;
using System.Messaging;
using System.ServiceModel;
using WCF.MSMQ.Serivce;

namespace WCF.MSMQ.Host
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                if (!MessageQueue.Exists(".\\Private$\\MSMQDemo"))
                {
                    MessageQueue.Create(".\\Private$\\MSMQDemo");
                }

                ServiceHost _host = new ServiceHost(typeof(Order));
                if (_host.State != CommunicationState.Opened)
                    _host.Open();
                Console.WriteLine("服务已经启动！");
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