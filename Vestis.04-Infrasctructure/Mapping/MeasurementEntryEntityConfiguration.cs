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

        builder.HasKey(m => m.Id);

        builder.Property<string>(m => m.Name)
            .IsRequired(true)
            .HasMaxLength(64);

        builder.Property<double>(m => m.Value)
            .IsRequired(true);

        builder.HasOne(m => m.BodyMeasurement)
            .WithMany(b => b.Entries)
            .HasForeignKey(m => m.BodyMeasurementId)
            .IsRequired(true);
    }
}
