using Microsoft.EntityFrameworkCore;

namespace Forum.Models
{
    class ForumDb : DbContext
    {
        public ForumDb(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Token> Tokens { get; set; } = null!;
        public DbSet<Catalog> Catalogs { get; set; } = null!;
        public DbSet<File> Files { get; set; } = null!;
        public DbSet<Question> Questions { get; set; } = null!;
        public DbSet<Reply> Replies { get; set; } = null!;
    }
}