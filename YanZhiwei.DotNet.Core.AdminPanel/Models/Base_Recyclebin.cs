using System;

namespace YanZhiwei.DotNet.Core.AdminPanel.Models
{
    [PetaPoco.TableName("Base_Recyclebin")]
    [PetaPoco.PrimaryKey("Recyclebin_ID", autoIncrement = false)]
    [PetaPoco.ExplicitColumns]
    public class Base_Recyclebin
    {
        [PetaPoco.Column]
        public string Recyclebin_ID { get; set; } // varchar(50), not null

        [PetaPoco.Column]
        public string Recyclebin_Name { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string Recyclebin_Database { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string Recyclebin_Table { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string Recyclebin_FieldKey { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string Recyclebin_EventField { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string Recyclebin_Remark { get; set; } // varchar(max), null

        [PetaPoco.Column]
        public DateTime? CreateDate { get; set; } // datetime, null

        [PetaPoco.Column]
        public string CreateUserId { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string CreateUserName { get; set; } // varchar(50), null
    }
}