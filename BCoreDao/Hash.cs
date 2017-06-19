using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BCoreDao
{
    public class Hash : Entity
    {
        [Required]
        public DateTime CreatedOn { set; get; } = DateTime.Now;

        [MaxLength(450)]
        public string UserId { set; get; }

        public string Tag { set; get; }        
        public virtual ICollection<PostHash> PostHashes { set; get; } = new List<PostHash>();
    }
}
