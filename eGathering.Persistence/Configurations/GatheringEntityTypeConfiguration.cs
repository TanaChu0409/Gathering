using eGathering.Domain.Gatherings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eGathering.Persistence.Configurations;

internal class GatheringEntityTypeConfiguration : IEntityTypeConfiguration<Gathering>
{
    public void Configure(EntityTypeBuilder<Gathering> gatheringBuilder)
    {
        gatheringBuilder.ToTable(nameof(Gathering));
        gatheringBuilder.HasKey(x => x.Id);
        gatheringBuilder.Ignore(x => x.Creator);
        gatheringBuilder.Property(x => x.Name)
                        .IsUnicode(true)
                        .HasMaxLength(500);
        gatheringBuilder.Property(x => x.Location)
                        .IsUnicode(true)
                        .HasMaxLength(500);
        gatheringBuilder.HasMany(x => x.Attendees)
                        .WithOne(x => x.Gathering)
                        .HasForeignKey(x => x.GatheringId);
        gatheringBuilder.HasMany(x => x.Invitations)
                        .WithOne(x => x.Gathering)
                        .HasForeignKey(x => x.GatheringId);
        gatheringBuilder.HasOne(x => x.Creator)
                        .WithMany(x => x.Gatherings)
                        .HasForeignKey(x => x.CreatorId);
        gatheringBuilder.Property<uint>("Version")
                        .IsRowVersion();
    }
}