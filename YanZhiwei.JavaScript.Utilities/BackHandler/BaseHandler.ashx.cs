using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using YanZhiwei.DotNet3._5.Utilities.Model;
using YanZhiwei.DotNet3._5.Utilities.WebForm.Jquery;

namespace YanZhiwei.JavaScript.Utilities.BackHandler
{
    /// <summary>
    /// Summary description for BaseHandler
    /// </summary>
    public class BaseHandler : IHttpHandler
    {
        public static IEnumerable<Person> GetPersons()
        {
            for (int i = 0; i < 57; i++)
            {
                yield return new Person
                {
                    Id = i,
                    Name = "name " + i
                };
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.ExecutePageQuery<Person>((pageLength, pageIndex, orderIndex, orderBy) =>
            {
                var persons = GetPersons();
                Func<Person, object> order = p =>
                {
                    if (orderIndex == 0)
                    {
                        return p.Id;
                    }
                    return p.Name;
                };
                if ("desc" == orderBy)
                {
                    persons = persons.OrderByDescending(order);
                }
                else
                {
                    persons = persons.OrderBy(order);
                }
                //错误测试
                //DataTablePageResult result = new DataTablePageResult();
                //result.ExecuteMessage = "测试错误";
                //result.ExecuteState = HttpStatusCode.BadGateway;
                //正确测试
                DataTablePageResult result = new DataTablePageResult();
                result.iTotalDisplayRecords = persons.Count();
                List<Person> _personList = new List<Person>();
                result.iTotalRecords = persons.Count();
                result.aaData = persons.Skip(pageIndex).Take(pageLength);
                result.ExecuteState = HttpStatusCode.OK;
                return result;
            });
            // // Those parameters are sent by the plugin
            // var iDisplayLength = int.Parse(context.Request["iDisplayLength"]);
            // var iDisplayStart = int.Parse(context.Request["iDisplayStart"]);
            // var iSortCol = int.Parse(context.Request["iSortCol_0"]);
            // var iSortDir = context.Request["sSortDir_0"];

            // // Fetch the data from a repository (in my case in-memory)
            // var persons = GetPersons();

            // // Define an order function based on the iSortCol parameter
            // Func<Person, object> order = p =>
            // {
            //     if (iSortCol == 0)
            //     {
            //         return p.Id;
            //     }
            //     return p.Name;
            // };

            // // Define the order direction based on the iSortDir parameter
            // if ("desc" == iSortDir)
            // {
            //     persons = persons.OrderByDescending(order);
            // }
            // else
            // {
            //     persons = persons.OrderBy(order);
            // }

            // // prepare an anonymous object for JSON serialization
            // var result = new
            // {
            //     iTotalRecords = persons.Count(),
            //     iTotalDisplayRecords = persons.Count(),
            //     aaData = persons
            //         .Skip(iDisplayStart)
            //         .Take(iDisplayLength)
            // };

            // //var serializer = new JavaScriptSerializer();
            //// var json = SerializationHelper.JsonSerialize(result);// serializer.Serialize(result);
            //  context.CreateResponse(result, System.Net.HttpStatusCode.OK);
            // //context.Response.ContentType = "application/json";
            // //context.Response.Write(json);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}