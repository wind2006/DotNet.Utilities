using System;

namespace YanZhiwei.DotNet.Core.AdminPanel.Models
{
    [PetaPoco.TableName("Base_UserInfo")]
    [PetaPoco.PrimaryKey("User_ID", autoIncrement = false)]
    [PetaPoco.ExplicitColumns]
    public class Base_UserInfo
    {
        [PetaPoco.Column]
        public string User_ID { get; set; } // varchar(50), not null

        [PetaPoco.Column]
        public string User_Code { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string User_Account { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string User_Pwd { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string User_Name { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public int? User_Sex { get; set; } // int, null

        [PetaPoco.Column]
        public string Title { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string Email { get; set; } // varchar(20), null

        [PetaPoco.Column]
        public string Theme { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string Question { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string AnswerQuestion { get; set; } // varchar(50), null

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

        [PetaPoco.Column]
        public string User_Remark { get; set; } // varchar(max), null
    }
}