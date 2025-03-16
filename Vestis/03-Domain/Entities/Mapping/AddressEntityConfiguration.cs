using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vestis._02_Domain.Entities.Mapping
{
    public class AddressEntityConfiguration : IEntityTypeConfiguration<AddressEntity>
    {
        public void Configure(EntityTypeBuilder<AddressEntity> builder)
        {
            builder.ToTable(nameof(AddressEntity).Replace("Entity", string.Empty).Pluralize());

            builder.HasKey(x => x.Id);

            builder.Property<string>(x => x.ZipCode)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}
