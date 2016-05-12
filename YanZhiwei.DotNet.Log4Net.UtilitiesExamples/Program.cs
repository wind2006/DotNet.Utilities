using System;
using System.Threading;
using YanZhiwei.DotNet.Log4Net.Utilities;

namespace YanZhiwei.DotNet.Log4Net.UtilitiesExamples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(DateTime.Now.ToLongTimeString());
                Log4NetHelper.SetLogger("AdoNetLogger");

                while (true)
                {
                    Thread.Sleep(5000);
                    Console.WriteLine(DateTime.Now.ToLongTimeString());
                    Log4NetHelper.WriteDebug("Debug 你好");
                    Log4NetHelper.WriteError("ERROR");
                    Log4NetHelper.WriteFatal("Fatal");
                    Log4NetHelper.WriteWarn("Warn");
                    Log4NetHelper.WriteInfo("Info");
                }

                //Console.WriteLine(DateTime.Now.ToLongTimeString());
                //ConsoleLogHelper.WriteDebug("Debug");
                //ConsoleLogHelper.WriteDebug("Debug");
                //ConsoleLogHelper.WriteError("ERROR");
                //ConsoleLogHelper.WriteFatal("Fatal");
                //ConsoleLogHelper.WriteWarn("Warn");
                //ConsoleLogHelper.WriteInfo("Info");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine(DateTime.Now.ToLongTimeString());
                Console.ReadLine();
            }
        }
    }
}