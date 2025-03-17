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
    }
}
