using System;

namespace YanZhiwei.DotNet.Core.AdminPanel.Models
{
    [PetaPoco.TableName("Base_Organization")]
    [PetaPoco.PrimaryKey("Organization_ID", autoIncrement = false)]
    [PetaPoco.ExplicitColumns]
    public class Base_Organization
    {
        [PetaPoco.Column]
        public string Organization_ID { get; set; } // varchar(50), not null

        [PetaPoco.Column]
        public string Organization_Code { get; set; } // varchar(20), null

        [PetaPoco.Column]
        public string Organization_Name { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string Organization_InnerPhone { get; set; } // varchar(20), null

        [PetaPoco.Column]
        public string Organization_OuterPhone { get; set; } // varchar(20), null

        [PetaPoco.Column]
        public string Organization_Manager { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string Organization_AssistantManager { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string Organization_Fax { get; set; } // varchar(20), null

        [PetaPoco.Column]
        public string Organization_Zipcode { get; set; } // varchar(20), null

        [PetaPoco.Column]
        public string Organization_Address { get; set; } // varchar(200), null

        [PetaPoco.Column]
        public string ParentId { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string Organization_Remark { get; set; } // varchar(200), null

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