using cdz360Tools.Contracts;
using System;
using System.ServiceModel;

namespace cdz360Tools.Clients
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            InstanceContext _instance = new InstanceContext(new Czd360Callback());
            using (DuplexChannelFactory<ICzd360DuplexServices> channelFactory = new DuplexChannelFactory<ICzd360DuplexServices>(_instance, "NetTcpBinding_ICzd360DuplexServices"))
            {
                ICzd360DuplexServices _proxy = channelFactory.CreateChannel();
                using (_proxy as IDisposable)
                {
                    SvrCommMessage _result = new SvrCommMessage();
                    try
                    {
                        SvrRetMessage _svrResult = _proxy.CheckoutChannel(DateTime.Now);

                        _result.FailedMessage = _svrResult.Message;
                        _result.ExecuteState = _svrResult.ExcuResult == true ? 1 : 0;
                        Console.WriteLine(_svrResult.Message);

                        _svrResult = _proxy.ChargingPileOrder(1, 0, "18217594157", "066386776065", "018501600184", DateTime.Now.AddHours(1), DateTime.Now.AddHours(2));
                        Console.WriteLine(_svrResult.ExcuResult + ":" + _svrResult.Message);
                    }
                    catch (EndpointNotFoundException)
                    {
                        _result.CommunState = CommState.Failed;
                        _result.FailedMessage = CommMessage.NoServer;
                    }
                    catch (TimeoutException)
                    {
                        _result.CommunState = CommState.TimeOut;
                        _result.FailedMessage = CommMessage.TimeOuteMessahe;
                    }
                    catch (Exception ex)
                    {
                        _result.CommunState = CommState.Failed;
                        _result.FailedMessage = CommMessage.NoServer;
                        Console.WriteLine("Error:" + ex.Message);
                    }
                    finally
                    {
                        Console.ReadLine();
                    }
                }
            }
        }
    }
}