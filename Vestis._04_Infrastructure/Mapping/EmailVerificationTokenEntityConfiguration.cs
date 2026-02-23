using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vestis._03_Domain.Entities;

namespace Vestis._04_Infrastructure.Mapping;

public class EmailVerificationTokenEntityConfiguration : IEntityTypeConfiguration<EmailVerificationTokenEntity>
{
    private const int _TokenMaxLength = 256;

    public void Configure(EntityTypeBuilder<EmailVerificationTokenEntity> builder)
    {
        builder.ToTable(nameof(EmailVerificationTokenEntity).Replace("Entity", string.Empty).Pluralize());

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Token)
            .IsRequired()
            .HasMaxLength(_TokenMaxLength);

        builder.Property(x => x.ExpirationDateUtc)
            .IsRequired();

        builder.Property(x => x.IsUsed)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(u => u.EmailVerificationTokens)
            .HasForeignKey(x => x.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.ClientNoAction);

        builder.HasIndex(x => x.Token);
        builder.HasIndex(x => x.UserId);
    }
}
