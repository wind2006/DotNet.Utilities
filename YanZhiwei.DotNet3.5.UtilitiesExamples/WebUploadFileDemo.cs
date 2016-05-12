using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YanZhiwei.DotNet3._5.Utilities.WebForm.Core;

namespace YanZhiwei.DotNet3._5.Utilities.Examples
{
    class WebUploadFileDemo
    {
        static void Main(string[] args)
        {

            try
            {
                WebUploadFile _uploadFile = new WebUploadFile();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
