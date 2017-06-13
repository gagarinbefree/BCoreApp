using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BCoreDal.Entities
{
    public class Part : Entity
    {
        [Required]
        public DateTime CreatedOn { set; get; } = DateTime.Now;

        public string Value { set; get; }

        // 0 - text
        // 1 - image
        // 2 - code
        [Required]
        public int PartType { set; get; }

        [ForeignKey("Post")]
        public Guid PostId { set; get; }
        public virtual Post Post { set; get; }        
    }
}
