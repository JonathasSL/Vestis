using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vestis._02_Domain.Entities.Mapping
{
    public class BodyMeasurementEntityConfiguration : IEntityTypeConfiguration<BodyMeasurementEntity>
    {
        public void Configure(EntityTypeBuilder<BodyMeasurementEntity> builder)
        {
            builder.ToTable(nameof(BodyMeasurementEntity).Replace("Entity",string.Empty).Pluralize());

            builder.HasKey(x => x.Id);
        }
    }
}
