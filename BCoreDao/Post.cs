using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BCoreDao
{
    public class Post : Entity
    {
        public DateTime CreatedOn { set; get; } = DateTime.Now;
        public DateTime? Removed { set; get; }

        [Required]
        [MaxLength(450)]
        public string UserId { set; get; }        

        public virtual ICollection<Part> Parts { set; get; } = new List<Part>();
        public virtual ICollection<Comment> Comments { set; get; } = new List<Comment>();
        public virtual ICollection<PostHash> PostHashes { set; get; } = new List<PostHash>();
        public virtual ICollection<Star> Stars { set; get; } = new List<Star>();
    }        
}
