using System;

[PetaPoco.TableName("titles")]
[PetaPoco.PrimaryKey("title_id", autoIncrement = false)]
[PetaPoco.ExplicitColumns]
public class Titles
{
    [PetaPoco.Column]
    public string title_id { get; set; } // varchar(6), not null

    [PetaPoco.Column]
    public string title { get; set; } // varchar(80), not null

    [PetaPoco.Column]
    public string type { get; set; } // char(12), not null

    [PetaPoco.Column]
    public string pub_id { get; set; } // char(4), null

    [PetaPoco.Column]
    public decimal? price { get; set; } // money, null

    [PetaPoco.Column]
    public decimal? advance { get; set; } // money, null

    [PetaPoco.Column]
    public int? royalty { get; set; } // int, null

    [PetaPoco.Column]
    public int? ytd_sales { get; set; } // int, null

    [PetaPoco.Column]
    public string notes { get; set; } // varchar(200), null

    [PetaPoco.Column]
    public DateTime? pubdate { get; set; } // datetime, not null
}