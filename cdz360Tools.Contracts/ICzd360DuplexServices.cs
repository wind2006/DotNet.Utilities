using System;
using System.ServiceModel;

namespace cdz360Tools.Contracts
{
    [ServiceContract(CallbackContract = typeof(ICzd360Callback))]
    public interface ICzd360DuplexServices
    {
        /// <summary>
        /// 检查双工通道
        /// </summary>
        /// <param name="time">The time.</param>
        /// 时间：2016-04-15 16:15
        /// 备注：
        [OperationContract]
        SvrRetMessage CheckoutChannel(DateTime time);

        /// <summary>
        /// 充电桩预约
        /// </summary>
        /// <param name="gunSeqNo">枪编号</param>
        /// <param name="OrderChargeType">预约充电方式，交流，直流</param>
        /// <param name="deviceSeqNo">充电桩号</param>
        /// <param name="cardSeqNo">预约卡号</param>
        /// <param name="phoneNumber">手机号码</param>
        /// <param name="startTime">预约开始时间</param>
        /// <param name="endTime">预约结束时间</param>
        [OperationContract]
        SvrRetMessage ChargingPileOrder(byte gunSeqNo, byte OrderChargeType, string deviceSeqNo, string cardSeqNo, string phoneNumber, DateTime startTime, DateTime endTime);
    }
}