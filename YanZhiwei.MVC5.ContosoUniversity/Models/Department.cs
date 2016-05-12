using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YanZhiwei.MVC5.ContosoUniversity.Models
{
    public class Department
    {
        public virtual Instructor Administrator { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Budget { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public int DepartmentID { get; set; }

        public int? InstructorID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "起始日期")]
        public DateTime StartDate { get; set; }
    }
}