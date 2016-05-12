[PetaPoco.TableName("roysched")]
[PetaPoco.ExplicitColumns]
public class Roysched
{
    [PetaPoco.Column]
    public string title_id { get; set; } // varchar(6), not null

    [PetaPoco.Column]
    public int? lorange { get; set; } // int, null

    [PetaPoco.Column]
    public int? hirange { get; set; } // int, null

    [PetaPoco.Column]
    public int? royalty { get; set; } // int, null
}