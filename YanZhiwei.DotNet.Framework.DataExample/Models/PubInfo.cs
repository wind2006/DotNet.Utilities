[PetaPoco.TableName("pub_info")]
[PetaPoco.PrimaryKey("pub_id", autoIncrement = false)]
[PetaPoco.ExplicitColumns]
public class PubInfo
{
    [PetaPoco.Column]
    public string pub_id { get; set; } // char(4), not null

    [PetaPoco.Column]
    public byte[] logo { get; set; } // image, null

    [PetaPoco.Column]
    public string pr_info { get; set; } // text, null
}