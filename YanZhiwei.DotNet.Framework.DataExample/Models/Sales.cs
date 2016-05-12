using System;

[PetaPoco.TableName("sales")]
[PetaPoco.PrimaryKey("stor_id,ord_num,title_id", autoIncrement = false)]
[PetaPoco.ExplicitColumns]
public class Sales
{
    [PetaPoco.Column]
    public string stor_id { get; set; } // char(4), not null

    [PetaPoco.Column]
    public string ord_num { get; set; } // varchar(20), not null

    [PetaPoco.Column]
    public DateTime? ord_date { get; set; } // datetime, not null

    [PetaPoco.Column]
    public short? qty { get; set; } // smallint, not null

    [PetaPoco.Column]
    public string payterms { get; set; } // varchar(12), not null

    [PetaPoco.Column]
    public string title_id { get; set; } // varchar(6), not null
}