using System;

namespace YanZhiwei.DotNet.Dapper.UtilitiesTests
{
    public class Person
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int age { get; set; }
        public DateTime registerDate { get; set; }
        public string address { set; get; }
    }
}