namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System;
    using System.IO.Ports;

    /// <summary>
    ///串口帮助类
    /// </summary>
    public static class SerialPortHelper
    {
        #region Fields

        private static string[] _baudRate;
        private static string[] _dataBits;
        private static string[] _parity;
        private static string[] _stopBits;

        #endregion Fields

        #region Properties

        /// <summary>
        /// 波特率
        /// </summary>
        public static string[] BaudRates
        {
            get
            {
                if (_baudRate == null)
                    _baudRate = new string[] { "600", "1200", "1800", "2400", "4800", "7200", "9600", "14400", "19200", "38400", "57600", "115200", "128000" };
                return _baudRate;
            }
        }

        /// <summary>
        /// 数据位
        /// </summary>
        public static string[] DataBits
        {
            get
            {
                if (_dataBits == null)
                    _dataBits = new string[] { "4", "5", "6", "7", "8" };
                return _dataBits;
            }
        }

        /// <summary>
        /// 效验_英文
        /// </summary>
        public static string[] Paritys
        {
            get
            {
                return Enum.GetNames(typeof(Parity));
            }
        }

        /// <summary>
        /// 效验_中文
        /// </summary>
        public static string[] Paritys_CH
        {
            get
            {
                if (_parity == null)
                {
                    string[] _paritys = Enum.GetNames(typeof(Parity));
                    _parity = new string[_paritys.Length];
                    int i = 0;
                    foreach (string pt in _paritys)
                    {
                        if (string.Compare(pt, "None", true) == 0)
                            _parity[i] = "无";
                        else if (string.Compare(pt, "Odd", true) == 0)
                            _parity[i] = "奇";
                        else if (string.Compare(pt, "Even", true) == 0)
                            _parity[i] = "偶";
                        else if (string.Compare(pt, "Mark", true) == 0)
                            _parity[i] = "标志";
                        else if (string.Compare(pt, "Space", true) == 0)
                            _parity[i] = "空格";
                        else
                            _parity[i] = pt;
                        i++;
                    }
                }
                return _parity;
            }
        }

        /// <summary>
        /// 串口列表
        /// </summary>
        public static string[] PortNames
        {
            get
            {
                return SerialPort.GetPortNames();
            }
        }

        /// <summary>
        /// 停止位
        /// </summary>
        public static string[] StopBits
        {
            get
            {
                return Enum.GetNames(typeof(StopBits));
            }
        }

        /// <summary>
        /// 停止位_英文转移成数字
        /// </summary>
        public static string[] StopBits_CH
        {
            get
            {
                if (_stopBits == null)
                {
                    string[] _stopBitses = Enum.GetNames(typeof(StopBits));
                    int i = 0;
                    _stopBits = new string[_stopBitses.Length - 1];
                    foreach (string st in _stopBitses)
                    {
                        if (string.Compare(st, "None", true) == 0)
                            continue;
                        else if (string.Compare(st, "One", true) == 0)
                            _stopBits[i] = "1";
                        else if (string.Compare(st, "Two", true) == 0)
                            _stopBits[i] = "2";
                        else if (string.Compare(st, "OnePointFive", true) == 0)
                            _stopBits[i] = "1.5";
                        else
                            _stopBits[i] = st;
                        i++;
                    }
                }
                return _stopBits;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <param name="serialPort">串口组件</param>
        /// <param name="portName">需要关闭的串口名称</param>
        public static void Close(this SerialPort serialPort, string portName)
        {
            if (serialPort.IsOpen)
                serialPort.Close();
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="serialPort">串口</param>
        /// <param name="portName">需要打开串口的名称</param>
        /// <param name="dateBits">数据位</param>
        /// <param name="bondRate">波特率</param>
        /// <param name="parity">效验</param>
        /// <param name="stopBit">停止位</param>
        public static void Open(this SerialPort serialPort, string portName, string dateBits, string bondRate, string parity, string stopBit)
        {
            int _bondRate = -1, _dateBits = -1;
            if (!int.TryParse(bondRate, out _bondRate))
                throw new ArgumentException("bondRate");
            if (!int.TryParse(dateBits, out _dateBits))
                throw new ArgumentException("dateBits");
            if (serialPort.IsOpen)
                serialPort.Close();
            serialPort.PortName = portName;
            serialPort.BaudRate = _bondRate;
            serialPort.DataBits = _dateBits;
            switch (stopBit)
            {
                case "1":
                    serialPort.StopBits = System.IO.Ports.StopBits.One;
                    break;

                case "1.5":
                    serialPort.StopBits = System.IO.Ports.StopBits.OnePointFive;
                    break;

                case "2":
                    serialPort.StopBits = System.IO.Ports.StopBits.Two;
                    break;

                default:
                    serialPort.StopBits = System.IO.Ports.StopBits.None;
                    break;
            }
            switch (parity)
            {
                case "偶":
                    serialPort.Parity = Parity.Even;
                    break;

                case "奇":
                    serialPort.Parity = Parity.Odd;
                    break;

                case "空格":
                    serialPort.Parity = Parity.Space;
                    break;

                case "标志":
                    serialPort.Parity = Parity.Mark;
                    break;

                case "无":
                    serialPort.Parity = Parity.None;
                    break;

                default:
                    serialPort.Parity = Parity.None;
                    break;
            }
            serialPort.Open();
        }

        #endregion Methods
    }
}