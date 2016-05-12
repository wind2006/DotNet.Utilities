namespace YanZhiwei.DotNet2.Utilities.WinForm
{
    using System;
    using System.IO.Ports;

    using YanZhiwei.DotNet2.Utilities.Common;
    using YanZhiwei.DotNet2.Utilities.Enums;

    /// <summary>
    /// 串口操作帮助类
    /// </summary>
    public class SerialPortHelper
    {
        #region Fields

        private static readonly object syncObj = new object();

        private SerialPort comport = null;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="portName">需要打开串口的名称</param>
        /// <param name="dateBits">数据位</param>
        /// <param name="bondRate">波特率</param>
        /// <param name="parity">效验</param>
        /// <param name="stopBit">停止位</param>
        public SerialPortHelper(string portName, int dateBits, int bondRate, string parity, string stopBit)
        {
            comport = InitSerialPort();
            comport.PortName = portName;
            comport.BaudRate = bondRate;
            comport.DataBits = dateBits;
            InitStopBitParameter(stopBit);
            InitParityParameter(parity);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="dateBits"></param>
        /// <param name="bondRate"></param>
        /// <param name="parity"></param>
        /// <param name="stopBit"></param>
        public SerialPortHelper(string portName, SerialPortDatabits dateBits, SerialPortBaudRates bondRate, Parity parity, StopBits stopBit)
        {
            comport = InitSerialPort();
            comport.PortName = portName;
            comport.BaudRate = (int)bondRate;
            comport.DataBits = (int)dateBits;
            comport.Parity = parity;
            comport.StopBits = stopBit;
        }

        #endregion Constructors

        #region Delegates

        /// <summary>
        /// 串口数据接收委托
        /// </summary>
        /// <param name="buffer"></param>
        public delegate void ReceiveDataHanlder(byte[] buffer);

        #endregion Delegates

        #region Events

        /// <summary>
        /// 串口数据接收事件
        /// </summary>
        public event ReceiveDataHanlder ReceiveDataHanlderEvent;

        #endregion Events

        #region Methods

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void Close()
        {
            if (comport.IsOpen)
                comport.Close();
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <returns></returns>
        public void Open()
        {
            if (!comport.IsOpen)
                comport.Close();
            comport.Open();
            comport.DiscardInBuffer();
            comport.DiscardOutBuffer();
        }

        /// <summary>
        /// 向串口写入数据
        /// </summary>
        /// <param name="buffer">需要写入的二进制流</param>
        /// <returns>组元</returns>
        public Tuple<SerialPortState, string> Write(byte[] buffer)
        {
            if (CheckedSerialPortOpen())
            {
                try
                {
                    comport.Write(buffer, 0, buffer.Length);
                    string _hexString = ByteHelper.ToHexStringWithBlank(buffer);
                    return Tuple.Create(SerialPortState.SendSucceed, _hexString);
                }
                catch (TimeoutException ex)
                {
                    return Tuple.Create(SerialPortState.SendTimeout, ex.Message.Trim());
                }
                catch (Exception ex)
                {
                    return Tuple.Create(SerialPortState.SendFailed, ex.Message.Trim());
                }
            }
            else
            {
                return Tuple.Create(SerialPortState.UnOpened, string.Empty);
            }
        }

        private bool CheckedSerialPortOpen()
        {
            return comport != null && comport.IsOpen;
        }

        /// <summary>
        /// DataReceived
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Comport_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            lock (syncObj)
            {
                SerialPort _serialPort = sender as SerialPort;
                byte[] _buffer = new byte[_serialPort.BytesToRead];
                if (_buffer.Length > 0)
                {
                    int _bufferCount = _serialPort.Read(_buffer, 0, _buffer.Length);//需要读取来自串口数据长度
                    byte[] _actualBuffer = ArrayHelper.Copy<byte>(_buffer, 0, _buffer.Length);//截取
                    ReceiveDataHanlderEvent(_actualBuffer);
                }
            }
        }

        private void InitParityParameter(string parity)
        {
            switch (parity)
            {
                case "偶":
                    comport.Parity = Parity.Even;
                    break;

                case "奇":
                    comport.Parity = Parity.Odd;
                    break;

                case "空格":
                    comport.Parity = Parity.Space;
                    break;

                case "标志":
                    comport.Parity = Parity.Mark;
                    break;

                case "无":
                    comport.Parity = Parity.None;
                    break;

                default:
                    comport.Parity = Parity.None;
                    break;
            }
        }

        /// <summary>
        /// 重新初始化串口对象
        /// </summary>
        /// <returns></returns>
        private SerialPort InitSerialPort()
        {
            if (comport == null)
                comport = new SerialPort();
            if (!comport.IsOpen)
                comport.Close();
            comport.DataReceived += Comport_DataReceived;
            return comport;
        }

        private void InitStopBitParameter(string stopBit)
        {
            switch (stopBit)
            {
                case "1":
                    comport.StopBits = StopBits.One;
                    break;

                case "1.5":
                    comport.StopBits = StopBits.OnePointFive;
                    break;

                case "2":
                    comport.StopBits = StopBits.Two;
                    break;

                default:
                    comport.StopBits = StopBits.None;
                    break;
            }
        }

        private void OnReceiveDataHanlderEvent(byte[] buffer)
        {
            if (ReceiveDataHanlderEvent != null)
                ReceiveDataHanlderEvent(buffer);
        }

        #endregion Methods
    }
}