using EventHub.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventHub.Infrastructure.Data.Configurations
{
    internal class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            var seedData = new SeedData();

            builder.HasOne(x => x.Creator).WithMany(x => x.CreatedEvents).OnDelete(DeleteBehavior.Restrict);
            builder.HasData(seedData.Events);
        }
    }
}
