using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vestis._03_Domain.Entities;

namespace Vestis._04_Infrasctructure.Mapping;

public class BodyMeasurementEntityConfiguration : IEntityTypeConfiguration<BodyMeasurementEntity>
{
    public void Configure(EntityTypeBuilder<BodyMeasurementEntity> builder)
    {
        builder.ToTable(nameof(BodyMeasurementEntity).Replace("Entity",string.Empty).Pluralize());

        builder.HasKey(x => x.Id);
    }
}
