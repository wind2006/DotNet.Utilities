using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YanZhiwei.DotNet2.Utilities.Core;
using YanZhiwei.DotNet2.Utilities.Models;

namespace YanZhiwei.DotNet2.UtilitiesExamples
{
    public class SocketDemo
    {
        private static readonly object syncObj = new object();
        private static void Main(string[] args)
        {
            try
            {
                SocketServer _server = new SocketServer("192.168.1.240", 9882);
                _server.PushServerMsgHanlderEvent += (sks) =>
                {
                    switch (sks.Code)
                    {
                        case SocketCode.StartSucceed:
                            Console.WriteLine("StartSucceed." + sks.Ip.ToString());
                            break;

                        case SocketCode.DataReceived:
                            byte[] _cc = sks.DataBuffer;
                            Console.WriteLine("Server DataReceived:" + _cc.Length + sks.Ip.ToString());
                            break;

                        case SocketCode.NewClientConnect:
                            Console.WriteLine("NewClientConnect" + sks.Ip.ToString());
                            break;

                        case SocketCode.ClientOffline:
                            Console.WriteLine("ClientOffline" + sks.Ip.ToString());
                            break;
                    }
                };
                _server.Start();

                SocketClient _client = new SocketClient("192.168.1.240", 9882);
                _client.PushClientMsgHanlderEvent += (sks) =>
                {
                    switch (sks.Code)
                    {
                        case SocketCode.ConnectSuccess:
                            Console.WriteLine("ConnectSuccess." + sks.Ip.ToString());
                            break;

                        case SocketCode.DataReceived:
                            byte[] _cc = sks.DataBuffer;
                            Console.WriteLine("Client DataReceived:" + _cc.Length + sks.Ip.ToString());
                            break;

                        case SocketCode.Disconnect:
                            Console.WriteLine("Disconnect" + sks.Ip.ToString());
                            break;

                        case SocketCode.ServerClose:
                            Console.WriteLine("ServerClose" + sks.Ip.ToString());
                            break;
                    }
                };
                _client.Connect();


                SocketClient _client2 = new SocketClient("192.168.1.240", 9882);
                _client2.PushClientMsgHanlderEvent += (sks) =>
                {
                    switch (sks.Code)
                    {
                        case SocketCode.ConnectSuccess:
                            Console.WriteLine("ConnectSuccess." + sks.Ip.ToString());
                            break;

                        case SocketCode.DataReceived:
                            byte[] _cc = sks.DataBuffer;
                            Console.WriteLine("Client DataReceived:" + _cc.Length + sks.Ip.ToString());
                            break;

                        case SocketCode.Disconnect:
                            Console.WriteLine("Disconnect" + sks.Ip.ToString());
                            break;

                        case SocketCode.ServerClose:
                            Console.WriteLine("ServerClose" + sks.Ip.ToString());
                            break;
                    }
                };
                _client2.Connect();

                _server.SendToAll("Hello World.");
                _client2.SendData("Hello World2.");
                _client.SendData("Hello World.");
                //_server.Stop();
                _client.Disconnect();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
