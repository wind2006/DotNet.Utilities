using System;
using System.Xml.Serialization;
using YanZhiwei.DotNet.Core.Model;

namespace cdz360Tools.Services.Models
{
    public class JobItem : ConfigNodeBase
    {
        /// <summary>
        /// 定时任务名称
        /// </summary>
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// 执行任务时间
        /// </summary>
        [XmlAttribute(AttributeName = "ExecuteTime")]
        public DateTime ExecuteTime { get; set; }

        /// <summary>
        /// 执行任务周期，单位小时
        /// </summary>
        [XmlAttribute(AttributeName = "ExecutePeriod")]
        public int ExecutePeriod { get; set; }

        /// <summary>
        /// 需要执行的方法
        /// </summary>
        [XmlAttribute(AttributeName = "RunAction")]
        public string RunAction { get; set; }
    }
}