using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cdz360Tools.Clients
{
    public class CommunResult
    {
        /// <summary>
        /// 通信状态
        /// </summary>
        public CommState CommunState
        {
            get;
            set;
        }

        /// <summary>
        /// 通信失败相关信息
        /// </summary>
        public string FailedMessage
        {
            get;
            set;
        }
    }
}
