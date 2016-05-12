using System;

namespace YanZhiwei.DotNet.Core.AdminPanel.Models
{
    [PetaPoco.TableName("Base_StaffOrganize")]
    [PetaPoco.PrimaryKey("StaffOrganize_Id", autoIncrement = false)]
    [PetaPoco.ExplicitColumns]
    public class Base_StaffOrganize
    {
        [PetaPoco.Column]
        public string StaffOrganize_Id { get; set; } // varchar(50), not null

        [PetaPoco.Column]
        public string Organization_ID { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string User_ID { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public DateTime? CreateDate { get; set; } // datetime, null

        [PetaPoco.Column]
        public string CreateUserId { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string CreateUserName { get; set; } // varchar(50), null
    }
}