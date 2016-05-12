using MvcSolution.Core.Config;
using MvcSolution.Core.Log;
using MvcSolution.OA.Contract.Models;
using System.Data.Entity;
using YanZhiwei.DotNet4.Framework.Data;

namespace MvcSolution.OA.DAL
{
    public class OADbContext : DbContextBase
    {
        public OADbContext()
            : base(CachedConfigContext.Current.DaoConfig.OA, new LogDbContext())
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<OADbContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Branch> Branchs { get; set; }
    }
}