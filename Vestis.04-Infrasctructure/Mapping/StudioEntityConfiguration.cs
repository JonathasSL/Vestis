using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vestis._03_Domain.Entities;

namespace Vestis._04_Infrasctructure.Mapping;

public class StudioEntityConfiguration : IEntityTypeConfiguration<StudioEntity>
{
    public void Configure(EntityTypeBuilder<StudioEntity> builder)
    {
        builder.ToTable(nameof(StudioEntity).Replace("Entity", string.Empty).Pluralize());

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.ContactEmail)
            .HasMaxLength(100);

        builder.Property(e => e.PhoneNumber)
            .HasMaxLength(20);

        builder.HasOne(x => x.Address)
            .WithOne()
            .HasForeignKey<StudioEntity>(x => x.AddressId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.Clients)
            .WithOne(x => x.Studio)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
