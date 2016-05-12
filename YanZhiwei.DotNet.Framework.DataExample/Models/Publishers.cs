[PetaPoco.TableName("publishers")]
[PetaPoco.PrimaryKey("pub_id", autoIncrement = false)]
[PetaPoco.ExplicitColumns]
public class Publishers
{
    [PetaPoco.Column]
    public string pub_id { get; set; } // char(4), not null

    [PetaPoco.Column]
    public string pub_name { get; set; } // varchar(40), null

    [PetaPoco.Column]
    public string city { get; set; } // varchar(20), null

    [PetaPoco.Column]
    public string state { get; set; } // char(2), null

    [PetaPoco.Column]
    public string country { get; set; } // varchar(30), null
}