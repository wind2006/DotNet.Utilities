using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YanZhiwei.DotNet.Framework.Contract;

namespace MvcSolution.Cms.Contract.Models
{
    [Serializable]
    [Table("Tag")]
    public class Tag : ModelBase
    {
        public Tag()
        {
        }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        public int Hits { get; set; }

        public virtual List<Article> Articles { get; set; }
    }
}