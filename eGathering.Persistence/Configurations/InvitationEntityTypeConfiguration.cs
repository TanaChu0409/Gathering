using eGathering.Domain.Invitations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eGathering.Persistence.Configurations;

internal class InvitationEntityTypeConfiguration : IEntityTypeConfiguration<Invitation>
{
    public void Configure(EntityTypeBuilder<Invitation> invitationBuilder)
    {
        invitationBuilder.ToTable(nameof(Invitation));
        invitationBuilder.HasKey(x => x.Id);
    }
}