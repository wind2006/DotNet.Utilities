namespace YanZhiwei.DotNet2.Utilities.WebForm
{
    using System.Net;
    using System.Net.Sockets;
    using System.Web;

    /// <summary>
    /// NetWork 帮助类
    /// </summary>
    public static class NetWorkHelper
    {
        #region Methods

        /// <summary>
        /// 获取本地ip4地址
        /// </summary>
        /// <returns>ip4地址</returns>
        public static string GetLocalIP4()
        {
            string _ipv4Address = string.Empty;

            foreach (IPAddress currentIPAddress in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (currentIPAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    _ipv4Address = currentIPAddress.ToString();
                    break;
                }
            }

            return _ipv4Address;
        }

        /// <summary>
        /// 获取访客ip4地址
        /// </summary>
        /// <param name="request">HttpRequest</param>
        /// <returns>ip地址</returns>
        public static string GetVisitorIp4(this HttpRequest request)
        {
            string _ip4address;
            _ip4address = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(_ip4address))
            {
                _ip4address = request.ServerVariables["REMOTE_ADDR"];
            }

            return _ip4address;
        }

        #endregion Methods
    }
}