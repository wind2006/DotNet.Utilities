[PetaPoco.TableName("titleauthor")]
[PetaPoco.PrimaryKey("au_id,title_id", autoIncrement = false)]
[PetaPoco.ExplicitColumns]
public class Titleauthor
{
    [PetaPoco.Column]
    public string au_id { get; set; } // varchar(11), not null

    [PetaPoco.Column]
    public string title_id { get; set; } // varchar(6), not null

    [PetaPoco.Column]
    public byte? au_ord { get; set; } // tinyint, null

    [PetaPoco.Column]
    public int? royaltyper { get; set; } // int, null
}