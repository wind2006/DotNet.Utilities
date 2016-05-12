using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YanZhiwei.DotNet.Framework.Contract;

namespace MvcSolution.Crm.BLL.Models
{
    [Table("Project")]
    public class Project : ModelBase
    {
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<VisitRecord> VisitRecords { get; set; }
    }
}