using System;

namespace YanZhiwei.DotNet2.TS.BaseLean
{
    public class FlagsLearn
    {
        public static void Demo1()
        {
            var _result = Enum.Parse(typeof(Deliver), "17");//==>CND|Airport
            Console.WriteLine(_result);
        }
    }

    [Flags]
    internal enum Deliver : byte
    {
        CND = 0x01,
        PJS = 0x02,
        SND = 0x04,
        PJN = 0x08,
        Airport = 0x10,
        EMS = 0x20
    }
}