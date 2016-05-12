using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace YanZhiwei.DotNet2.Utilities.Common.Tests
{
    [TestClass()]
    public class FileHelperTests
    {

        [TestMethod()]
        public void GetFileNameOnlyTest()
        {
            string _actual = FileHelper.GetFileNameOnly(@"C:\yanzhiwei.docx");
            Assert.AreEqual<string>("yanzhiwei", _actual);
        }

        [TestMethod()]
        public void GetFileNameTest()
        {
            string _actual = FileHelper.GetFileName(@"C:\yanzhiwei.docx");
            Assert.AreEqual<string>("yanzhiwei.docx", _actual);
        }

        [TestMethod()]
        public void GetFileExTest()
        {
            string _actual = FileHelper.GetFileEx(@"C:\yanzhiwei.docx");
            Assert.AreEqual<string>(".docx", _actual);
        }

        [TestMethod()]
        public void GetExceptNameTest()
        {
            string _actual = FileHelper.GetExceptName(@"C:\yanzhiwei.docx");
            Assert.AreEqual<string>(@"C:\", _actual);
        }

        [TestMethod()]
        public void GetExceptExTest()
        {
            string _actual = FileHelper.GetExceptEx(@"C:\yanzhiwei.docx");
            Assert.AreEqual<string>(@"C:\yanzhiwei", _actual);
        }

        private static string TestFilePath = string.Empty;

        [TestInitialize]
        public void TestInit()
        {
            TestFilePath = string.Format(@"{0}\DB.sql", AppDomain.CurrentDomain.BaseDirectory);
        }

        [TestMethod()]
        public void ToBytesTest()
        {
            byte[] _actual = FileHelper.ParseFile(TestFilePath);
            Assert.IsNotNull(_actual);
        }

        [TestMethod()]
        public void ToFileTest()
        {
            string _outputFilePath = @"D:\DB.sql";
            byte[] _bytes = FileHelper.ParseFile(TestFilePath);
            FileHelper.ToFile(_bytes, _outputFilePath);
            bool _actual = File.Exists(_outputFilePath);
            Assert.IsTrue(_actual);
        }

        [TestMethod()]
        public void CreateTempPathTest()
        {
            string _path = FileHelper.ChangeFileType("jpg");
            bool _actual = _path.IndexOf("tmp") > 0;
            Assert.IsTrue(_actual);
        }

        [TestMethod()]
        public void GetSizeTest()
        {
            long _actual = FileHelper.GetSize(TestFilePath);
            Assert.AreEqual(1952, _actual);
        }

        [TestMethod()]
        public void GetKBSizeTest()
        {
            double _actual = FileHelper.GetKBSize(TestFilePath);
            Assert.AreEqual(1.90625, _actual);
        }

        [TestMethod()]
        public void GetMBSizeTest()
        {
            double _actual = FileHelper.GetMBSize(TestFilePath);
            Assert.AreEqual(0.001861572265625, _actual);
        }

        [TestMethod()]
        public void CopyToBakTest()
        {
            bool _actual = FileHelper.CopyToBak(TestFilePath);
            Assert.IsTrue(_actual);
        }

        [TestMethod()]
        public void CreateDirectoryTest()
        {
            FileHelper.CreateDirectory(@"C:\aa", "bb");
            bool _actual = Directory.Exists(@"C:\aa\bb");
            Assert.IsTrue(_actual);
        }

        [TestMethod()]
        public void CreatePathTest()
        {
            bool _actual = FileHelper.CreatePath(@"C:\aa\cc\dd\aa.xml");
            Assert.IsTrue(_actual);
        }
    }
}