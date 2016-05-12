using System;
using System.Collections.Generic;
namespace YanZhiwei.DotNet3._5.UtilitiesTests.Model
{
    public class Person
    {
        public string Name { get; set; }

        public byte Age { get; set; }

        public string Address { get; set; }

        public DateTime Birth { get; set; }

        public DateTime Login { get; set; }
        public List<OptRecord> OptRecordList { get; set; }

    }

    public class OptRecord
    {
        public string Guid { get; set; }
        public DateTime OptTime { get; set; }
    }
}