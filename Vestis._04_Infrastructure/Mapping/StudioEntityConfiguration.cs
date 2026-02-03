using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vestis._03_Domain.Entities;

namespace Vestis._04_Infrastructure.Mapping;

public class StudioEntityConfiguration : IEntityTypeConfiguration<StudioEntity>
{
    private const int _StudioMaxLength = 256;
    private const int _EmailMaxLength = 256;

    public void Configure(EntityTypeBuilder<StudioEntity> builder)
    {
        builder.ToTable(nameof(StudioEntity).Replace("Entity", string.Empty).Pluralize());

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(_StudioMaxLength);

        builder.Property(e => e.ContactEmail)
            .HasMaxLength(_EmailMaxLength);

        builder.Property(e => e.PhoneNumber)
            .HasMaxLength(20);
        
        builder.HasOne(x => x.Address)
            .WithOne()
            .HasForeignKey<StudioEntity>(x => x.AddressId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.ClientNoAction);
    }
}
