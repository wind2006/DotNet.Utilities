using System;

namespace YanZhiwei.DotNet.Core.AdminPanel.Models
{
    [PetaPoco.TableName("Base_Roles")]
    [PetaPoco.PrimaryKey("Roles_ID", autoIncrement = false)]
    [PetaPoco.ExplicitColumns]
    public class Base_Roles
    {
        [PetaPoco.Column]
        public string Roles_ID { get; set; } // varchar(50), not null

        [PetaPoco.Column]
        public string ParentId { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string Roles_Name { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string Role_Restriction { get; set; } // varchar(200), null

        [PetaPoco.Column]
        public string Roles_Remark { get; set; } // varchar(200), null

        [PetaPoco.Column]
        public int? AllowEdit { get; set; } // int, null

        [PetaPoco.Column]
        public int? AllowDelete { get; set; } // int, null

        [PetaPoco.Column]
        public int? SortCode { get; set; } // int, null

        [PetaPoco.Column]
        public int? DeleteMark { get; set; } // int, null

        [PetaPoco.Column]
        public DateTime? CreateDate { get; set; } // datetime, null

        [PetaPoco.Column]
        public string CreateUserId { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string CreateUserName { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public DateTime? ModifyDate { get; set; } // datetime, null

        [PetaPoco.Column]
        public string ModifyUserId { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string ModifyUserName { get; set; } // varchar(50), null
    }
}