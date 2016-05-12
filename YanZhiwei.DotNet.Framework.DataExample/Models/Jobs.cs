[PetaPoco.TableName("jobs")]
[PetaPoco.PrimaryKey("job_id")]
[PetaPoco.ExplicitColumns]
public class Jobs
{
    [PetaPoco.Column]
    public short? job_id { get; set; } // smallint, not null

    [PetaPoco.Column]
    public string job_desc { get; set; } // varchar(50), not null

    [PetaPoco.Column]
    public byte? min_lvl { get; set; } // tinyint, not null

    [PetaPoco.Column]
    public byte? max_lvl { get; set; } // tinyint, not null
}