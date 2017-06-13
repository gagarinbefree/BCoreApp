using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BCoreDal.Entities
{
    public class PostHash : Entity
    {
        [ForeignKey("Post")]
        public Guid PostId { set; get; }
        public Post Post { set; get; }

        [ForeignKey("Hash")]
        public Guid HashId { set; get; }
        public Hash Hash { set; get; }             
    }
}
