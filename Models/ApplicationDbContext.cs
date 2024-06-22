using Microsoft.EntityFrameworkCore;

namespace InstagramCloneAPI.Models
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; init; }
//     public DbSet<Post> Posts { get; set; }
//     public DbSet<Comment> Comments { get; set; }
//     public DbSet<Like> Likes { get; set; }
    }
}
