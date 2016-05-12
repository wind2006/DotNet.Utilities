using System;

namespace cdz360Tools.Services.Models
{
    /// <summary>
    /// 充电桩设备信息表
    /// </summary>
    /// 时间：2016-04-14 11:39
    /// 备注：
    [PetaPoco.TableName("TChargingPileInfo")]
    [PetaPoco.PrimaryKey("ObjID", autoIncrement = false)]
    [PetaPoco.ExplicitColumns]
    public class ChargingPileInfo : ModelBase
    {
        [PetaPoco.Column]
        public Guid? CabObjID { get; set; } // uniqueidentifier, null

        [PetaPoco.Column]
        public string IpAddress { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public byte? ConnectType { get; set; } // tinyint, null

        [PetaPoco.Column]
        public byte? DeviceType { get; set; } // tinyint, not null

        [PetaPoco.Column]
        public string DeviceSeqNo { get; set; } // varchar(20), not null

        [PetaPoco.Column]
        public string Address { get; set; } // nvarchar(50), not null

        [PetaPoco.Column]
        public DateTime? LastCommTime { get; set; } // datetime, not null

        [PetaPoco.Column]
        public string Meno { get; set; } // nvarchar(50), null
    }
}