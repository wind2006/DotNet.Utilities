using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using YanZhiwei.DotNet2.Utilities.Models;

namespace YanZhiwei.DotNet2.Utilities.Common.Tests
{
    [TestClass()]
    public class ExifHelperTests
    {
        [TestMethod()]
        public void GetExifMetaDataTest()
        {
            ExifHelper _exifHelper = new ExifHelper();
            string _path = string.Format(@"{0}\TestSource\WP_20150912_18_11_56_Pro.jpg", AppDomain.CurrentDomain.BaseDirectory);
            ExifMetadata _mateData = _exifHelper.GetMetaData(_path);
            Assert.AreEqual("2015:09:12 18:11:56\0", _mateData.DatePictureTaken.DisplayValue);
        }
    }
}