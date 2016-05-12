using System;

namespace cdz360Tools.Services.Models
{
    /// <summary>
    /// 充电结束后上报
    /// </summary>
    /// 时间：2016-04-21 15:27
    /// 备注：
    [PetaPoco.TableName("TChargeOverHisRec")]
    [PetaPoco.ExplicitColumns]
    public class ChargeOverHisRec : ModelBase
    {
        [PetaPoco.Column]
        public byte? CurChargeType { get; set; } // tinyint

        [PetaPoco.Column]
        public byte? GunSeqNo { get; set; } // tinyint

        [PetaPoco.Column]
        public string DeviceSeqNo { get; set; } // varchar(20)

        [PetaPoco.Column]
        public string CardSeqNo { get; set; } // varchar(20)

        [PetaPoco.Column]
        public string UserOrderNo { get; set; } // varchar(30)

        [PetaPoco.Column]
        public decimal? PowerConsume { get; set; } // decimal(6,2)

        [PetaPoco.Column]
        public decimal? TotalAmount { get; set; } // decimal(6,2)

        [PetaPoco.Column]
        public decimal? BatteryOrgValue { get; set; } // decimal(6,2)

        [PetaPoco.Column]
        public decimal? BatteryCurValue { get; set; } // decimal(6,2)

        [PetaPoco.Column]
        public DateTime? OptStartTime { get; set; } // datetime

        [PetaPoco.Column]
        public DateTime? OptEndTime { get; set; } // datetime
    }
}