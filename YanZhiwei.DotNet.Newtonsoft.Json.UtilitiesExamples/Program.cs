using System;

namespace YanZhiwei.DotNet.Newtonsoft.Json.UtilitiesExamples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                JsonHelperExample.Demo();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}