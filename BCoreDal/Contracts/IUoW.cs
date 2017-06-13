using BCoreDal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCoreDal.Contracts
{
    public interface IUoW
    {
        IRepository<Post> PostRepository { get; }
        IRepository<Part> PartRepository { get; }
        IRepository<Comment> CommentRepository { get; }
        IRepository<Hash> HashRepository { get; }
        IRepository<PostHash> PostHashRepository { get; }
        IRepository<Star> StarRepository { get; }
    }
}
