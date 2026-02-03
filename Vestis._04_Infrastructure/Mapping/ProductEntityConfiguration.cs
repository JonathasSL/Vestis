using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vestis._03_Domain.Entities;

namespace Vestis._04_Infrastructure.Mapping;

public sealed class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired();

        builder.HasOne(x => x.Studio)
            .WithMany(s => s.Products)
            .HasForeignKey(x => x.StudioId)
            .IsRequired()
            .OnDelete(DeleteBehavior.ClientNoAction);

        builder.HasMany(x => x.ProductUnits)
            .WithOne(u => u.Product)
            .HasForeignKey(u => u.ProductId)
            .IsRequired()
            .OnDelete(DeleteBehavior.ClientNoAction);
    }
}
