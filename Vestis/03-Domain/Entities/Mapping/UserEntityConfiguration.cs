using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vestis.Entities;

namespace Vestis._02_Domain.Entities.Mapping
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable(nameof(UserEntity).Replace("Entity", string.Empty).Pluralize());
            
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}
