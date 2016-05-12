using cdz360Tools.Contracts;
using cdz360Tools.Services;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Xml;

namespace cdz360Tools.Hosting
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Uri baseAddress = new Uri("http://127.0.0.1:8888/");
                using (ServiceHost host = new ServiceHost(typeof(Czd360DuplexServices), baseAddress))
                {
                    TimeSpan _tcpTimeOut = new TimeSpan(0, 1, 0);
                    NetTcpBinding tcpBinding = new NetTcpBinding();
                    tcpBinding.CloseTimeout = _tcpTimeOut;
                    tcpBinding.OpenTimeout = _tcpTimeOut;
                    tcpBinding.ReceiveTimeout = new TimeSpan(0, 10, 0);
                    tcpBinding.SendTimeout = _tcpTimeOut;
                    tcpBinding.TransactionFlow = false;
                    tcpBinding.TransferMode = TransferMode.Buffered;
                    tcpBinding.TransactionProtocol = TransactionProtocol.OleTransactions;
                    tcpBinding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
                    tcpBinding.ListenBacklog = 10;
                    tcpBinding.MaxBufferPoolSize = 524288000;
                    tcpBinding.MaxBufferSize = 65536000;
                    tcpBinding.MaxConnections = 100;
                    tcpBinding.MaxReceivedMessageSize = 65536000;

                    tcpBinding.ReaderQuotas = new XmlDictionaryReaderQuotas()
                    {
                        MaxArrayLength = 20971510,
                        MaxBytesPerRead = 4096,
                        MaxDepth = 32,
                        MaxNameTableCharCount = 16384,
                        MaxStringContentLength = 20971510
                    };
                    tcpBinding.ReliableSession = new OptionalReliableSession()
                    {
                        Enabled = true,
                        InactivityTimeout = new TimeSpan(0, 10, 0),
                        Ordered = true
                    };
                    //提供安全传输
                    tcpBinding.Security.Mode = SecurityMode.Transport;
                    tcpBinding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
                    tcpBinding.Security.Message.ClientCredentialType = MessageCredentialType.Windows;
                    //netTcp协议终结点
                    host.AddServiceEndpoint(typeof(ICzd360DuplexServices), tcpBinding, "net.tcp://127.0.0.1:9999/Czd360DuplexServicesTcp");

                    //元数据发布行为
                    ServiceMetadataBehavior _behavior = new ServiceMetadataBehavior();
                    //支持get请求
                    _behavior.HttpGetEnabled = true;
                    _behavior.HttpGetUrl = new Uri("http://127.0.0.1:8888/Czd360DuplexServices/metadata");
                    //设置到Host中
                    host.Description.Behaviors.Add(_behavior);

                    host.Opened += delegate
                    {
                        Console.WriteLine("Czd360DuplexServices已经启动，按任意键终止服务！");
                    };
                    host.Open();
                    Console.Read();
                }
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