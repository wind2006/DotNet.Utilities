using System;

namespace YanZhiwei.DotNet.Core.AdminPanel.Models
{
    [PetaPoco.TableName("Base_SysLoginlog")]
    [PetaPoco.ExplicitColumns]
    public class Base_SysLoginlog
    {
        [PetaPoco.Column]
        public string SYS_LOGINLOG_ID { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string SYS_LOGINLOG_IP { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public DateTime? SYS_LOGINLOG_TIME { get; set; } // datetime, null

        [PetaPoco.Column]
        public string User_Account { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public int? SYS_LOGINLOG_STATUS { get; set; } // int, null

        [PetaPoco.Column]
        public string OWNER_address { get; set; } // varchar(200), null
    }
}