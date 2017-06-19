using BCoreAbstractions;
using BCoreDao;

namespace BCoreDal
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
