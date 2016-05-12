namespace YanZhiwei.DotNet3._5.Utilities.Common
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Description;
    using System.Xml;

    using YanZhiwei.DotNet2.Utilities.WebForm.Core;

    /// <summary>
    /// Wcf帮助类
    /// </summary>
    public class WcfServiceProxy
    {
        #region Fields

        private const int maxReceivedMessageSize = 2147483647;

        private static TimeSpan timeout = TimeSpan.FromMinutes(10);

        #endregion Fields

        #region Methods

        /// <summary>
        /// 动态创建Wcf客户端代理实例
        /// </summary>
        /// <typeparam name="T">Contract/接口</typeparam>
        /// <param name="uri">Wcf服务地址</param>
        /// <returns>代理实例</returns>
        public static T CreateServiceProxy<T>(string uri)
        {
            string _key = string.Format("{0} - {1}", typeof(T), uri);

            if (CacheManger.Get(_key) == null)
            {
                BasicHttpBinding _binding = new BasicHttpBinding();
                _binding.MaxReceivedMessageSize = maxReceivedMessageSize;
                _binding.ReaderQuotas = new XmlDictionaryReaderQuotas();
                _binding.ReaderQuotas.MaxStringContentLength = maxReceivedMessageSize;
                _binding.ReaderQuotas.MaxArrayLength = maxReceivedMessageSize;
                _binding.ReaderQuotas.MaxBytesPerRead = maxReceivedMessageSize;
                _binding.OpenTimeout = timeout;
                _binding.ReceiveTimeout = timeout;
                _binding.SendTimeout = timeout;

                ChannelFactory<T> _chan = new ChannelFactory<T>(_binding, new EndpointAddress(uri));

                foreach (OperationDescription op in _chan.Endpoint.Contract.Operations)
                {
                    var dataContractBehavior = op.Behaviors.Find<DataContractSerializerOperationBehavior>();
                    if (dataContractBehavior != null)
                        dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                }

                _chan.Open();

                var service = _chan.CreateChannel();
                CacheManger.Set(_key, service);

                return service;
            }
            else
            {
                return (T)CacheManger.Get(_key);
            }
        }

        #endregion Methods
    }
}