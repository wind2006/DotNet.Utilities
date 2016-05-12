using System;
using System.Collections.Generic;
using YanZhiwei.DotNet.Newtonsoft.Json.Utilities;
using YanZhiwei.DotNet.Newtonsoft.Json.UtilitiesExamples.Models;

namespace YanZhiwei.DotNet.Newtonsoft.Json.UtilitiesExamples
{
    public class JsonHelperExample
    {
        public static void Demo()
        {
            List<Person> _personList = new List<Person>();
            for (int i = 0; i < 100; i++)
            {
                _personList.Add(new Person() { Age = i, Name = "Yanzhiwei" + i });
            }
            string _jsonString = JsonHelper.Serialize(_personList);
            Console.WriteLine("列化JSON字符串：\r\n" + _jsonString);
            object _toJsonObj = JsonHelper.Deserialize<List<Person>>(_jsonString);
            Console.WriteLine(_toJsonObj == null ? "反序列化失败." : "反序列化成功.");
        }
    }
}