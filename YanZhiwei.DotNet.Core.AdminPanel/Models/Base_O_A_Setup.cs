namespace YanZhiwei.DotNet.Core.AdminPanel.Models
{
    [PetaPoco.TableName("Base_O_A_Setup")]
    [PetaPoco.PrimaryKey("Setup_ID", autoIncrement = false)]
    [PetaPoco.ExplicitColumns]
    public class Base_O_A_Setup
    {
        [PetaPoco.Column]
        public string Setup_ID { get; set; } // varchar(50), not null

        [PetaPoco.Column]
        public string User_ID { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string Setup_IName { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string NavigateUrl { get; set; } // varchar(200), null

        [PetaPoco.Column]
        public string Target { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string Setup_Img { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string Setup_Remak { get; set; } // varchar(200), null

        [PetaPoco.Column]
        public int? SortCode { get; set; } // int, null
    }
}