using cdz360Tools.Services.Contract;
using cdz360Tools.Services.Models;

namespace cdz360Tools.Services.BLL
{
    /// <summary>
    /// 充电桩基于Sql Server数据存储服务实现
    /// </summary>
    /// 时间：2016-04-14 17:23
    /// 备注：
    public class Cdz360DbService : ICdz360DbService
    {
        /// <summary>
        /// 保存充电桩注册信息
        /// </summary>
        /// <param name="item">The project.</param>
        /// 时间：2016-04-13 14:57
        /// 备注：
        public bool SaveChargingPileInfo(ChargingPileInfo item)
        {
            using (var dbContext = new Cdz360DbContext())
            {
                var _finded = dbContext.SingleOrDefault<ChargingPileInfo>("where DeviceSeqNo=@0", item.DeviceSeqNo);
                if (_finded == null)
                    return dbContext.Insert(item) != null;
                else {
                    item.ObjID = _finded.ObjID;
                    item.CabObjID = _finded.CabObjID;
                    return dbContext.Update(item) > 0;
                }
            }
        }

        /// <summary>
        /// 保存充电桩在线状态信息
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        /// 时间：2016-04-14 11:41
        /// 备注：
        public bool SaveChargingPileOnlineStatus(ChargingPileOnlineStatus item)
        {
            using (var dbContext = new Cdz360DbContext())
            {
                return dbContext.Insert(item) != null;
            }
        }

        /// <summary>
        /// 保存充电桩充电记录(充电完成后）
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        /// 时间：2016-04-14 15:07
        /// 备注：
        public bool SaveChargeOverRec(ChargeOverHisRec item)
        {
            using (var dbContext = new Cdz360DbContext())
            {
                return dbContext.Insert(item) != null;
            }
        }

        /// <summary>
        /// 保存线下充电上报记录
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        /// 时间：2016-04-14 17:19
        /// 备注：
        public bool SaveChargingPileOffline(ChargingPileOffline_HisRec item)
        {
            using (var dbContext = new Cdz360DbContext())
            {
                return dbContext.Insert(item) != null;
            }
        }

        /// <summary>
        /// 保存预约充电
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        /// 时间：2016/4/15 星期五 23:48
        /// 备注：
        public bool SaveChargingPileOrder(ChargingPileOrder_HisRec item)
        {
            using (var dbContext = new Cdz360DbContext())
            {
                return dbContext.Insert(item) != null;
            }
        }
    }
}