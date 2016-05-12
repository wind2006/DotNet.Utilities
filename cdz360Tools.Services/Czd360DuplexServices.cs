namespace cdz360Tools.Services
{
    using System;
    using System.IO;
    using System.Net;
    using System.ServiceModel;
    using System.Threading;

    using cdz360Tools.Contracts;
    using cdz360Tools.Services.Models;

    using YanZhiwei.DotNet.Core.Config;
    using YanZhiwei.DotNet.Log4Net.Utilities;
    using YanZhiwei.DotNet2.Utilities.Common;
    using YanZhiwei.DotNet2.Utilities.Models;
    using YanZhiwei.DotNet3._5.Interfaces;
    using YanZhiwei.DotNet3._5.Utilities.Core;
    using YanZhiwei.DotNet3._5.Utilities.Core.SchedulerType;

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Czd360DuplexServices : ICzd360DuplexServices
    {
        #region Fields

        /// <summary>
        /// Cdz360 Socket对象
        /// </summary>
        public static Cdz360SocketServer Cdz360Socket = new Cdz360SocketServer();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public Czd360DuplexServices()
        {
            SocketConfig _socket = ReadSocketConfig();
            if (CheckedSocketConfig(_socket))
            {
                Cdz360Socket.StartSocketServer(_socket.SocketItem);
                Cdz360Socket.Cdz360CallBackHanlderEvent += Cdz360Socket_Cdz360CallBackHanlderEvent;
                HanlderDetectKeyPress();
                HanlderJobTask();
            }
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// 充电桩预约
        /// </summary>
        /// <param name="gunSeqNo">枪编号</param>
        /// <param name="orderChargeType">预约充电方式，交流，直流</param>
        /// <param name="deviceSeqNo">充电桩号</param>
        /// <param name="cardSeqNo">预约卡号</param>
        /// <param name="phoneNumber">手机号码</param>
        /// <param name="startTime">预约开始时间</param>
        /// <param name="endTime">预约结束时间</param>
        public SvrRetMessage ChargingPileOrder(byte gunSeqNo, byte orderChargeType, string deviceSeqNo, string cardSeqNo, string phoneNumber, DateTime startTime, DateTime endTime)
        {
            SvrRetMessage _svrMessage = new SvrRetMessage();
            try
            {
                Cdz360Helper.PrintLog("当前连接总数：" + Cdz360Socket.CurConnectDevices.Keys.Count);
                phoneNumber = phoneNumber.PadLeft(12, '0');
                ChargingPileOrder_HisRec _item = new ChargingPileOrder_HisRec();
                _item.GunSeqNo = gunSeqNo;
                _item.OrderChargeType = orderChargeType;//0，交流，1，直流
                _item.DeviceSeqNo = deviceSeqNo;
                _item.UserPhoneNo = phoneNumber;
                _item.OrderSeqNo = string.Format("{0}{1}", cardSeqNo, DateTime.Now.ToString("yyMMddHHmmss"));
                _item.OrderType = true;
                _item.OrderStartTime = startTime;
                _item.OrderEndTime = endTime;
                _item.ServerTime = DateTime.Now;
                _item.OptStatus = 0;//默认没有配置成功
                byte _cmdWord = 0x04;
                using (MemoryStream stream = new MemoryStream())
                {
                    using (BinaryWriter writer = new BinaryWriter(stream))
                    {
                        writer.Write(gunSeqNo);//枪编号
                                               // writer.Write(orderChargeType);//充电方式
                        string _deviceSeqNo = string.Format("{0}{1}", orderChargeType, deviceSeqNo);
                        writer.Write(_deviceSeqNo.Parse8421BCDString(false));//充电桩号
                        writer.Write(phoneNumber.Parse8421BCDString(false));//手机号码
                        writer.Write(_item.OrderSeqNo.Parse8421BCDString(false));//订单编号
                        writer.Write((byte)0x02);//预约充电
                        writer.Write(startTime.ToString("yyMMddHHmmss").Parse8421BCDString(false));//开始时间
                        writer.Write(endTime.ToString("yyMMddHHmmss").Parse8421BCDString(false));//结束时间
                        writer.Write(DateTime.Now.ToString("yyMMddHHmmss").Parse8421BCDString(false));//当前服务器时间
                        if (Cdz360Socket.HanlderCommandSend(deviceSeqNo.PadLeft(12, '0'), _cmdWord, stream.ToArray(), "充电桩预约"))//指令发送成功，则存储数据库记录
                        {
                            _svrMessage.ExcuResult = Cdz360Socket.DbService.SaveChargingPileOrder(_item);
                        }
                        else {
                            _svrMessage.ExcuResult = false;
                            _svrMessage.Message = string.Format("充电桩『{0}』不在线, 当前连接总数：{1}。", _item.DeviceSeqNo, Cdz360Socket.CurConnectDevices.Count);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Cdz360Helper.PrintLog(string.Format("预约『{0}』充电预约失败，原因：{1}", deviceSeqNo, ex.Message));
            }
            return _svrMessage;
        }

        /// <summary>
        /// 通道检查
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public SvrRetMessage CheckoutChannel(DateTime time)
        {
            ICzd360Callback _callback = OperationContext.Current.GetCallbackChannel<ICzd360Callback>();
            _callback.CheckoutChannelMessage(string.Format("{0} 双工通道回调成功.", time.ToString("yyyy-MM-dd HH:mm:ss")));
            Cdz360Helper.PrintLog("当前连接总数：" + Cdz360Socket.CurConnectDevices.Keys.Count);
            foreach (var item in Cdz360Socket.CurConnectDevices)
            {
                Cdz360Helper.PrintLog(item.Key + ":" + item.Value);
            }
            return new SvrRetMessage()
            {
                ExcuResult = true,
                Message = string.Format("{0} 发起检查双工通道成功请求.", DateTime.Now.FormatDate(1))
            };
        }

        /// <summary>
        /// 检查Socket配置项
        /// </summary>
        /// <param name="socket">The _socket.</param>
        /// <returns></returns>
        /// 时间：2016-04-12 15:50
        /// 备注：
        private static bool CheckedSocketConfig(SocketConfig socket)
        {
            if (socket != null && socket.SocketItem != null)
            {
                Console.WriteLine("服务端Ip:{0}，端口:{1}。", socket.SocketItem.IpAddress, socket.SocketItem.Port);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 检查Socket服务是否初始化
        /// </summary>
        /// <returns></returns>
        private static bool CheckedSocketServer()
        {
            if (Cdz360Socket.Server == null)
            {
                Console.WriteLine("服务尚未初始化.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取操作提示
        /// </summary>
        /// 时间：2016-04-12 16:26
        /// 备注：
        private static void GetHelper()
        {
            Console.WriteLine("输入'service stop'停止服务.'");
            Console.WriteLine("输入'get clients'获取所有连接信息.'");
            Console.WriteLine("输入'clear clients'清除所有连接信息.'");
        }

        /// <summary>
        /// 处理清除并断开所有链接终端
        /// </summary>
        /// <param name="input">The input.</param>
        /// 时间：2016-04-12 19:29
        /// 备注：
        private static void HanlderClearSocketClients(string input)
        {
            if (input.Contains("clear clients"))
            {
                if (CheckedSocketServer())
                {
                    Cdz360Socket.Server.ClearAllClients();
                }
            }
        }

        /// <summary>
        /// 监听键盘输入
        /// </summary>
        /// 时间：2016-04-12 16:15
        /// 备注：
        private static void HanlderDetectKeyPress()
        {
            Thread _task = new Thread(() =>
            {
                do
                {
                    while (!Console.KeyAvailable)
                    {
                        try
                        {
                            string _input = Console.ReadLine();
                            HanlderHelper(_input);
                            HanlderServiceStop(_input);
                            HanlderGetSocketClients(_input);
                            HanlderClearSocketClients(_input);
                            HanlderGetConnectDevices(_input);
                        }
                        catch (Exception ex)
                        {
                            Log4NetHelper.WriteFatal("捕获到错误.", ex);
                            Console.WriteLine(ex);
                        }
                    }
                } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            });
            _task.Start();
        }

        /// <summary>
        /// 获取充电桩链接情况
        /// </summary>
        /// <param name="input">The input.</param>
        /// 时间：2016-04-25 9:40
        /// 备注：
        private static void HanlderGetConnectDevices(string input)
        {
            if (input.Contains("get devices"))
            {
                Cdz360Helper.PrintLog("当前连接总数：" + Cdz360Socket.CurConnectDevices.Count);
                Cdz360Helper.PrintLog("当前全部桩号:\r");
                foreach (var item in Cdz360Socket.CurConnectDevices)
                {
                    Cdz360Helper.PrintLog(item.Key + ":" + item.Value.ToString());
                }
            }
        }

        /// <summary>
        /// 获取终端连接列表
        /// </summary>
        /// <param name="input">The input.</param>
        /// 时间：2016-04-12 16:47
        /// 备注：
        private static void HanlderGetSocketClients(string input)
        {
            if (input.Contains("get clients"))
            {
                if (CheckedSocketServer())
                {
                    Console.WriteLine("共有连接终端：" + Czd360DuplexServices.Cdz360Socket.Server.ClientList.Count + " 个，详情如下：");
                    for (int i = 0; i < Czd360DuplexServices.Cdz360Socket.Server.ClientList.Count; i++)
                    {
                        SocketObj _socketClient = Czd360DuplexServices.Cdz360Socket.Server.ClientList[i];
                        Console.WriteLine(_socketClient.Ip);
                    }
                }
            }
        }

        /// <summary>
        /// 获取帮助提示信息
        /// </summary>
        /// <param name="input">The input.</param>
        /// 时间：2016-04-12 19:29
        /// 备注：
        private static void HanlderHelper(string input)
        {
            if (input.Contains("helper"))
            {
                GetHelper();
            }
        }

        /// <summary>
        /// 处理定时任务
        /// </summary>
        /// 时间：2016-04-13 17:06
        /// 备注：
        private static void HanlderJobTask()
        {
            if (CachedConfigContext.Current.JobConfig.JobItems != null)
            {
                foreach (JobItem job in CachedConfigContext.Current.JobConfig.JobItems)
                {
                    Cdz360Helper.PrintLog(string.Format("定时任务：{0} 执行时间：{1} 周期：{2}(小时)", job.Name, job.ExecuteTime, job.ExecutePeriod));
                    ISchedule _schedule = new CycExecution(job.ExecuteTime, new TimeSpan(job.ExecutePeriod, 0, 0));
                    Job _task = new Job((obj) =>
                    {
                        try
                        {
                            RunMethod(obj.ToString());
                        }
                        catch (Exception ex)
                        {
                            Log4NetHelper.WriteFatal("捕获到错误.", ex);
                            Console.WriteLine(ex.Message);
                        }
                    },
                    _schedule,
                    job.Name);
                    _task.Start(job.RunAction);
                }
            }
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        /// <param name="input">The _input.</param>
        /// 时间：2016-04-12 16:20
        /// 备注：
        private static void HanlderServiceStop(string input)
        {
            if (input.Contains("service stop"))
            {
                if (CheckedSocketServer())
                    Cdz360Socket.Server.Stop();
            }
        }

        /// <summary>
        /// 读取Socket配置
        /// </summary>
        /// <returns></returns>
        /// 时间：2016-04-12 15:32
        /// 备注：
        private static SocketConfig ReadSocketConfig()
        {
            ConfigContext _configContext = new ConfigContext();
            var _socketConfig = CachedConfigContext.Current.Get<SocketConfig>();

            return _socketConfig;
        }

        private static void RunMethod(string p)
        {
            //Action _callMethod = () => { };
            switch (p)
            {
                case "HanlderCorrectingTimeJob":
                    //_callMethod = () => HanlderCorrectingTimeJob();
                    Cdz360Socket.SendCorrectingTimeOrder();
                    break;
            }
            // return _callMethod;
        }

        private void Cdz360Socket_Cdz360CallBackHanlderEvent(IPEndPoint ip, Cdz360PackageData packageData)
        {
            try
            {
                HanlderChargingPileOrder(ip, packageData);
            }
            catch (Exception ex)
            {
                Cdz360Helper.PrintLog(string.Format("充电桩回调处理失败，原因：{0}", ex.Message));
            }
        }

        /// <summary>
        /// 充电桩预约响应
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="packageData">The _package data.</param>
        /// 时间：2016-04-21 14:59
        /// 备注：
        private void HanlderChargingPileOrder(IPEndPoint ip, Cdz360PackageData packageData)
        {
            if (packageData.CmdWord == 0x04)
            {
                byte _gunSeqNo = packageData.CmdParms[0];//充电枪号
                string _deviceSeqNo = ByteHelper.ToHexString(packageData.CmdParms, 2, 8);//充电桩号
                string _orderSeqNo = ByteHelper.ToHexString(packageData.CmdParms, 8, 20);//预约单号
                byte _orderStatus = packageData.CmdParms[20];//预约状态 0xff 成功，0x00失败

                ICzd360Callback _callback = OperationContext.Current.GetCallbackChannel<ICzd360Callback>();
                _callback.ChargingPileOrderMesage(_deviceSeqNo, _gunSeqNo, _orderSeqNo, _orderStatus == 0xff);//触发回调
            }
        }

        #endregion Methods
    }
}