using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using YanZhiwei.DotNet2.Utilities.Core;

namespace YanZhiwei.DotNet2.UtilitiesExamples
{
    class ProjectTraceListenerDemo
    {
        //static void Main(string[] args)
        //{
        //    try
        //    {
        //        Trace.Listeners.Clear();  //清除系统监听器 (就是输出到Console的那个)
        //        Trace.Listeners.Add(new ProjectTraceListener(@"d:\Error.log")); //添加ProjectTraceListener实例

        //        Test();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    finally
        //    {

        //        Console.ReadLine();
        //    }
        //}
        private static void Test()
        {
            try
            {
                int i = 0;
                Console.WriteLine(5 / i); //出现除0异常
            }
            catch (Exception ex)
            {
                Trace.Write(ex, "计算员工工资出现异常");
            }
        }

    }
}
