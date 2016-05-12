using System;

namespace cdz360Tools.Services.Models
{
    [PetaPoco.TableName("TChargingPileOrder_HisRec")]
    [PetaPoco.PrimaryKey("ObjID", autoIncrement = false)]
    [PetaPoco.ExplicitColumns]
    public class ChargingPileOrder_HisRec : ModelBase
    {
        [PetaPoco.Column]
        public byte? GunSeqNo { get; set; } // tinyint, not null

        [PetaPoco.Column]
        public byte? OrderChargeType { get; set; } // tinyint, not null

        [PetaPoco.Column]
        public string DeviceSeqNo { get; set; } // varchar(20), not null

        [PetaPoco.Column]
        public string UserPhoneNo { get; set; } // varchar(30), not null

        [PetaPoco.Column]
        public string OrderSeqNo { get; set; } // varchar(24), not null

        [PetaPoco.Column]
        public bool? OrderType { get; set; } // bit, not null

        [PetaPoco.Column]
        public DateTime? OrderStartTime { get; set; } // datetime, not null

        [PetaPoco.Column]
        public DateTime? OrderEndTime { get; set; } // datetime, not null

        [PetaPoco.Column]
        public DateTime? ServerTime { get; set; } // datetime, not null

        [PetaPoco.Column]
        public byte? OptStatus { get; set; } // tinyint, not null
    }
}