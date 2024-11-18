using EventHub.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EventHub.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventRegistration> EventRegistrations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Comment>().HasOne(x => x.User).WithMany(x => x.Comments).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Event>().HasOne(x => x.Creator).WithMany(x => x.CreatedEvents).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<EventRegistration>().HasOne(x => x.User).WithMany(x => x.Registrations).OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
