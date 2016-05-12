using System;

namespace YanZhiwei.DotNet.Core.AdminPanel.Models
{
    [PetaPoco.TableName("Base_UserRole")]
    [PetaPoco.PrimaryKey("UserRole_ID", autoIncrement = false)]
    [PetaPoco.ExplicitColumns]
    public class Base_UserRole
    {
        [PetaPoco.Column]
        public string UserRole_ID { get; set; } // varchar(50), not null

        [PetaPoco.Column]
        public string User_ID { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string Roles_ID { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public DateTime? CreateDate { get; set; } // datetime, null

        [PetaPoco.Column]
        public string CreateUserId { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string CreateUserName { get; set; } // varchar(50), null
    }
}