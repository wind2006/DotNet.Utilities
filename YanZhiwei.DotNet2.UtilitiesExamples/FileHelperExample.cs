using System;
using System.Diagnostics;
using YanZhiwei.DotNet2.Utilities.Common;

namespace YanZhiwei.DotNet2.UtilitiesExamples
{
    public class FileHelperExample
    {
        public static void CopyLocalBigFile()
        {
            Stopwatch _watch = new Stopwatch();
            _watch.Start();
            if (FileHelper.CopyLargeFile(@"C:\Users\YanZh_000\Downloads\TheInterview.mp4", @"D:\The Interview(1080p).mp4", 1024 * 1024 * 5))
            {
                _watch.Stop();
                Console.WriteLine("拷贝完成,耗时：" + _watch.Elapsed.Seconds + "秒");
            }
            Console.Read();
        }
    }
}