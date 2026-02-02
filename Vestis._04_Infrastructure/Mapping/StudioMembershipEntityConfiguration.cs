using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vestis._03_Domain.Entities;

namespace Vestis._04_Infrastructure.Mapping;

public class StudioMembershipEntityConfiguration : IEntityTypeConfiguration<StudioMembershipEntity>
{
    public void Configure(EntityTypeBuilder<StudioMembershipEntity> builder)
    {
        builder.ToTable(nameof(StudioMembershipEntity).Replace("Entity", string.Empty).Pluralize());

        builder.HasKey(x => x.Id);

        builder.HasOne(s => s.User)
            .WithMany(u => u.StudioMemberships)
            .HasForeignKey(s => s.UserId)
            .IsRequired();

        builder.HasOne(s => s.Studio)
            .WithMany(st => st.StudioMemberships)
            .HasForeignKey(s => s.StudioId)
            .IsRequired();

        builder.HasIndex(x => new { x.UserId, x.StudioId }).IsUnique();

        /*
        builder.HasOne(s => s.Client)
            .WithOne()
            .HasForeignKey<StudioMembershipEntity>(s => s.ClientId)
            .IsRequired(true);
        builder.HasOne(s => s.Role)
            .WithOne()
            .HasForeignKey<StudioMembershipEntity>(s => s.RoleId)
            .IsRequired(true);
        */
    }
}
