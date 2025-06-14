using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vestis._03_Domain.Entities;

namespace Vestis._04_Infrasctructure.Mapping;

public class AddressEntityConfiguration : IEntityTypeConfiguration<AddressEntity>
{
    public void Configure(EntityTypeBuilder<AddressEntity> builder)
    {
        builder.ToTable(nameof(AddressEntity).Replace("Entity", string.Empty).Pluralize());
 
        builder.HasKey(a => a.Id);

        builder.Property<string>(a => a.ZipCode)
            .IsRequired(true)
            .HasMaxLength(10);
    }
}
