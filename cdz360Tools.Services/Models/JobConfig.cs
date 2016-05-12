using System;
using YanZhiwei.DotNet.Core.Model;

namespace cdz360Tools.Services.Models
{
    [Serializable]
    public class JobConfig : ConfigFileBase
    {
        public JobConfig()
        {
        }

        public JobItem[] JobItems { get; set; }
    }
}