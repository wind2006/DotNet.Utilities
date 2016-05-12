namespace cdz360Tools.Services.Models
{
    /// <summary>
    /// 充电桩在线状态表
    /// </summary>
    /// 时间：2016-04-14 11:40
    /// 备注：
    [PetaPoco.TableName("TChargingPileOnlineStatus")]
    [PetaPoco.ExplicitColumns]
    public class ChargingPileOnlineStatus : ModelBase
    {
        [PetaPoco.Column]
        public string DeviceSeqNo { get; set; } // varchar(20)

        [PetaPoco.Column]
        public byte? DeviceType { get; set; } // tinyint

        [PetaPoco.Column]
        public byte? GunSeqNo { get; set; } // tinyint

        [PetaPoco.Column]
        public decimal? ChargingVoltage { get; set; } // decimal(6,2)

        [PetaPoco.Column]
        public decimal? ChargingCurrent { get; set; } // decimal(6,2)

        [PetaPoco.Column]
        public decimal? PowerConsume { get; set; } // decimal(6,2)

        [PetaPoco.Column]
        public decimal? SOC { get; set; } // decimal(6,2)

        [PetaPoco.Column]
        public string OrderSeqNo { get; set; } // varchar(30)
    }
}