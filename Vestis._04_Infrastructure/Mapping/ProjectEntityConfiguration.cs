using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vestis._03_Domain.Entities;

namespace Vestis._04_Infrastructure.Mapping;

public class ProjectEntityConfiguration : IEntityTypeConfiguration<ProjectEntity>
{
    public void Configure(EntityTypeBuilder<ProjectEntity> builder)
    {
        builder.ToTable(nameof(ProjectEntity).Replace("Entity",string.Empty).Pluralize());

        builder.HasKey(p => p.Id);
        builder.HasOne(p => p.Studio)
            .WithMany()
            .HasForeignKey(p => p.StudioId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Restrict);
    }
}