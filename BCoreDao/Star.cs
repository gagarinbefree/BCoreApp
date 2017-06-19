using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BCoreDao
{
    public class Star : Entity
    {
        [Required]
        public DateTime CreatedOn { set; get; } = DateTime.Now;        

        [Required]
        [MaxLength(450)]
        public string UserId { set; get; }

        [ForeignKey("Post")]
        public Guid PostId { set; get; }

        public virtual Post Post { set; get; }
    }
}
