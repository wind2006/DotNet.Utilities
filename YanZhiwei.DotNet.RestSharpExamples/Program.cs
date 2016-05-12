using RestSharp;
using System;
using System.Net;
using YanZhiwei.DotNet.Newtonsoft.Json.Utilities;
using YanZhiwei.DotNet.RestSharpExamples.Models;

namespace YanZhiwei.DotNet.RestSharpExamples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Demo1();
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

        //private async<IRestResponse> Demo2()
        //{
        //    var client = new RestClient("http://www.haoweis.com:8820/howell/v10/ecamera_service/pu/System/PUs/P0226GQ5V01700000000/Channels/1/PTZ/Direction");

        //    var request = new RestRequest(Method.PUT);
        //    Control _control = new Control();
        //    _control.Direction = "Right";
        //    string jsonToSend = JsonHelper.Serialize(_control);
        //    request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
        //    request.RequestFormat = DataFormat.Json;

        //    var aa =await client.GetResponseAsync(request);
        //}

        private static void Demo1()
        {
            var client = new RestClient("http://www.haoweis.com:8820/howell/v10/ecamera_service/pu/System/PUs/P0226GQ5V01700000000/Channels/1/PTZ/Direction");

            var request = new RestRequest(Method.PUT);
            Control _control = new Control();
            _control.Direction = "Right";
            string jsonToSend = JsonHelper.Serialize(_control);
            request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            client.ExecuteAsync(request, response =>
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine("ok");
                }
                else
                {
                    // NOK
                    Console.WriteLine("nok" + response);
                }
            });
        }
    }
}