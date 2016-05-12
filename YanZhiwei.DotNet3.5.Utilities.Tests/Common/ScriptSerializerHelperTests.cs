using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using YanZhiwei.DotNet3._5.Utilities.Core.ScriptConverter;
using YanZhiwei.DotNet3._5.UtilitiesTests.Model;

namespace YanZhiwei.DotNet3._5.Utilities.Common.Tests
{
    [TestClass()]
    public class ScriptSerializerHelperTests
    {
        [TestMethod()]
        public void ToJsonTest()
        {
            Person _personA = new Person() { Name = "YanZhiweiA", Age = 10, Address = "shanghaiA", Login = DateTime.Now, Birth = new DateTime(2012, 10, 10, 1, 1, 1) };
            Person _personB = new Person() { Name = "YanZhiweiB", Age = 11, Address = "shanghaiB", Login = DateTime.Now, Birth = new DateTime(2012, 10, 10, 1, 1, 1) };
            IList<Person> _personList = new List<Person>();
            _personList.Add(_personA);
            _personList.Add(_personB);
            DateTimeConverter _datetimeConvert = new DateTimeConverter("yyyy-MM-dd");
            string _actual = SerializationHelper.JsonSerialize<Person>(_personList, _datetimeConvert);
            string _expect = "[{\"Name\":\"YanZhiweiA\",\"Age\":10,\"Address\":\"shanghaiA\",\"Birth\":{\"DateTime\":\"2012-10-10\"},\"Login\":{\"DateTime\":\"2015-05-07\"},\"OptRecordList\":null},{\"Name\":\"YanZhiweiB\",\"Age\":11,\"Address\":\"shanghaiB\",\"Birth\":{\"DateTime\":\"2012-10-10\"},\"Login\":{\"DateTime\":\"2015-05-07\"},\"OptRecordList\":null}]";
            Assert.AreEqual<string>(_expect, _actual);
        }

        [TestMethod()]
        public void FromJsonToIEnumerableTest()
        {
            Person _personA = new Person() { Name = "YanZhiweiA", Age = 10, Address = "shanghaiA" };
            Person _personB = new Person() { Name = "YanZhiweiB", Age = 11, Address = "shanghaiB" };
            List<Person> _expected = new List<Person>();
            _expected.Add(_personA);
            _expected.Add(_personB);
            string _jsonString = "[{'Name':'YanZhiweiA','Age':10,'Address':'shanghaiA'},{'Name':'YanZhiweiB','Age':11,'Address':'shanghaiB'}]";
            List<Person> _result = (List<Person>)SerializationHelper.JsonDeserialize(_jsonString);
            bool _actual = _expected.SequenceEqual(_result, new PersonCompare());
            Assert.IsTrue(_actual);
        }

        [TestMethod()]
        public void ConvertTimeJsonTest()
        {
            string _actual = SerializationHelper.ParseJsonDateTime(@"[{'getTime':'\/Date(1419564257428)\/'}]", "yyyyMMdd hh:mm:ss");
            Assert.AreEqual("[{'getTime':'20141226 11:24:17'}]", _actual);
        }
    }

    public class PersonCompare : IEqualityComparer<Person>
    {
        public bool Equals(Person x, Person y)
        {
            return (x.Age == y.Age) && (x.Address == y.Address) && (x.Name == y.Name);
        }

        public int GetHashCode(Person obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}