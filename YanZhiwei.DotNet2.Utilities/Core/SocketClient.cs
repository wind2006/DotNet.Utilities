namespace YanZhiwei.DotNet2.Utilities.Core
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;

    using YanZhiwei.DotNet2.Utilities.Models;

    /// <summary>
    /// Socket Clinet
    /// </summary>
    public class SocketClient
    {
        #region Fields

        /// <summary>
        /// 客户端
        /// </summary>
        private TcpClient client;

        /// <summary>
        /// 当前连接服务端地址
        /// </summary>
        private IPAddress ipAddress;

        /// <summary>
        /// 服务端IP+端口
        /// </summary>
        private IPEndPoint ipEndPoint;

        /// <summary>
        /// 是否关闭.(窗体关闭时关闭代码)
        /// </summary>
        private bool isClose = false;

        /// <summary>
        /// 当前连接服务端端口号
        /// </summary>
        private int portNumber;

        /// <summary>
        /// 接收缓冲区
        /// </summary>
        private byte[] recBuffer = new byte[1 * 1024 * 1024];

        /// <summary>
        /// 发送缓冲区
        /// </summary>
        private byte[] sendBuffer = new byte[1 * 1024 * 1024];

        /// <summary>
        /// 当前管理对象
        /// </summary>
        private SocketObj sk;

        /// <summary>
        /// 发送与接收使用的流
        /// </summary>
        private NetworkStream stream;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ip">The ipaddress.</param>
        /// <param name="port">The port.</param>
        public SocketClient(string ip, int port)
        {
            ipAddress = IPAddress.Parse(ip);
            portNumber = port;
            ipEndPoint = new IPEndPoint(ipAddress, portNumber);
            client = new TcpClient();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ip">The ipaddress.</param>
        /// <param name="port">The port.</param>
        public SocketClient(IPAddress ip, int port)
        {
            ipAddress = ip;
            portNumber = port;
            ipEndPoint = new IPEndPoint(ipAddress, portNumber);
            client = new TcpClient();
        }

        #endregion Constructors

        #region Delegates

        /// <summary>
        /// 消息推送委托
        /// </summary>
        /// <param name="sockets">The sockets.</param>
        public delegate void PushClientMsgHanlder(SocketData sockets);

        #endregion Delegates

        #region Events

        /// <summary>
        /// 消息推送事件
        /// </summary>
        public event PushClientMsgHanlder PushClientMsgHanlderEvent;

        #endregion Events

        #region Methods

        /// <summary>
        /// 连接到Server
        /// </summary>
        public void Connect()
        {
            try
            {
                client.Connect(ipEndPoint);
                stream = new NetworkStream(client.Client, true);
                sk = new SocketObj(ipEndPoint, client, stream);
                sk.SkStream.BeginRead(recBuffer, 0, recBuffer.Length, new AsyncCallback(EndReader), sk);
                PusbClientMessage(SocketCode.ConnectSuccess, null, null, ipEndPoint, null);
            }
            catch (Exception ex)
            {
                PusbClientMessage(SocketCode.ConnectError, null, ex, ipEndPoint, null);
            }
        }

        /// <summary>
        /// 端口与Server的连接
        /// </summary>
        public void Disconnect()
        {
            SocketObj _sks = new SocketObj();
            if (client != null)
            {
                client.Client.Shutdown(SocketShutdown.Both);
                Thread.Sleep(10);
                client.Close();
                isClose = true;
                client = null;
                PusbClientMessage(SocketCode.Disconnect, null, null, ipEndPoint, null);
            }
            else
            {
                PusbClientMessage(SocketCode.Uninitialized, null, null, ipEndPoint, null);
            }
        }

        /// <summary>
        /// 重连上端.
        /// </summary>
        public void RestartConnect()
        {
            ipEndPoint = new IPEndPoint(ipAddress, portNumber);
            client = new TcpClient();
            Connect();
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sendData">The send data.</param>
        public void SendData(string sendData)
        {
            try
            {
                if (client != null)
                {
                    SendDataSucceed(sendData);
                    SendDataFailed_UnConnect();
                }
                else
                {
                    SendDataFailed_NullServer();
                }
            }
            catch (Exception ex)
            {
                SendDataFailed_Exception(ex);
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sendData">The send data.</param>
        public void SendData(byte[] sendData)
        {
            try
            {
                if (client != null)
                {
                    SendDataSucceed(sendData);
                    SendDataFailed_UnConnect();
                }
                else
                {
                    SendDataFailed_NullServer();
                }
            }
            catch (Exception ex)
            {
                SendDataFailed_Exception(ex);
            }
        }

        /// <summary>
        /// Ends the reader.
        /// </summary>
        /// <param name="ir">The ir.</param>
        private void EndReader(IAsyncResult ir)
        {
            SocketObj _sks = ir.AsyncState as SocketObj;
            try
            {
                if (_sks != null)
                {
                    if (isClose && client == null)
                    {
                        sk.SkStream.Close();
                        sk.SkStream.Dispose();
                        return;
                    }

                    _sks.Offset = _sks.SkStream.EndRead(ir);
                    byte[] _buffer = new byte[_sks.Offset];
                    Array.Copy(recBuffer, _buffer, _sks.Offset);
                    if (_buffer != null)
                    {
                        string _readString = Encoding.UTF8.GetString(_buffer);
                        if (string.Compare(_readString, "ServerOff", true) == 0)
                        {
                            PusbClientMessage(SocketCode.ServerClose, null, null, _sks.Ip, null);
                        }
                        else
                        {
                            PusbClientMessage(SocketCode.DataReceived, _buffer, null, _sks.Ip, null);
                        }
                    }

                    sk.SkStream.BeginRead(recBuffer, 0, recBuffer.Length, new AsyncCallback(EndReader), sk);
                }
            }
            catch (Exception ex)
            {
                PusbClientMessage(SocketCode.DataReceivedError, null, ex, ipEndPoint, null);
            }
        }

        /// <summary>
        /// Called when [push client MSG hanlder event].
        /// </summary>
        /// <param name="sockets">The sockets.</param>
        private void OnPushClientMsgHanlderEvent(SocketData sockets)
        {
            if (PushClientMsgHanlderEvent != null && sockets != null)
            {
                PushClientMsgHanlderEvent(sockets);
            }
        }

        /// <summary>
        /// Pusbs the client message.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="buffer">The buffer.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="ipaddress">The ipaddress.</param>
        /// <param name="tag">The tag.</param>
        private void PusbClientMessage(SocketCode code, byte[] buffer, Exception exception, IPEndPoint ipaddress, object tag)
        {
            SocketData _sksData = new SocketData();
            _sksData.Code = code;
            _sksData.DataBuffer = buffer;
            _sksData.Ex = exception;
            _sksData.Ip = ipaddress;
            _sksData.Tag = tag;
            OnPushClientMsgHanlderEvent(_sksData);
        }

        /// <summary>
        /// Sends the data failed_ exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        private void SendDataFailed_Exception(Exception ex)
        {
            PusbClientMessage(SocketCode.SendDataError, null, ex, ipEndPoint, null);
            RestartConnect();
        }

        /// <summary>
        /// Sends the data failed_ null server.
        /// </summary>
        private void SendDataFailed_NullServer()
        {
            PusbClientMessage(SocketCode.ObjectNull, null, null, ipEndPoint, null);
            RestartConnect();
        }

        /// <summary>
        /// Sends the data failed_ un connect.
        /// </summary>
        private void SendDataFailed_UnConnect()
        {
            if (!client.Connected)
            {
                PusbClientMessage(SocketCode.UnConnect, null, null, ipEndPoint, null);
                RestartConnect();
            }
        }

        /// <summary>
        /// Sends the data succeed.
        /// </summary>
        /// <param name="sendData">The send data.</param>
        private void SendDataSucceed(string sendData)
        {
            if (client.Connected)
            {
                if (stream == null)
                {
                    stream = client.GetStream();
                }

                byte[] _buffer = Encoding.UTF8.GetBytes(sendData);
                stream.Write(_buffer, 0, _buffer.Length);
            }
        }

        /// <summary>
        /// Sends the data succeed.
        /// </summary>
        /// <param name="sendData">The send data.</param>
        private void SendDataSucceed(byte[] sendData)
        {
            if (client.Connected)
            {
                if (stream == null)
                {
                    stream = client.GetStream();
                }

                byte[] _buffer = sendData;
                stream.Write(_buffer, 0, _buffer.Length);
            }
        }

        #endregion Methods
    }
}