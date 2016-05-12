[PetaPoco.TableName("discounts")]
[PetaPoco.ExplicitColumns]
public class Discounts
{
    [PetaPoco.Column]
    public string discounttype { get; set; } // varchar(40), not null

    [PetaPoco.Column]
    public string stor_id { get; set; } // char(4), null

    [PetaPoco.Column]
    public short? lowqty { get; set; } // smallint, null

    [PetaPoco.Column]
    public short? highqty { get; set; } // smallint, null

    [PetaPoco.Column]
    public decimal? discount { get; set; } // decimal(4,2), not null
}