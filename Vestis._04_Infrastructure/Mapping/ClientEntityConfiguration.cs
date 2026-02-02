using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vestis._03_Domain.Entities;

namespace Vestis._04_Infrastructure.Mapping;

public class ClientEntityConfiguration : IEntityTypeConfiguration<ClientEntity>
{
    private const int _NameMaxLength = 256;

    public void Configure(EntityTypeBuilder<ClientEntity> builder)
    {
        builder.ToTable(nameof(ClientEntity).Replace("Entity", string.Empty).Pluralize());

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(_NameMaxLength);
    }
}
