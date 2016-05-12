using System;
using System.Configuration;

namespace YanZhiwei.DotNet2.Utilities.Tests.Model
{
    public class Person : ConfigurationSection, IComparable<Person>
    {
        [ConfigurationProperty("Name")]
        public string Name
        {
            get { return (string)this["Name"]; }
            set { this["Name"] = value; }
        }

        [ConfigurationProperty("Age")]
        public byte Age
        {
            get { return (byte)this["Age"]; }
            set { this["Age"] = value; }
        }

        [ConfigurationProperty("Address")]
        public string Address
        {
            get { return (string)this["Address"]; }
            set { this["Address"] = value; }
        }

        public int CompareTo(Person other)
        {
            if (Age > other.Age)
            {
                return 1;
            }
            else if (Age == other.Age)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
        public override string ToString()
        {
            return string.Format("{0};{1};{2}", Name, Age, Address);
        }
    }
}