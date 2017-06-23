using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [NotMapped]
        public List<Hash> Hashes
        {
            set
            {
                PostHashes.Clear();
                foreach (Hash hash in value)
                {
                    PostHashes.Add(new PostHash
                    {
                        HashId = hash.Id,
                        Hash = hash,
                        PostId = this.Id,
                        Post = this
                    });
                }
            }
            get
            {
                List<Hash> res = new List<Hash>();
                foreach (PostHash postHash in PostHashes)
                {
                    res.Add(postHash.Hash);
                }

                return res;
            }
        }
    }        
}
