using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YanZhiwei.DotNet.Core.Model;

namespace MvcSolution.Core.Config.Models
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    [Serializable]
    public class SystemConfig : ConfigFileBase
    {
        public SystemConfig()
        {
        }

        #region 序列化属性
        public int UserLoginTimeoutMinutes { get; set; }
        #endregion
    }
}
