using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YanZhiwei.MVC5.ContosoUniversity.Models
{
    /// <summary>
    /// 学生实体
    /// </summary>
    /// 日期：2015-10-10 9:43
    /// 备注：
    public class Student
    {
        /*
         * 1. 导航属性通常被定义为virtual，使他们能获得某些实体框架的功能，比如延迟加载的优势。如果某个导航属性可以包含多个实体(如多对多或一对多关系)，它的类型必须可以进行增删改操作，比如ICollection。
         */

        public int ID { get; set; }

        [Required]
        [Display(Name = "姓")]
        [StringLength(50, MinimumLength = 1)]
        public string LastName { get; set; }

        [StringLength(50, ErrorMessage = "名字不能超过50个字符")]
        [Column("FirstName")]
        [Display(Name = "名")]
        public string FirstMidName { get; set; }

        [Display(Name = "注册日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }

        [Display(Name = "全名")]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstMidName;
            }
        }

        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public virtual OfficeAssignment OfficeAssignment { get; set; }
    }
}