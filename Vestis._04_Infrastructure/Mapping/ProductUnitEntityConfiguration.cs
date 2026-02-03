using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vestis._03_Domain.Entities;

namespace Vestis._04_Infrastructure.Mapping;

public sealed class ProductUnitEntityConfiguration : IEntityTypeConfiguration<ProductUnitEntity>
{
    public void Configure(EntityTypeBuilder<ProductUnitEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code)
            .IsRequired();

        builder.Property(x => x.RentedPrice)
            .HasPrecision(18, 2);

        builder.HasOne(x => x.Product)
            .WithMany(p => p.ProductUnits)
            .HasForeignKey(x => x.ProductId)
            .IsRequired()
            .OnDelete(DeleteBehavior.ClientNoAction);
    }
}
