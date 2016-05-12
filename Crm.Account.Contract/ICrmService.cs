using MvcSolution.Crm.BLL.Models;
using System;
using System.Collections.Generic;
using YanZhiwei.DotNet.Framework.Contract;

namespace MvcSolution.Crm.BLL
{
    public interface ICrmService
    {
        Project GetProject(int id);

        IEnumerable<Project> GetProjectList(ProjectRequest request = null);

        void SaveProject(Project project);

        void DeleteProject(List<int> ids);

        Customer GetCustomer(int id);

        IEnumerable<Customer> GetCustomerList(CustomerRequest request = null);

        void SaveCustomer(Customer customer);

        void DeleteCustomer(List<int> ids);

        VisitRecord GetVisitRecord(int id);

        IEnumerable<VisitRecord> GetVisitRecordList(VisitRecordRequest request = null);

        void SaveVisitRecord(VisitRecord visitRecord);

        void DeleteVisitRecord(List<int> ids);

        IEnumerable<City> GetCityList(BusinessRequest request = null);

        IEnumerable<Area> GetAreaList(BusinessRequest request = null);

        IEnumerable<UserAnalysis> GetUserAnalysis(DateTime startDate, DateTime endDate);

        IEnumerable<VisitStatistics> GetVisitStatistics(DateTime startDate, DateTime endDate);
    }
}