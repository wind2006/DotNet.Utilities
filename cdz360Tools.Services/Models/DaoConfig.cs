using System;
using YanZhiwei.DotNet.Core.Model;

namespace cdz360Tools.Services.Models
{
    [Serializable]
    public class DaoConfig : ConfigFileBase
    {
        public DaoConfig()
        {
        }

        public string Cdz360ConnectString { get; set; }
    }
}