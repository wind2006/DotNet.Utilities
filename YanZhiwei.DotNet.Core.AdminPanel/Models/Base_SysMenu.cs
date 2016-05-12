using System;

namespace YanZhiwei.DotNet.Core.AdminPanel.Models
{
    [PetaPoco.TableName("Base_SysMenu")]
    [PetaPoco.PrimaryKey("Menu_Id", autoIncrement = false)]
    [PetaPoco.ExplicitColumns]
    public class Base_SysMenu
    {
        [PetaPoco.Column]
        public string Menu_Id { get; set; } // varchar(50), not null

        [PetaPoco.Column]
        public string ParentId { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string Menu_Name { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string Menu_Title { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string Menu_Img { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public int? Menu_Type { get; set; } // int, null

        [PetaPoco.Column]
        public string NavigateUrl { get; set; } // varchar(200), null

        [PetaPoco.Column]
        public string Target { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public int? AllowEdit { get; set; } // int, null

        [PetaPoco.Column]
        public int? AllowDelete { get; set; } // int, null

        [PetaPoco.Column]
        public int? SortCode { get; set; } // int, null

        [PetaPoco.Column]
        public int? DeleteMark { get; set; } // int, null

        [PetaPoco.Column]
        public DateTime? CreateDate { get; set; } // datetime, not null

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