using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vestis._03_Domain.Entities;

namespace Vestis._04_Infrasctructure.Mapping;

public class MeasurementEntryEntityConfiguration : IEntityTypeConfiguration<MeasurementEntryEntity>
{
    public void Configure(EntityTypeBuilder<MeasurementEntryEntity> builder)
    {
        builder.ToTable(nameof(MeasurementEntryEntity).Replace("Entity",string.Empty).Pluralize());

        builder.HasKey(x => x.Id);

        builder.Property<string>(x => x.Name)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property<double>(x => x.Value)
            .IsRequired();
    }
}
