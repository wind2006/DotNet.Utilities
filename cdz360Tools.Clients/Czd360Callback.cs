using System;

namespace cdz360Tools.Clients
{
    //SvcUtil /out:d:\SvcOutPut\Cdz360ClientProxy.cs /config:d:\SvcOutPut\Cdz360ClientApp.config http://127.0.0.1:8888
    public class Czd360Callback : ICzd360DuplexServicesCallback
    {
        public void ChargingPileOrderMesage(string deviceSeqNo, short gunSqNo, string orderSeqNo, bool orderStatus)
        {
            Console.WriteLine(string.Format("{0}\r\n{1}\r\n{2}\r\n{3}\r\n", deviceSeqNo, gunSqNo, orderSeqNo, orderStatus));
        }

        public void CheckoutChannelMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}