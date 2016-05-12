using System.Collections.Generic;
using YanZhiwei.MVC5.ContosoUniversity.Models;

namespace YanZhiwei.MVC5.ContosoUniversity.ViewModels
{
    public class InstructorIndexData
    {
        public IEnumerable<Instructor> Instructors { get; set; }

        public IEnumerable<Course> Courses { get; set; }

        public IEnumerable<Enrollment> Enrollments { get; set; }
    }
}