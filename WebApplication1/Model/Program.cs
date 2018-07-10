using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model
{
    public class Programs
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int BatchId { get; set; }
        [ForeignKey("BatchId")]
        public virtual Batch BatchDetails { get; set; }
    }
}
