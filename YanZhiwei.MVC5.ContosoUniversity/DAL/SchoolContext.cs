using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using YanZhiwei.MVC5.ContosoUniversity.Models;

namespace YanZhiwei.MVC5.ContosoUniversity.DAL
{
    public class SchoolContext : DbContext
    {
        /*
         * 1.DbSet<TEntity> : 在实体框架中，一个实体集对应数据库中的表，一个实体对应数据表中的一行。
         * 2.
         */

        public SchoolContext()
            : base("SchoolContext")
        {
            /*
             * 你同样可以通过传递连接字符串而不是存储在web.config文件的连接字符串名称本身来指定连接。如果你不指定连接字符串或一个明确的名称，实体框架 将假定连接字符串名称和类名称一致，即在本例中，默认的连接字符串名称为SchoolContext，同你显示声明的一致。 
             */
        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }

        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //}
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*
             * OnModelCreating方法中的modelBuilder.Convertions.Remove被用来防止生成复数表名。如果你不这样 做，在数据库中生成的数据表将被命名为Students,Courses及Entrollments。相反，在本例中我们的表名是 Student,Course及Enrollment。对于表名称是否应该使用复数或单数命名模式并没有明确的要求。在本教程中我们将使用单数形式。重要 的一点是，你可以选择任意的命名方式——通过是否注释掉该行代码。
             */
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Instructors).WithMany(i => i.Courses)
                .Map(t => t.MapLeftKey("CourseID")
                    .MapRightKey("InstructorID")
                    .ToTable("CourseInstructor"));
        }
    }
}