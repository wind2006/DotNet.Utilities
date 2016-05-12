using System;
using System.IO;

namespace WCF.MSMQ.Serivce
{
    public class Order : IOrder
    {
        public void Add(string order)
        {
            try
            {
                File.AppendAllText("D://MSMQDemo.txt", order);
            }
            catch (Exception)
            {
            }
        }
    }
}