using eGathering.Domain.Members;
using eGathering.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eGathering.Persistence.Configurations;

internal class MemberEntityTypeConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> memberBuilder)
    {
        memberBuilder.ToTable(nameof(Member));
        memberBuilder.HasKey(x => x.Id);
        memberBuilder.OwnsOne(x => x.FullName)
                     .Property(x => x.FirstName)
                     .HasColumnName("FirstName")
                     .IsUnicode(true)
                     .HasMaxLength(FullName.MaxLength);
        memberBuilder.OwnsOne(x => x.FullName)
                     .Property(x => x.LastName)
                     .HasColumnName("LastName")
                     .IsUnicode(true)
                     .HasMaxLength(FullName.MaxLength);
        memberBuilder.OwnsOne(x => x.Email)
                     .Property(x => x.Value)
                     .HasColumnName("Email")
                     .IsUnicode(false)
                     .HasMaxLength(Email.MaxLength);
    }
}