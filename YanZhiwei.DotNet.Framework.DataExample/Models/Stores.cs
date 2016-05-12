[PetaPoco.TableName("stores")]
[PetaPoco.PrimaryKey("stor_id", autoIncrement = false)]
[PetaPoco.ExplicitColumns]
public class Stores
{
    [PetaPoco.Column]
    public string stor_id { get; set; } // char(4), not null

    [PetaPoco.Column]
    public string stor_name { get; set; } // varchar(40), null

    [PetaPoco.Column]
    public string stor_address { get; set; } // varchar(40), null

    [PetaPoco.Column]
    public string city { get; set; } // varchar(20), null

    [PetaPoco.Column]
    public string state { get; set; } // char(2), null

    [PetaPoco.Column]
    public string zip { get; set; } // char(5), null
}