using EventHub.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventHub.Infrastructure.Data.Configurations
{
    internal class EventRegistrationConfiguration : IEntityTypeConfiguration<EventRegistration>
    {
        public void Configure(EntityTypeBuilder<EventRegistration> builder)
        {
            var seedData = new SeedData();

            builder.HasOne(x => x.User).WithMany(x => x.Registrations).OnDelete(DeleteBehavior.Restrict);

            builder.HasData(seedData.Registrations);
        }
    }
}
