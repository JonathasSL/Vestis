using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vestis._03_Domain.Entities;

namespace Vestis._04_Infrastructure.Mapping;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    private const int _NameMaxLength = 256;
    private const int _EmailMaxLength = 256;

    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable(nameof(UserEntity).Replace("Entity", string.Empty).Pluralize());
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(_NameMaxLength);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(_EmailMaxLength);

        builder.Property(x => x.Password)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasIndex(x => x.Email).IsUnique();
    }
}
