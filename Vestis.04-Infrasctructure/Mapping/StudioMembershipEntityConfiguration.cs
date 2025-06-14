using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vestis._03_Domain.Entities;

namespace Vestis._04_Infrasctructure.Mapping;

public class StudioMembershipEntityConfiguration : IEntityTypeConfiguration<StudioMembershipEntity>
{
    public void Configure(EntityTypeBuilder<StudioMembershipEntity> builder)
    {
        builder.ToTable(nameof(StudioMembershipEntity).Replace("Entity",string.Empty).Pluralize());

        builder.HasKey(x => x.Id);

        builder.HasOne(s => s.User)
            .WithOne()
            .HasForeignKey<StudioMembershipEntity>(s => s.UserId)
            .IsRequired(true);

        builder.HasOne(s => s.Studio)
            .WithOne()
            .HasForeignKey<StudioMembershipEntity>(s => s.StudioId)
            .IsRequired(true);

        builder.HasOne(s => s.Client)
            .WithOne()
            .HasForeignKey<StudioMembershipEntity>(s => s.ClientId)
            .IsRequired(true);

        builder.HasOne(s => s.Role)
            .WithOne()
            .HasForeignKey<StudioMembershipEntity>(s => s.RoleId)
            .IsRequired(true);
    }
}
