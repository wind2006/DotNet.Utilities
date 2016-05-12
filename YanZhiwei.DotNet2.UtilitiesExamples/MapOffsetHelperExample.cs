using System;
using System.Collections;
using YanZhiwei.DotNet2.Utilities.Common;
using YanZhiwei.DotNet2.Utilities.Models;

namespace YanZhiwei.DotNet2.UtilitiesExamples
{
    internal class MapOffsetHelperExample
    {
        public static void Demo1()
        {
            Console.WriteLine("开始初始化纠偏数据库：" + DateTime.Now);
            string _path = @"D:\YanZhiwei.DotNet.Utilities\YanZhiwei.DotNet.Utilities\YanZhiwei.DotNet2.Utilities\Data\offset.dat";
            MapOffsetDataHelper _offsetDbHelper = new MapOffsetDataHelper(_path);
            ArrayList _mapCoordList = _offsetDbHelper.GetMapCoordArrayList();
            Console.WriteLine("初始化纠偏数据库结束：" + DateTime.Now);
            Console.WriteLine("共有纠偏数据数据:" + (_mapCoordList == null ? 0 : _mapCoordList.Count) + "条");
            MapOffsetHelper _offsetHelper = new MapOffsetHelper(_mapCoordList);
            LatLngPoint _wgs84 = new LatLngPoint(34.121, 115.21212);
            Console.WriteLine(string.Format("WGS84坐标 Lat:{0} Lon:{1}", _wgs84.LatY, _wgs84.LonX));
            LatLngPoint _gcj02 = _offsetHelper.WGS84ToGCJ02(_wgs84);
            Console.WriteLine(string.Format("GCJ02坐标 Lat:{0} Lon:{1}", _gcj02.LatY, _gcj02.LonX));
        }
    }
}