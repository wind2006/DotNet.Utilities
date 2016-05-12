using System;
using YanZhiwei.DotNet.Core.Model;

namespace cdz360Tools.Services.Models
{
    [Serializable]
    public class SocketConfig : ConfigFileBase
    {
        public SocketConfig()
        {
        }
        /// <summary>
        /// Socket配置项
        /// </summary>
        public SocketItem SocketItem
        {
            get; set;
        }
    }
}