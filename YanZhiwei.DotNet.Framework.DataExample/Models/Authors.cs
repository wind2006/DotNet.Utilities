using YanZhiwei.DotNet.Framework.Contract;

[PetaPoco.TableName("authors")]
[PetaPoco.PrimaryKey("au_id", autoIncrement = false)]
[PetaPoco.ExplicitColumns]
[Auditable]
public class Authors
{
    [PetaPoco.Column]
    public string au_id { get; set; } // varchar(11), not null

    [PetaPoco.Column]
    public string au_lname { get; set; } // varchar(40), not null

    [PetaPoco.Column]
    public string au_fname { get; set; } // varchar(20), not null

    [PetaPoco.Column]
    public string phone { get; set; } // char(12), not null

    [PetaPoco.Column]
    public string address { get; set; } // varchar(40), null

    [PetaPoco.Column]
    public string city { get; set; } // varchar(20), null

    [PetaPoco.Column]
    public string state { get; set; } // char(2), null

    [PetaPoco.Column]
    public string zip { get; set; } // char(5), null

    [PetaPoco.Column]
    public bool? contract { get; set; } // bit, not null
}