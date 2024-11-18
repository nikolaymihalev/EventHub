using EventHub.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventHub.Infrastructure.Data.Configurations
{
    internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            var seedData = new SeedData();

            builder.HasOne(x => x.User).WithMany(x => x.Comments).OnDelete(DeleteBehavior.Restrict);

            builder.HasData(seedData.Comments);
        }
    }
}
