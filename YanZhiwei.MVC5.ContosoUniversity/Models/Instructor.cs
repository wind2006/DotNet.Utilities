using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YanZhiwei.MVC5.ContosoUniversity.Models
{
    public class Instructor
    {
        public virtual ICollection<Course> Courses { get; set; }
        [Column("FirstName"), Display(Name = "First Name"), StringLength(50, MinimumLength = 1)]
        public string FirstMidName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return LastName + ", " + FirstMidName; }
        }

        [DataType(DataType.Date), Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        public int ID { get; set; }

        [Display(Name = "Last Name"), StringLength(50, MinimumLength = 1)]
        public string LastName { get; set; }
        public virtual OfficeAssignment OfficeAssignment { get; set; }
    }
}