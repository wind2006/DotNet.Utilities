using System.ServiceModel;

namespace WCF.MSMQ.Serivce
{
    [ServiceContract]
    public interface IOrder
    {
        [OperationContract(IsOneWay =true)]
        void Add(string order);
    }
}