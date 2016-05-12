namespace cdz360Tools.Services.Contract
{
    using Models;

    public interface ICdz360DbService
    {
        #region Methods

        /// <summary>
        /// 保存充电桩设备信息
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        /// 时间：2016-04-14 15:06
        /// 备注：
        bool SaveChargingPileInfo(ChargingPileInfo item);

        /// <summary>
        /// 保存充电桩在线信息（心跳）
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        /// 时间：2016-04-14 15:06
        /// 备注：
        bool SaveChargingPileOnlineStatus(ChargingPileOnlineStatus item);

        /// <summary>
        /// 保存充电桩充电记录(充电完成后）
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        /// 时间：2016-04-14 15:06
        /// 备注：
        bool SaveChargeOverRec(ChargeOverHisRec item);

        /// <summary>
        /// 保存线下充电上报记录
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        /// 时间：2016-04-14 17:19
        /// 备注：
        bool SaveChargingPileOffline(ChargingPileOffline_HisRec item);

        /// <summary>
        /// 保存预约充电
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        /// 时间：2016/4/15 星期五 23:47
        /// 备注：
        bool SaveChargingPileOrder(ChargingPileOrder_HisRec item);

        #endregion Methods
    }
}