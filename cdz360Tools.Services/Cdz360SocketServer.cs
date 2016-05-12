namespace cdz360Tools.Services
{
    using BLL;
    using Contract;
    using Models;
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Net;
    using YanZhiwei.DotNet.Log4Net.Utilities;
    using YanZhiwei.DotNet2.Utilities.Common;
    using YanZhiwei.DotNet2.Utilities.Core;
    using YanZhiwei.DotNet2.Utilities.Models;

    /// <summary>
    /// 鼎充Socket Server
    /// </summary>
    /// 时间：2016-04-12 20:12
    /// 备注：
    public class Cdz360SocketServer
    {
        #region Fields

        public ConcurrentDictionary<string, IPEndPoint> CurConnectDevices = new ConcurrentDictionary<string, IPEndPoint>();

        /// <summary>
        /// 鼎充数据存储服务
        /// </summary>
        /// 时间：2016-04-13 15:29
        /// 备注：
        public ICdz360DbService DbService = null;

        /// <summary>
        /// Socket服务对象
        /// </summary>
        /// 时间：2016-04-12 19:30
        /// 备注：
        public SocketServer Server = null;

        /// <summary>
        /// 锁对象
        /// </summary>
        /// 时间：2016-04-12 19:28
        /// 备注：
        private static readonly object syncObj = new object();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 静态构造函数
        /// </summary>
        /// 时间：2016-04-13 15:29
        /// 备注：
        public Cdz360SocketServer()
        {
            DbService = new Cdz360DbService();
        }

        #endregion Constructors

        #region Delegates

        /// <summary>
        /// 充电桩上行指令委托
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="packageData">The package data.</param>
        /// 时间：2016-04-25 9:57
        /// 备注：
        public delegate void Cdz360CallBackHanlder(IPEndPoint ip, Cdz360PackageData packageData);

        #endregion Delegates

        #region Events

        /// <summary>
        /// 充电桩上行指令事件
        /// </summary>
        /// 时间：2016-04-25 9:59
        /// 备注：
        public event Cdz360CallBackHanlder Cdz360CallBackHanlderEvent;

        #endregion Events

        #region Methods

        /// <summary>
        /// 向终端发送数据包协议
        /// </summary>
        /// <param name="deviceSeqNo">The device seq no.</param>
        /// <param name="cmdWord">The command word.</param>
        /// <param name="cmdParams">The command parameters.</param>
        /// <param name="desc">The desc.</param>
        /// 时间：2016-04-21 14:41
        /// 备注：
        public bool HanlderCommandSend(string deviceSeqNo, byte cmdWord, byte[] cmdParams, string desc)
        {
            try
            {
                Cdz360Helper.PrintLog("HanlderCommandSend:" + deviceSeqNo);
                Tuple<bool, IPEndPoint> _result = GetConnectDevices(deviceSeqNo);
                if (_result.Item1)
                {
                    HanlderCommandSend(_result.Item2, cmdWord, cmdParams, desc);
                }
                return _result.Item1;
            }
            catch (Exception ex)
            {
                throw new BusinessException("向终端发送数据包协议发生错误", ex);
            }
        }

        /// <summary>
        /// 充电桩上行指令触发
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="packageData">The package data.</param>
        /// 时间：2016-04-25 10:04
        /// 备注：
        public void OnCdz360CallBackHanlderEvent(IPEndPoint ip, Cdz360PackageData packageData)
        {
            if (Cdz360CallBackHanlderEvent != null)
            {
                Cdz360CallBackHanlderEvent(ip, packageData);
            }
        }

        /// <summary>
        /// 执行网络校时指令
        /// </summary>
        /// 时间：2016-04-13 17:23
        /// 备注：
        public void SendCorrectingTimeOrder()
        {
            if (Server != null)
            {
                byte _cmdWord = 0x08;
                for (int i = 0; i < Server.ClientList.Count; i++)
                {
                    SocketObj _client = Server.ClientList[i];
                    byte[] _cmdParms = BCDHelper.Parse8421BCDString(DateTime.Now.ToString("yyMMddHHmmss"), false);
                    HanlderCommandSend(_client.Ip, _cmdWord, _cmdParms, "电桩校时");
                }
            }
        }

        /// <summary>
        /// 启动Socket 服务
        /// </summary>
        /// <param name="socketItem">The socket item.</param>
        /// 时间：2016-04-12 15:50
        /// 备注：
        public void StartSocketServer(SocketItem socketItem)
        {
            Server = new SocketServer(socketItem.IpAddress, socketItem.Port);
            Server.PushServerMsgHanlderEvent += (sks) =>
            {
                lock (syncObj)
                {
                    try
                    {
                        switch (sks.Code)
                        {
                            case SocketCode.StartSucceed:
                                Cdz360Helper.PrintLog(string.Format("主站启动：『{0}』成功。", sks.Ip.ToString()));
                                break;

                            case SocketCode.DataReceived:
                                byte[] _buffer = sks.DataBuffer;
                                string _hexString = ByteHelper.ToHexStringWithBlank(_buffer);
                                Cdz360Helper.PrintLog(string.Format("主站<==终端『{0}』数据：{1}", sks.Ip.ToString(), _hexString));
                                HanlderCommandParser(_buffer, sks.Ip);
                                break;

                            case SocketCode.NewClientConnect:
                                Cdz360Helper.PrintLog(string.Format("终端：『{0}』连接。", sks.Ip.ToString()));
                                break;

                            case SocketCode.ClientOffline:
                                Cdz360Helper.PrintLog(string.Format("终端：『{0}』下线。", sks.Ip.ToString()));
                                break;

                            case SocketCode.Stop:
                                Cdz360Helper.PrintLog(string.Format("主站关闭：『{0}』成功。", sks.Ip.ToString()));
                                break;

                            case SocketCode.SendDataError:
                                Cdz360Helper.PrintLog(string.Format("主站向终端：『{0}』发送数据失败。", sks.Ip.ToString()));
                                break;

                            case SocketCode.NoClinets:
                                Cdz360Helper.PrintLog("暂无终端连接主站。");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Cdz360Helper.PrintLog(ex.Message);
                    }
                }
            };
            Server.Start();
        }

        /// <summary>
        /// 根据充电桩号获取连接ip
        /// </summary>
        /// <param name="deivceSeqNo">The deivce seq no.</param>
        /// <returns></returns>
        /// 时间：2016-04-21 14:24
        /// 备注：
        private Tuple<bool, IPEndPoint> GetConnectDevices(string deivceSeqNo)
        {
            IPEndPoint _getIp = new IPEndPoint(IPAddress.Any, 2222);
            bool _result = CurConnectDevices.TryGetValue(deivceSeqNo, out _getIp);
            return new Tuple<bool, IPEndPoint>(_result, _getIp);
        }

        /// <summary>
        /// 处理充电桩注册协议
        /// </summary>
        /// <param name="packageData">The package data.</param>
        /// 时间：2016-04-13 11:21
        /// 备注：
        private bool HanlderCdz360Register(IPEndPoint ip, Cdz360PackageData packageData)
        {
            if (packageData.CmdWord == 0x01)
            {
                ChargingPileInfo _chargePieInfo = new ChargingPileInfo();
                //_chargePieInfo.Address = "上海天华信息科技园.";
                _chargePieInfo.ConnectType = packageData.CmdParms[0];
                _chargePieInfo.DeviceType = packageData.CmdParms[1];
                _chargePieInfo.DeviceSeqNo = ByteHelper.ToHexString(ArrayHelper.Copy(packageData.CmdParms, 2, packageData.CmdParms.Length));
                _chargePieInfo.Meno = "测试设备";
                _chargePieInfo.LastCommTime = DateTime.Now;
                _chargePieInfo.IpAddress = ip.ToString();
                StorageConnectDevices(_chargePieInfo.DeviceSeqNo, ip);
                Cdz360Helper.PrintLog("StorageConnectDevices:" + _chargePieInfo.DeviceSeqNo);
                return DbService.SaveChargingPileInfo(_chargePieInfo);
            }
            return false;
        }

        /// <summary>
        /// 充电结束后上报
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="_packageData">The _package data.</param>
        /// 时间：2016-04-14 14:44
        /// 备注：
        private bool HanlderChargeOver(IPEndPoint ip, Cdz360PackageData packageData)
        {
            if (packageData.CmdWord == 0x03)
            {
                ChargeOverHisRec _item = new ChargeOverHisRec();
                _item.GunSeqNo = packageData.CmdParms[0];//枪口编号
                _item.CurChargeType = packageData.CmdParms[1];//（0 交流，1 直流)
                _item.DeviceSeqNo = ByteHelper.ToHexString(packageData.CmdParms, 2, 8);//充电桩号
                _item.CardSeqNo = ByteHelper.ToHexString(packageData.CmdParms, 8, 14);//卡号
                _item.UserOrderNo = ByteHelper.ToHexString(packageData.CmdParms, 14, 26);//订单号
                _item.PowerConsume = BCDHelper.To8421BCDString(packageData.CmdParms, 26, 29, false).ToDecimalOrDefault(0) / 100.0m;//消费电量
                _item.TotalAmount = BCDHelper.To8421BCDString(packageData.CmdParms, 29, 32, false).ToDecimalOrDefault(0) / 100.0m;//消费金额
                _item.BatteryOrgValue = ByteHelper.CalcPercentage(packageData.CmdParms[32]);//soc
                _item.BatteryCurValue = ByteHelper.CalcPercentage(packageData.CmdParms[33]);//soc
                _item.OptStartTime = BCDHelper.To8421BCDString(ArrayHelper.Copy(packageData.CmdParms, 34, 40), false).ParseDateTimeString("yyMMddHHmmss").ToDateOrDefault(default(DateTime));//充电开始时间
                _item.OptStartTime = _item.OptStartTime == default(DateTime) ? UnixEpochHelper.UnixEpochUtcValue : _item.OptStartTime;

                _item.OptEndTime = BCDHelper.To8421BCDString(ArrayHelper.Copy(packageData.CmdParms, 40, 46), false).ParseDateTimeString("yyMMddHHmmss").ToDateOrDefault(default(DateTime));//充电结束时间
                _item.OptEndTime = _item.OptEndTime == default(DateTime) ? UnixEpochHelper.UnixEpochUtcValue : _item.OptEndTime;

                DbService.SaveChargeOverRec(_item);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 接收数据包处理
        /// </summary>
        /// <param name="_buffer">The _buffer.</param>
        /// <param name="ip">The ip.</param>
        /// 时间：2016-04-12 15:56
        /// 备注：
        private void HanlderCommandParser(byte[] buffer, IPEndPoint ip)
        {
            if (buffer != null)
            {
                try
                {
                    Cdz360PackageData _packageData = Cdz360PackageData.InitPackage();
                    byte[] _actulBuffer = null;
                    PackageErrorEnum _packageError = PackageErrorEnum.Normal;
                    if (Cdz360PackageData.BuileObjFromBytes(buffer, out _packageData, out _actulBuffer, out _packageError))//合法数据协议
                    {
                        if (HanlderCdz360Register(ip, _packageData))
                            RespondCdz360Register(ip, _packageData);
                        if (HanlderHeartbeat(ip, _packageData))
                            RespondHeartbeat(ip, _packageData);
                        if (HanlderChargeOver(ip, _packageData))
                            RespondChargeOver(ip, _packageData);
                        if (HanlderOfflineChargeOver(_packageData))
                            RespondOfflineChargeOver(ip, _packageData);
                        else
                            OnCdz360CallBackHanlderEvent(ip, _packageData);
                    }
                    else {
                        Cdz360Helper.PrintLog(ByteHelper.ToHexStringWithBlank(buffer) + " " + _packageError);
                    }
                }
                catch (Exception ex)
                {
                    Log4NetHelper.WriteFatal("转换为协议包对象失败.", ex);
                    Console.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        /// 向终端发送数据包协议
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="cmdWord">The command word.</param>
        /// <param name="cmdParams">The command parameters.</param>
        /// 时间：2016-04-13 16:16
        /// 备注：
        private void HanlderCommandSend(IPEndPoint ip, byte cmdWord, byte[] cmdParams, string desc)
        {
            Cdz360PackageData _packageData = Cdz360PackageData.InitPackage();
            _packageData.CmdWord = cmdWord;
            _packageData.CmdParms = cmdParams;
            byte[] _buffer = _packageData.ToBytes();
            Server.SendToClient(ip, _buffer);
            Cdz360Helper.PrintLog(string.Format("{2} 主站==>终端『{0}』数据：{1}", ip, ByteHelper.ToHexStringWithBlank(_buffer), desc));
        }

        /// <summary>
        /// 处理心跳协议
        /// </summary>
        /// <param name="packageData">The package data.</param>
        /// 时间：2016-04-14 11:14
        /// 备注：
        private bool HanlderHeartbeat(IPEndPoint ip, Cdz360PackageData packageData)
        {
            if (packageData.CmdWord == 0x02)
            {
                ChargingPileOnlineStatus _item = new ChargingPileOnlineStatus();
                _item.GunSeqNo = packageData.CmdParms[1];//枪口编号
                _item.DeviceType = packageData.CmdParms[2];//（0 交流，1 直流)
                _item.DeviceSeqNo = ByteHelper.ToHexString(ArrayHelper.Copy(packageData.CmdParms, 3, 9));//充电桩号
                _item.ChargingVoltage = BCDHelper.To8421BCDString(ArrayHelper.Copy(packageData.CmdParms, 9, 12), false).ToDecimalOrDefault(0) / 100.0m; //充电电压
                _item.ChargingCurrent = BCDHelper.To8421BCDString(ArrayHelper.Copy(packageData.CmdParms, 12, 15), false).ToDecimalOrDefault(0) / 100.0m;//充电电流
                _item.PowerConsume = BCDHelper.To8421BCDString(ArrayHelper.Copy(packageData.CmdParms, 18, 18), false).ToDecimalOrDefault(0) / 100.0m;//消费电量
                _item.SOC = ByteHelper.CalcPercentage(ArrayHelper.Copy(packageData.CmdParms, 18, 19)[0]);//当前SOC
                _item.OrderSeqNo = ByteHelper.ToHexString(ArrayHelper.Copy(packageData.CmdParms, 19, 31));//订单流水号
                DbService.SaveChargingPileOnlineStatus(_item);
                StorageConnectDevices(_item.DeviceSeqNo, ip);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 线下充电上报选择模式
        /// </summary>
        /// <param name="packageData">The _package data.</param>
        /// 时间：2016-04-14 17:24
        /// 备注：
        private bool HanlderOfflineChargeOver(Cdz360PackageData packageData)
        {
            if (packageData.CmdWord == 0x0d)
            {
                ChargingPileOffline_HisRec _item = new ChargingPileOffline_HisRec();
                _item.GunSeqNo = packageData.CmdParms[0];//枪口编号
                _item.CurChargeType = packageData.CmdParms[1];//（0 交流，1 直流)
                _item.DeviceSeqNo = ByteHelper.ToHexString(packageData.CmdParms, 2, 8);//充电桩号
                _item.CardSeqNo = ByteHelper.ToHexString(packageData.CmdParms, 8, 14);//卡号
                _item.CardBalance = BCDHelper.To8421BCDString(packageData.CmdParms, 14, 18, false).ToDecimalOrDefault(0) / 100.0m;//消费金额
                _item.UserChooseMode = packageData.CmdParms[18];//用户选中模式
                _item.ModeValue = ByteHelper.ToHexString(packageData.CmdParms, 19, 21).ToInt32OrDefault(0);
                _item.PowerConsume = BCDHelper.To8421BCDString(packageData.CmdParms, 21, 24, false).ToDecimalOrDefault(0) / 100.0m;//消费电量
                _item.TotalAmount = BCDHelper.To8421BCDString(packageData.CmdParms, 24, 27, false).ToDecimalOrDefault(0) / 100.0m;//消费金额
                DbService.SaveChargingPileOffline(_item);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 响应充电桩注册协议
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="packageData">The package data.</param>
        /// 时间：2016-04-13 16:17
        /// 备注：
        private void RespondCdz360Register(IPEndPoint ip, Cdz360PackageData packageData)
        {
            byte _cmdWord = 0x01;
            byte[] _cmdParms = BCDHelper.Parse8421BCDString(DateTime.Now.ToString("yyMMddHHmmss"), false);
            HanlderCommandSend(ip, _cmdWord, _cmdParms, "充电桩注册响应");
        }

        /// <summary>
        /// 响应充电结束后充电记录上报
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="packageData">The package data.</param>
        /// 时间：2016-04-14 15:47
        /// 备注：
        private void RespondChargeOver(IPEndPoint ip, Cdz360PackageData packageData)
        {
            byte _cmdWord = 0x03;
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(packageData.CmdParms[0]);//枪口编号
                    writer.Write(ArrayHelper.Copy(packageData.CmdParms, 1, 8));//（0 交流，1直流,后边紧跟手机号码的BCD 码）
                    writer.Write(ArrayHelper.Copy(packageData.CmdParms, 14, 26));//订单流水号
                    HanlderCommandSend(ip, _cmdWord, stream.ToArray(), "响应充电结束后充电记录");
                }
            }
        }

        /// <summary>
        /// 心跳响应
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="packageData">The package data.</param>
        /// 时间：2016-04-14 14:30
        /// 备注：
        private void RespondHeartbeat(IPEndPoint ip, Cdz360PackageData packageData)
        {
            byte _cmdWord = 0x02;
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(packageData.CmdParms[1]);//枪口编号
                    writer.Write(ArrayHelper.Copy(packageData.CmdParms, 2, 9));//（0 交流，1直流,后边紧跟手机号码的BCD 码）
                    writer.Write((byte)0xff);//成功
                    writer.Write(ArrayHelper.Copy(packageData.CmdParms, 19, 31));//订单流水号
                    HanlderCommandSend(ip, _cmdWord, stream.ToArray(), "心跳响应");
                }
            }
        }

        /// <summary>
        /// 响应线下充电上报选择模式
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="_packageData">The _package data.</param>
        /// 时间：2016-04-14 17:38
        /// 备注：
        private void RespondOfflineChargeOver(IPEndPoint ip, Cdz360PackageData packageData)
        {
            byte _cmdWord = 0x0d;
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(packageData.CmdParms[0]);//枪口编号
                    writer.Write(ArrayHelper.Copy(packageData.CmdParms, 1, 8));//（0 交流，1直流,后边紧跟手机号码的BCD 码）
                    writer.Write((byte)0xff);//确认
                    HanlderCommandSend(ip, _cmdWord, stream.ToArray(), "响应线下充电上报选择模式");
                }
            }
        }

        /// <summary>
        /// 存储新连接上充电桩设备号以及Ip地址
        /// </summary>
        /// <param name="deivceSeqNo">The deivce seq no.</param>
        /// <param name="ip">The ip.</param>
        /// 时间：2016-04-21 14:23
        /// 备注：
        private void StorageConnectDevices(string deivceSeqNo, IPEndPoint ip)
        {
            CurConnectDevices.AddOrUpdate(deivceSeqNo, ip, (deviceSeqNo, oldIp) =>
            {
                return ip;
            });
        }

        #endregion Methods
    }
}