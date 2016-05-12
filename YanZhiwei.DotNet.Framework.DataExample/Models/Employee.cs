using System;
using YanZhiwei.DotNet.Framework.Contract;

[PetaPoco.TableName("employee")]
[PetaPoco.PrimaryKey("emp_id", autoIncrement = false)]
[PetaPoco.ExplicitColumns]
[Auditable]
public class Employee
{
    [PetaPoco.Column]
    public string emp_id { get; set; } // char(9), not null

    [PetaPoco.Column]
    public string fname { get; set; } // varchar(20), not null

    [PetaPoco.Column]
    public string minit { get; set; } // char(1), null

    [PetaPoco.Column]
    public string lname { get; set; } // varchar(30), not null

    [PetaPoco.Column]
    public short? job_id { get; set; } // smallint, not null

    [PetaPoco.Column]
    public byte? job_lvl { get; set; } // tinyint, null

    [PetaPoco.Column]
    public string pub_id { get; set; } // char(4), not null

    [PetaPoco.Column]
    public DateTime? hire_date { get; set; } // datetime, not null
}