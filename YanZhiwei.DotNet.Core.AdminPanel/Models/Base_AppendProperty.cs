using System;

namespace YanZhiwei.DotNet.Core.AdminPanel.Models
{
    [PetaPoco.TableName("Base_AppendProperty")]
    [PetaPoco.PrimaryKey("Property_ID", autoIncrement = false)]
    [PetaPoco.ExplicitColumns]
    public class Base_AppendProperty
    {
        [PetaPoco.Column]
        public string Property_ID { get; set; } // varchar(50), not null

        [PetaPoco.Column]
        public string Property_Function { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string Property_FunctionUrl { get; set; } // varchar(200), null

        [PetaPoco.Column]
        public string Property_Control_ID { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string Property_Name { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public int? Property_Control_Type { get; set; } // int, null

        [PetaPoco.Column]
        public string Property_Control_DataSource { get; set; } // varchar(max), null

        [PetaPoco.Column]
        public string Property_Control_Length { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public int? Property_Control_Maxlength { get; set; } // int, null

        [PetaPoco.Column]
        public string Property_Control_Style { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string Property_Control_Validator { get; set; } // varchar(200), null

        [PetaPoco.Column]
        public int? Property_Colspan { get; set; } // int, null

        [PetaPoco.Column]
        public string Property_Event { get; set; } // varchar(200), null

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