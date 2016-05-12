namespace cdz360Tools.Services.Models
{
    [PetaPoco.TableName("TChargingPileOffline_HisRec")]
    [PetaPoco.ExplicitColumns]
    public class ChargingPileOffline_HisRec : ModelBase
    {
        [PetaPoco.Column]
        public byte? CurChargeType { get; set; } // tinyint

        [PetaPoco.Column]
        public string DeviceSeqNo { get; set; } // varchar(20)

        [PetaPoco.Column]
        public byte? GunSeqNo { get; set; } // tinyint

        [PetaPoco.Column]
        public string CardSeqNo { get; set; } // varchar(20)

        [PetaPoco.Column]
        public decimal? CardBalance { get; set; } // decimal(6,2)

        [PetaPoco.Column]
        public byte? UserChooseMode { get; set; } // tinyint

        [PetaPoco.Column]
        public int? ModeValue { get; set; } // int

        [PetaPoco.Column]
        public decimal? PowerConsume { get; set; } // decimal(6,2)

        [PetaPoco.Column]
        public decimal? TotalAmount { get; set; } // decimal(6,2)
    }
}