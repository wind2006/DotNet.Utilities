using System.ComponentModel.DataAnnotations;

namespace YanZhiwei.MVC5.ContosoUniversity.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }

    /// <summary>
    /// 学生实体
    /// </summary>
    /// 日期：2015-10-10 9:44
    /// 备注：
    public class Enrollment
    {
        public virtual Course Course { get; set; }
        public int CourseID { get; set; }
        public int EnrollmentID { get; set; }

        [DisplayFormat(NullDisplayText = "没有成绩")]
        public Grade? Grade { get; set; }

        public virtual Student Student { get; set; }
        public int StudentID { get; set; }
    }
}