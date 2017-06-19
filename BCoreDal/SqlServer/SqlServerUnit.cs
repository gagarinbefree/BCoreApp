using System;
using BCoreDao;
using BCoreAbstractions;

namespace BCoreDal.SqlServer
{
    public class SqlServerUnit : IUoW, IDisposable
    {
        private bool disposed = false;

        private SqlServerDbContext _db;

        private IRepository<Post> _postRepository;
        public IRepository<Post> PostRepository
        {
            get { return _postRepository ?? (_postRepository = new SqlServerRepository<Post>(_db)); }
        }

        private IRepository<Part> _partRepository;
        public IRepository<Part> PartRepository
        {
            get { return _partRepository ?? (_partRepository = new SqlServerRepository<Part>(_db)); }
        }

        private IRepository<Comment> _commentRepository;
        public IRepository<Comment> CommentRepository
        {
            get { return _commentRepository ?? (_commentRepository = new SqlServerRepository<Comment>(_db)); }
        }

        private IRepository<Hash> _hashRepository;
        public IRepository<Hash> HashRepository
        {
            get { return _hashRepository ?? (_hashRepository = new SqlServerRepository<Hash>(_db)); }
        }

        private IRepository<PostHash> _postHashRepository;
        public IRepository<PostHash> PostHashRepository
        {
            get { return _postHashRepository ?? (_postHashRepository = new SqlServerRepository<PostHash>(_db)); }
        }

        private IRepository<Star> _starRepository;
        public IRepository<Star> StarRepository
        {
            get { return _starRepository ?? (_starRepository = new SqlServerRepository<Star>(_db)); }
        }

        public SqlServerUnit(SqlServerDbContext db)
        {
            _db = db;
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
