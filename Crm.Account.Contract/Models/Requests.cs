using System;
using YanZhiwei.DotNet.Framework.Contract;

namespace MvcSolution.Crm.BLL.Models
{
    public class ProjectRequest : BusinessRequest
    {
        public string Name { get; set; }
    }

    public class CustomerRequest : BusinessRequest
    {
        public CustomerRequest()
        {
            this.Customer = new Customer();
        }

        public Customer Customer { get; set; }
    }

    public class VisitRecordRequest : BusinessRequest
    {
        public VisitRecordRequest()
        {
            this.VisitRecord = new VisitRecord();
        }

        public int? StartHour { get; set; }
        public int? EndHour { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public VisitRecord VisitRecord { get; set; }
    }

    public class UserAnalysis
    {
        public string UserName { get; set; }
        public int VisitRecordCount { get; set; }
        public int CustomerCount { get; set; }
    }

    public class VisitStatistics
    {
        public int Hour { get; set; }
        public int VisitRecordCount { get; set; }
        public int VisitCount { get; set; }
        public int TelCount { get; set; }
    }
}