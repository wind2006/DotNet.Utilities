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
    public class SettingConfig : ConfigFileBase
    {
        public SettingConfig()
        {
        }

        #region 序列化属性
        public String WebSiteTitle { get; set; }
        public String WebSiteDescription { get; set; }
        public String WebSiteKeywords { get; set; }
        #endregion
    }
}
