using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vestis._02_Domain.Entities.Mapping;

public class StudioMembershipEntityConfiguration : IEntityTypeConfiguration<StudioMembershipEntity>
{
    public void Configure(EntityTypeBuilder<StudioMembershipEntity> builder)
    {
        builder.ToTable(nameof(StudioMembershipEntity).Replace("Entity",string.Empty).Pluralize());

        builder.HasKey(x => x.Id);
    }
}
