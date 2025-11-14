using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vestis._03_Domain.Entities;

namespace Vestis._04_Infrasctructure.Mapping;

public class ClientEntityConfiguration : IEntityTypeConfiguration<ClientEntity>
{
    public void Configure(EntityTypeBuilder<ClientEntity> builder)
    {
        builder.ToTable(nameof(ClientEntity).Replace("Entity", string.Empty).Pluralize());

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(128);
        /*
        builder.HasOne(c => c.Studio)
            .WithMany(s => s.Clients)
            .HasForeignKey(c => c.StudioId).IsRequired(true)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Address)
            .WithOne().IsRequired(false)
            .HasForeignKey<ClientEntity>(c => c.AddressId)
            .OnDelete(DeleteBehavior.Cascade);
        */
    }
}
