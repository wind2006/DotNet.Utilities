using System;

namespace YanZhiwei.DotNet.Newtonsoft.Json.UtilitiesExamples.Models
{
    [Serializable]
    internal class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public DateTime AddTime { get { return DateTime.Now; } }
    }
}