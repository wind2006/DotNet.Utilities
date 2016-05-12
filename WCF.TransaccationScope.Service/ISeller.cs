using System.ServiceModel;
using WCF.TransaccationScope.Model;

namespace WCF.TransaccationScope.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface ISeller
    {
        [OperationContract(Name = "AddUser")]
        bool Add(User user, out string userId);

        [OperationContract(Name = "AddShop")]
        bool Add(Shop shop, string userId);

        [OperationContract]
        bool Add(Shop shop, User user);
    }
}