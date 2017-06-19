using BCoreDao;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BCoreDal.SqlServer
{    
    public class SqlServerDbContext : IdentityDbContext<SqlServerAppUser>
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Hash> Hashes { set; get; }
        public DbSet<PostHash> PostHashes { set; get; }
        public DbSet<Star> Stars { set; get; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PostHash>().HasKey(f => f.Id);

            builder.Entity<PostHash>()
                .HasOne(pt => pt.Post)
                .WithMany(p => p.PostHashes)
                .HasForeignKey(pt => pt.PostId);

            builder.Entity<PostHash>()
                .HasOne(pt => pt.Hash)
                .WithMany(t => t.PostHashes)
                .HasForeignKey(pt => pt.HashId);

            builder.Entity<Hash>().HasIndex(f => f.Tag).IsUnique();
            builder.Entity<Hash>().Property(f => f.Tag).IsRequired();

            base.OnModelCreating(builder);
        }
    }
}
