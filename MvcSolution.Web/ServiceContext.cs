using MvcSolution.Cms.Contract;
using MvcSolution.Core.Cache;
using MvcSolution.Crm.BLL;
using MvcSolution.GMS.Contract;
using MvcSolution.OA.Contract;
using YanZhiwei.DotNet.Core.Cache;

namespace MvcSolution.Web
{
    public class ServiceContext
    {
        public static ServiceContext Current
        {
            get
            {
                return CacheHelper.GetItem<ServiceContext>("ServiceContext", () => new ServiceContext());
            }
        }

        public IAccountService AccountService
        {
            get
            {
                return AppServiceHelper.Instance.CreateService<IAccountService>();
            }
        }

        public ICmsService CmsService
        {
            get
            {
                return AppServiceHelper.Instance.CreateService<ICmsService>();
            }
        }

        public ICrmService CrmService
        {
            get
            {
                return AppServiceHelper.Instance.CreateService<ICrmService>();
            }
        }

        public IOAService OAService
        {
            get
            {
                return AppServiceHelper.Instance.CreateService<IOAService>();
            }
        }
    }
}