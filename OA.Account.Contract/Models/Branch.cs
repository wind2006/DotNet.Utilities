using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YanZhiwei.DotNet.Framework.Contract;

namespace MvcSolution.OA.Contract.Models
{
    [Serializable]
    [Table("Branch")]
    public class Branch : ModelBase
    {
        public Branch()
        {
        }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        [StringLength(300)]
        public string Desc { get; set; }

        public virtual List<Staff> Staffs { get; set; }

        public int ParentId { get; set; }

        public virtual Branch ParentBranch { get; set; }

        [ForeignKey("ParentId")]
        public virtual List<Branch> Embranchment { get; set; }
    }
}