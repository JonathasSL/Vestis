using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vestis._02_Domain.Entities.Mapping;

public class ClientEntityConfiguration : IEntityTypeConfiguration<ClientEntity>
{
    public void Configure(EntityTypeBuilder<ClientEntity> builder)
    {
        builder.ToTable(nameof(ClientEntity).Replace("Entity", string.Empty).Pluralize());

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(128);

        builder.HasOne(x => x.Studio)
            .WithMany(x => x.Clients)
            .HasForeignKey(x => x.StudioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Address)
            .WithOne()
            .HasForeignKey<ClientEntity>(x => x.AddressId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
