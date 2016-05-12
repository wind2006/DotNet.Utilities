namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System;

    using YanZhiwei.DotNet2.Utilities.Core;

    /// <summary>
    /// 检查 帮助类
    /// </summary>
    public static class CheckHelper
    {
        #region Methods

        /// <summary>
        /// 判断字符串是否在某个范围
        /// <para>eg:CheckHelper.InRange("2", 1, 5);==>true</para>
        /// <para>判断：小于等于并且大于等于</para>
        /// </summary>
        /// <param name="data">判断字符串</param>
        /// <param name="minValue">范围最小值</param>
        /// <param name="maxValue">范围最大值</param>
        /// <returns>是否在某个范围</returns>
        public static bool InRange(this string data, int minValue, int maxValue)
        {
            bool _result = false;
            int _number = -1;
            if (int.TryParse(data, out _number))
            {
                _result = (_number >= minValue && _number <= maxValue);
            }
            return _result;
        }

        /// <summary>
        /// 判断时间是否在时间范围内
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <param name="includeEq">if set to <c>true</c> [include eq].</param>
        /// <returns></returns>
        /// 创建时间:2015-06-30 16:52
        /// 备注说明:<c>null</c>
        public static bool InRange(this DateTime date, DateTime startTime, DateTime endTime, bool includeEq)
        {
            bool _result = false;
            if (includeEq)
            {
                if ((date >= startTime) && (date <= endTime))
                {
                    _result = true;
                }
            }
            else
            {
                if ((date > startTime) && (date < endTime))
                {
                    _result = true;
                }
            }
            return _result;
        }

        /// <summary>
        /// 是否是Base64
        /// </summary>
        /// <param name="data">验证数据</param>
        /// <returns>是否是Base64</returns>
        public static bool IsBase64(string data)
        {
            return (data.Length % 4) == 0 && RegexHelper.IsMatch(data, RegexPattern.Base64Check);
        }

        /// <summary>
        /// 是否是Bigint类型
        /// </summary>
        /// <param name="value">判断字符串</param>
        /// <param name="number">Bigint</param>
        /// <returns>是否是Bigint类型</returns>
        public static bool IsBigint(string value, out long number)
        {
            number = -1;
            return long.TryParse(value, out number);
        }

        /// <summary>
        /// 判断是否是BCD字符串
        /// </summary>
        /// <param name="data">验证字符串</param>
        /// <returns>否是BCD字符串</returns>
        public static bool IsBinaryCodedDecimal(this string data)
        {
            return RegexHelper.IsMatch(data, RegexPattern.BinaryCodedDecimal);
        }

        /// <summary>
        /// 是否是布尔类型
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>布尔类型</returns>
        public static bool IsBool(this object data)
        {
            switch (data.ToString().Trim().ToLower())
            {
                case "0":
                    return false;

                case "1":
                    return true;

                case "是":
                    return true;

                case "否":
                    return false;

                case "yes":
                    return true;

                case "no":
                    return false;

                default:
                    return false;
            }
        }

        /// <summary>
        /// 是否是中文
        /// </summary>
        /// <param name="data">验证字符串</param>
        /// <returns>是否是中文</returns>
        public static bool IsChinses(this string data)
        {
            return RegexHelper.IsMatch(data, RegexPattern.IntCheck);
        }

        /// <summary>
        /// 是否是日期格式
        /// <para>eg:CheckHelper.IsDate("2014年12月12日");==>true</para>
        /// </summary>
        /// <param name="data">需要判断字符串</param>
        /// <returns>是否是日期格式</returns>
        public static bool IsDate(this string data)
        {
            if (String.IsNullOrEmpty(data)) return false;
            if (RegexHelper.IsMatch(data, RegexPattern.DateCheck))
            {
                data = data.Replace("年", "-");
                data = data.Replace("月", "-");
                data = data.Replace("日", " ");
                data = data.Replace("  ", " ");
                DateTime _date;
                return DateTime.TryParse(data, out _date);
            }
            return false;
        }

        /// <summary>
        /// 验证是否是email
        /// <para>eg:CheckHelper.IsEmail("Yan.Zhiwei@hotmail.com");==true</para>
        /// </summary>
        /// <param name="data">验证字符串</param>
        /// <returns>是否是email</returns>
        public static bool IsEmail(this string data)
        {
            return RegexHelper.IsMatch(data, RegexPattern.EmailCheck);
        }

        /// <summary>
        /// 是否是偶数
        /// </summary>
        /// <param name="data">需要判断项</param>
        /// <returns>是否是偶数</returns>
        public static bool IsEven(this int data)
        {
            return ((data & 1) == 0);
        }

        /// <summary>
        /// 验证是否是文件路径
        /// <para>eg:CheckHelper.IsFilePath(@"C:\alipay\log.txt");==>true</para>
        /// </summary>
        /// <param name="data">验证字符串</param>
        /// <returns>是否是文件路径</returns>
        public static bool IsFilePath(this string data)
        {
            return RegexHelper.IsMatch(data, RegexPattern.FileCheck);
        }

        /// <summary>
        /// 是否是十六进制字符串
        /// </summary>
        /// <param name="data">验证数据</param>
        /// <returns>是否是十六进制字符串</returns>
        public static bool IsHexString(string data)
        {
            return RegexHelper.IsMatch(data, RegexPattern.HexStringCheck);
        }

        /// <summary>
        /// 是否是身份证号码
        /// </summary>
        /// <param name="data">验证数据</param>
        /// <returns>是否是身份证号码</returns>
        public static bool IsIdCard(string data)
        {
            return RegexHelper.IsMatch(data, RegexPattern.IdCardCheck);
        }

        /// <summary>
        /// 判断图片byte[]是否合法
        /// </summary>
        /// <param name="data">图片byte[]</param>
        /// <returns>是否合法</returns>
        public static bool IsImageFormat(byte[] data)
        {
            if (data == null || data.Length < 4)
            {
                return false;
            }
            string _fileClass = "";
            int _len = data.Length;
            try
            {
                _fileClass = data[0].ToString();
                _fileClass += data[1].ToString();
                _fileClass = _fileClass.Trim();
                if (_fileClass == "7173" || _fileClass == "13780")//7173:gif;13780:PNG;
                {
                    return true;
                }
                else      // Jpg,Jpeg
                {
                    byte[] _jpg = new byte[4];
                    _jpg[0] = 0xff;
                    _jpg[1] = 0xd8;
                    _jpg[2] = 0xff;
                    _jpg[3] = 0xd9;
                    if (data[0] == _jpg[0] && data[1] == _jpg[1]
                        && data[_len - 2] == _jpg[2] && data[_len - 1] == _jpg[3])
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 是否是整数
        /// </summary>
        /// <returns>Validation</returns>
        public static bool IsInt(this string data)
        {
            return RegexHelper.IsMatch(data, RegexPattern.IntCheck);
        }

        /// <summary>
        /// 判断是否是合法的IP4,IP6地址
        /// <para>eg: Assert.IsTrue(CheckHelper.IsIp46Address("192.168.1.1:8060"));</para>
        /// <para>    Assert.IsTrue(CheckHelper.IsIp46Address("[2001:0DB8:02de::0e13]:9010"));</para>
        /// </summary>
        /// <param name="data">需要判断的字符串</param>
        /// <returns>合法则返回host部分，若不合法则返回空</returns>
        public static bool IsIp46Address(string data)
        {
            bool _result = false;
            if (!string.IsNullOrEmpty(data))
            {
                UriHostNameType _hostType = Uri.CheckHostName(data);
                if (_hostType == UriHostNameType.Unknown)//譬如 "192.168.1.1:8060"或者[2001:0DB8:02de::0e13]:9010
                {
                    Uri _url;
                    if (Uri.TryCreate(string.Format("http://{0}", data), UriKind.Absolute, out _url))
                    {
                        _result = true;
                    }
                }
                else if (_hostType == UriHostNameType.IPv4 || _hostType == UriHostNameType.IPv6)
                {
                    _result = true;
                }
            }
            return _result;
        }

        /// <summary>
        /// 是否是IP
        /// </summary>
        /// <param name="data">需要检测到IP</param>
        /// <returns>Validation</returns>
        public static bool IsIp4Address(this string data)
        {
            return RegexHelper.IsMatch(data, RegexPattern.IpCheck);
        }

        /// <summary>
        /// 判断纬度是否合法
        /// </summary>
        /// <param name="data">纬度</param>
        /// <returns>是否合法</returns>
        public static bool IsLatitude(this double data)
        {
            if (data < -90 || data > 90) return false;
            return true;
        }

        /// <summary>
        /// 是否是内网IP
        /// </summary>
        /// <param name="ipAddress">ip地址</param>
        /// <returns></returns>
        public static bool IsLocalIp4(this string ipAddress)
        {
            /*
             * 知识：
             * Internet设计者保留了IPv4地址空间的一部份供专用地址使用,专用地址空间中的IPv4地址叫专用地址,这些地址永远不会被当做公用地址来分配,所以专用地址永远不会与公用地址重复.
             * IPv4专用地址如下：
             * IP等级 IP位置==>Class A 10.0.0.0-10.255.255.255
             * 默认子网掩码:255.0.0.0==>Class B 172.16.0.0-172.31.255.255
             * 默认子网掩码:255.255.0.0==>Class C 192.168.0.0-192.168.255.255
             * 默认子网掩码:255.255.255.0
             * 内网是可以上网的.内网需要一台服务器或路由器做网关,通过它来上网
             * 做网关的服务器有一个网关（服务器/路由器）的IP地址,其它内网电脑的IP可根据它来随意设置,前提是IP前三个数要跟它一样,第四个可从0-255中任选但要跟服务器的IP不同
             */
            bool _result = false;
            if (!string.IsNullOrEmpty(ipAddress) && ipAddress.IsIp4Address())
            {
                if (ipAddress.StartsWith("192.168.") || ipAddress.StartsWith("172.") || ipAddress.StartsWith("10."))
                {
                    _result = true;
                }
            }
            return _result;
        }

        /// <summary>
        /// 判断经度是否合法
        /// </summary>
        /// <param name="data">经度</param>
        /// <returns>是否合法</returns>
        public static bool IsLongitude(this double data)
        {
            if (data < -180 || data > 180) return false;
            return true;
        }

        /// <summary>
        /// 是否是数字
        /// <para>eg:CheckHelper.IsNumber("abc");==>false</para>
        /// </summary>
        /// <param name="data">判断字符串</param>
        /// <returns>是否是数字</returns>
        public static bool IsNumber(this string data)
        {
            return RegexHelper.IsMatch(data, RegexPattern.NumberCheck);
        }

        /// <summary>
        /// 是否是奇数
        /// </summary>
        /// <param name="data">需要判断项</param>
        /// <returns>是否是奇数</returns>
        public static bool IsOdd(this int data)
        {
            return ((data & 1) == 1);
        }

        /// <summary>
        /// 验证是否是邮政编码
        /// </summary>
        /// <param name="data">验证字符串</param>
        /// <returns>是否是邮政编码</returns>
        public static bool IsPoseCode(this string data)
        {
            return RegexHelper.IsMatch(data, RegexPattern.PostCodeCheck);
        }

        /// <summary>
        /// 是否是Smallint类型
        /// </summary>
        /// <param name="value">判断字符串</param>
        /// <param name="number">short</param>
        /// <returns>是否是Smallint类型</returns>
        public static bool IsSmallint(string value, out short number)
        {
            number = -1;
            return short.TryParse(value, out number);
        }

        /// <summary>
        /// 是否是Tinyint
        /// </summary>
        /// <param name="value">判断字符串</param>
        /// <param name="number">Tinyint</param>
        /// <returns>是否是Tinyint类型</returns>
        public static bool IsTinyint(string value, out byte number)
        {
            number = 0;
            return byte.TryParse(value, out number);
        }

        /// <summary>
        /// 验证是否是URL
        /// <para>eg:CheckHelper.IsURL("www.cnblogs.com/yan-zhiwei");==>true</para>
        /// </summary>
        /// <param name="data">验证字符串</param>
        /// <returns>是否是URL</returns>
        public static bool IsURL(this string data)
        {
            return RegexHelper.IsMatch(data, RegexPattern.URLCheck);
        }

        /// <summary>
        /// 检查设置的端口号是否正确
        /// </summary>
        /// <param name="port">端口号</param>
        /// <returns>端口号是否正确</returns>
        public static bool IsValidPort(this string port)
        {
            bool _result = false;
            int _minPORT = 0, _maxPORT = 65535;
            int _portValue = -1;
            if (int.TryParse(port, out _portValue))
            {
                _result = !((_portValue < _minPORT) || (_portValue > _maxPORT));
            }
            return _result;
        }

        /// <summary>
        /// 验证非空
        /// </summary>
        /// <param name="data">验证对象</param>
        /// <returns> 验证非空</returns>
        public static bool NotNull(object data)
        {
            return !(data == null);
        }

        #endregion Methods
    }
}