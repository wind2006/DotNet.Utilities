using System;

namespace YanZhiwei.DotNet.Core.AdminPanel.Models
{
    [PetaPoco.TableName("Base_UserRight")]
    [PetaPoco.PrimaryKey("UserRight_ID", autoIncrement = false)]
    [PetaPoco.ExplicitColumns]
    public class Base_UserRight
    {
        [PetaPoco.Column]
        public string UserRight_ID { get; set; } // varchar(50), not null

        [PetaPoco.Column]
        public string User_ID { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string Menu_Id { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public DateTime? CreateDate { get; set; } // datetime, null

        [PetaPoco.Column]
        public string CreateUserId { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string CreateUserName { get; set; } // varchar(50), null
    }
}