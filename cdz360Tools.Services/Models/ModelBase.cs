using System;

namespace cdz360Tools.Services.Models
{
    /// <summary>
    /// 涉及到数据库操作基类
    /// </summary>
    /// 时间：2016-04-13 15:23
    /// 备注：
    public class ModelBase
    {
        public ModelBase()
        {
            ObjID = Guid.NewGuid();
            TheDate = DateTime.Now;
            Status = 1;
        }

        [PetaPoco.Column]
        public Guid? ObjID { get; set; } // uniqueidentifier

        [PetaPoco.Column]
        public DateTime? TheDate { get; set; } // datetime

        [PetaPoco.Column]
        public byte? Status { get; set; } // tinyint
    }
}