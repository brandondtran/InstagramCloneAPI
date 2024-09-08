using Microsoft.EntityFrameworkCore;

namespace InstagramCloneAPI.Models
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; init; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
//     public DbSet<Post> Posts { get; set; }
//     public DbSet<Comment> Comments { get; set; }
//     public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure one-to-many relationship between Chat and Message
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChatId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
