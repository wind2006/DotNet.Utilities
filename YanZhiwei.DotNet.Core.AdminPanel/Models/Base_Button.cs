using System;

namespace YanZhiwei.DotNet.Core.AdminPanel.Models
{
    [PetaPoco.TableName("Base_Button")]
    [PetaPoco.PrimaryKey("Button_ID", autoIncrement = false)]
    [PetaPoco.ExplicitColumns]
    public class Base_Button
    {
        [PetaPoco.Column]
        public string Button_ID { get; set; } // varchar(50), not null

        [PetaPoco.Column]
        public string Button_Name { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string Button_Title { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string Button_Img { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string Button_Code { get; set; } // varchar(200), null

        [PetaPoco.Column]
        public int? SortCode { get; set; } // int, null

        [PetaPoco.Column]
        public string Button_Type { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string Button_Remak { get; set; } // varchar(200), null

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