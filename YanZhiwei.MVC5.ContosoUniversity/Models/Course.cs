using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YanZhiwei.MVC5.ContosoUniversity.Models
{
    /// <summary>
    /// 课程实体
    /// </summary>
    /// 日期：2015-10-10 9:43
    /// 备注：
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "编号")]
        public int CourseID { get; set; }

        [Range(0, 5)]
        public int Credits { get; set; }

        public virtual Department Department { get; set; }

        public int DepartmentID { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public virtual ICollection<Instructor> Instructors { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }
    }
}