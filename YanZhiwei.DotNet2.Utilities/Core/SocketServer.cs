namespace YanZhiwei.DotNet2.Utilities.Core
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;

    using YanZhiwei.DotNet2.Utilities.Models;

    /// <summary>
    /// Socket Server
    /// </summary>
    public class SocketServer
    {
        #region Fields

        /// <summary>
        /// 当前IP地址
        /// </summary>
        private IPAddress ipaddress;

        /// <summary>
        /// 当前IP,端口对象
        /// </summary>
        private IPEndPoint ipEndPoint;

        /// <summary>
        /// 是否停止
        /// </summary>
        private bool isStop = false;

        /// <summary>
        /// 服务端
        /// </summary>
        private TcpListener listener;

        /// <summary>
        /// 当前监听端口
        /// </summary>
        private int portNumber;

        /// <summary>
        /// 接收缓冲区
        /// </summary>
        private byte[] recBuffer = new byte[1 * 1024 * 1024];

        /// <summary>
        /// 信号量
        /// </summary>
        private Semaphore semap = new Semaphore(5, 5000);

        /// <summary>
        /// 发送缓冲区
        /// </summary>
        private byte[] sendBuffer = new byte[1 * 1024 * 1024];

        /// <summary>
        /// The synchronize root
        /// </summary>
        private object syncRoot = new object();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="port">The port.</param>
        public SocketServer(IPAddress ip, int port)
        {
            ClientList = new ThreadSafeList<SocketObj>();
            ipaddress = ip;
            portNumber = port;
            listener = new TcpListener(ipaddress, portNumber);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="port">The port.</param>
        public SocketServer(string ip, int port)
        {
            ClientList = new ThreadSafeList<SocketObj>();
            ipaddress = IPAddress.Parse(ip);
            portNumber = port;
            ipEndPoint = new IPEndPoint(ipaddress, portNumber);
            listener = new TcpListener(ipaddress, portNumber);
        }

        #endregion Constructors

        #region Delegates

        /// <summary>
        /// 消息推送委托
        /// </summary>
        /// <param name="sockets">The sockets.</param>
        public delegate void PushServerMsgHanlder(SocketData sockets);

        #endregion Delegates

        #region Events

        /// <summary>
        /// 消息推送事件
        /// </summary>
        public event PushServerMsgHanlder PushServerMsgHanlderEvent;

        #endregion Events

        #region Properties

        /// <summary>
        /// 客户端队列集合
        /// </summary>
        public ThreadSafeList<SocketObj> ClientList
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 消息推送
        /// </summary>
        /// <param name="sockets">The sockets.</param>
        public void OnPushServerMsgHanlderEvent(SocketData sockets)
        {
            if (PushServerMsgHanlderEvent != null && sockets != null)
            {
                PushServerMsgHanlderEvent(sockets);
            }
        }

        /// <summary>
        /// 向所有在线的客户端发送信息.
        /// </summary>
        /// <param name="sendData">发送的文本</param>
        public void SendToAll(string sendData)
        {
            for (int i = 0; i < ClientList.Count; i++)
            {
                SendToClient(ClientList[i].Ip, sendData);
            }
        }

        /// <summary>
        /// 向所有在线的客户端发送信息.
        /// </summary>
        /// <param name="sendDataBuffer">发送的文本</param>
        public void SendToAll(byte[] sendDataBuffer)
        {
            for (int i = 0; i < ClientList.Count; i++)
            {
                SendToClient(ClientList[i].Ip, sendDataBuffer);
            }
        }

        /// <summary>
        /// 向某一位客户端发送信息
        /// </summary>
        /// <param name="ip">客户端IP+端口地址</param>
        /// <param name="sendDataBuffer">发送的数据包</param>
        public void SendToClient(IPEndPoint ip, byte[] sendDataBuffer)
        {
            try
            {
                SocketObj _sks = ClientList.Find(o => { return o.Ip == ip; });
                if (_sks != null)
                {
                    if (_sks.Client.Connected)
                    {
                        NetworkStream _stream = _sks.SkStream;
                        if (_stream.CanWrite)
                        {
                            byte[] _buffer = sendDataBuffer;
                            _stream.Write(_buffer, 0, _buffer.Length);
                        }
                        else
                        {
                            _stream = _sks.Client.GetStream();
                            if (_stream.CanWrite)
                            {
                                byte[] _buffer = sendDataBuffer;
                                _stream.Write(_buffer, 0, _buffer.Length);
                            }
                            else
                            {
                                ClientList.Remove(_sks);
                                PusbServerMessage(SocketCode.RemoveClientConnect, null, null, _sks.Ip, null);
                            }
                        }
                    }
                    else
                    {
                        PusbServerMessage(SocketCode.NoClinets, null, null, _sks.Ip, null);
                    }
                }
            }
            catch (Exception ex)
            {
                PusbServerMessage(SocketCode.SendDataError, null, ex, ip, null);
            }
        }

        /// <summary>
        /// 向某一位客户端发送信息
        /// </summary>
        /// <param name="ip">客户端IP+端口地址</param>
        /// <param name="sendData">发送的数据包</param>
        public void SendToClient(IPEndPoint ip, string sendData)
        {
            try
            {
                SocketObj _sks = ClientList.Find(o => { return o.Ip == ip; });
                if (_sks != null)
                {
                    if (_sks.Client.Connected)
                    {
                        NetworkStream _netStream = _sks.SkStream;
                        if (_netStream.CanWrite)
                        {
                            byte[] _buffer = Encoding.UTF8.GetBytes(sendData);
                            _netStream.Write(_buffer, 0, _buffer.Length);
                        }
                        else
                        {
                            _netStream = _sks.Client.GetStream();
                            if (_netStream.CanWrite)
                            {
                                byte[] _buffer = Encoding.UTF8.GetBytes(sendData);
                                _netStream.Write(_buffer, 0, _buffer.Length);
                            }
                            else
                            {
                                ClientList.Remove(_sks);
                            }
                        }
                    }
                    else
                    {
                        PusbServerMessage(SocketCode.NoClinets, null, null, ip, null);
                    }
                }
            }
            catch (Exception ex)
            {
                PusbServerMessage(SocketCode.SendDataError, null, ex, ip, null);
            }
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        public void Start()
        {
            try
            {
                listener.Start();
                Thread _task = new Thread(new ThreadStart(delegate
                {
                    while (true)
                    {
                        if (isStop != false)
                        {
                            break;
                        }

                        GetAcceptTcpClient();
                        Thread.Sleep(1);
                    }
                }));
                _task.Start();
                PusbServerMessage(SocketCode.StartSucceed, null, null, ipEndPoint, null);
            }
            catch (SocketException ex)
            {
                PusbServerMessage(SocketCode.StartError, null, ex, ipEndPoint, null);
            }
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        public void Stop()
        {
            if (listener != null)
            {
                SendToAll("ServerOff");
                listener.Stop();
                listener = null;
                isStop = true;
                PusbServerMessage(SocketCode.Stop, null, null, ipEndPoint, null);
            }
        }

        /// <summary>
        /// Adds the client list.
        /// </summary>
        /// <param name="sk">SocketObj</param>
        private void AddClientList(SocketObj sk)
        {
            SocketObj _sockets = ClientList.Find(o => { return o.Ip == sk.Ip; });
            if (_sockets == null)
            {
                ClientList.Add(sk);
            }
            else
            {
                ClientList.Remove(_sockets);
                ClientList.Add(sk);
            }

            PusbServerMessage(SocketCode.NewClientConnect, null, null, sk.Ip, null);
        }

        /// <summary>
        /// 异步接收发送的信息.
        /// </summary>
        /// <param name="ir">IAsyncResult</param>
        private void EndReader(IAsyncResult ir)
        {
            SocketObj _sks = ir.AsyncState as SocketObj;
            if (_sks != null && listener != null)
            {
                try
                {
                    if (_sks.NewClientFlag || _sks.Offset != 0)
                    {
                        _sks.NewClientFlag = false;
                        _sks.Offset = _sks.SkStream.EndRead(ir);
                        if (_sks.Offset != 0)
                        {
                            byte[] _buffer = new byte[_sks.Offset];
                            Array.Copy(recBuffer, _buffer, _sks.Offset);
                            PusbServerMessage(SocketCode.DataReceived, _buffer, null, _sks.Ip, null);
                        }
                        else
                        {
                            ClientList.Remove(_sks);//移除连接终端
                            PusbServerMessage(SocketCode.ClientOffline, null, null, _sks.Ip, null);
                        }

                        _sks.SkStream.BeginRead(recBuffer, 0, recBuffer.Length, new AsyncCallback(EndReader), _sks);
                    }
                }
                catch (Exception ex)
                {
                    lock (syncRoot)
                    {
                        ClientList.Remove(_sks);
                        PusbServerMessage(SocketCode.DataReceivedError, null, ex, _sks.Ip, null);
                    }
                }
            }
        }

        /// <summary>
        /// 等待处理新的连接
        /// </summary>
        private void GetAcceptTcpClient()
        {
            try
            {
                semap.WaitOne();

                TcpClient _tclient = listener.AcceptTcpClient();
                Socket _socket = _tclient.Client;
                NetworkStream _stream = new NetworkStream(_socket, true); //承载这个Socket
                SocketObj _sks = new SocketObj(_tclient.Client.RemoteEndPoint as IPEndPoint, _tclient, _stream);
                _sks.NewClientFlag = true;
                AddClientList(_sks);
                _sks.SkStream.BeginRead(recBuffer, 0, recBuffer.Length, new AsyncCallback(EndReader), _sks);
                if (_stream.CanWrite)
                {
                }

                semap.Release();
            }
            catch (Exception ex)
            {
                semap.Release();
                PusbServerMessage(SocketCode.NewClientConnectError, null, ex, null, null);
            }
        }

        /// <summary>
        /// 推送Server消息到终端
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="buffer">The buffer.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="ipaddress">The ipaddress.</param>
        /// <param name="tag">The tag.</param>
        private void PusbServerMessage(SocketCode code, byte[] buffer, Exception exception, IPEndPoint ipaddress, object tag)
        {
            SocketData _sksData = new SocketData();
            _sksData.Code = code;
            _sksData.DataBuffer = buffer;
            _sksData.Ex = exception;
            _sksData.Ip = ipaddress;
            _sksData.Tag = tag;
            OnPushServerMsgHanlderEvent(_sksData);
        }

        /// <summary>
        /// 断开，移除所有终端链接
        /// </summary>
        /// 时间：2016-04-12 19:19
        /// 备注：
        public void ClearAllClients()
        {
            if (ClientList != null)
            {
                for (int i = 0; i < ClientList.Count; i++)
                {
                    SocketObj _socketClient = ClientList[i];
                    ClientList.Remove(_socketClient);
                    _socketClient.Close();
                }
            }
        }

        /// <summary>
        /// 断开，移除某个终端连接
        /// </summary>
        /// <param name="ip">IPEndPoint</param>
        /// 时间：2016-04-12 19:21
        /// 备注：
        public void ClearClient(IPEndPoint ip)
        {
            if (ClientList != null)
            {
                SocketObj _sockets = ClientList.Find(o => { return o.Ip == ip; });
                if (_sockets != null)
                {
                    ClientList.Remove(_sockets);
                }
            }
        }

        #endregion Methods
    }
}