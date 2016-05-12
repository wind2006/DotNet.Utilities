using System.ServiceModel;

namespace cdz360Tools.Contracts
{
    public interface ICzd360Callback
    {
        [OperationContract(IsOneWay = true)]
        void CheckoutChannelMessage(string message);

        /// <summary>
        /// 充电预约回调通知
        /// </summary>
        /// <param name="deviceSeqNo">充电桩号</param>
        /// <param name="gunSqNo">充电枪号</param>
        /// <param name="orderSeqNo">预约单号</param>
        /// <param name="orderStatus">预约是否成功</param>
        /// 时间：2016-04-21 15:59
        /// 备注：
        [OperationContract(IsOneWay = true)]
        void ChargingPileOrderMesage(string deviceSeqNo, short gunSqNo, string orderSeqNo, bool orderStatus);
    }
}