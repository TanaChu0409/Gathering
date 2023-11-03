using eGathering.Domain.Attendees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eGathering.Persistence.Configurations;

internal class AttendeeEntityTypeConfiguration : IEntityTypeConfiguration<Attendee>
{
    public void Configure(EntityTypeBuilder<Attendee> attendeeBuilder)
    {
        attendeeBuilder.ToTable(nameof(Attendee));
        attendeeBuilder.HasKey(x => x.Id);
    }
}