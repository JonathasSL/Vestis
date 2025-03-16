using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vestis._02_Domain.Entities.Mapping
{
    public class ProjectEntityConfiguration : IEntityTypeConfiguration<ProjectEntity>
    {
        public void Configure(EntityTypeBuilder<ProjectEntity> builder)
        {
            builder.ToTable(nameof(ProjectEntity).Replace("Entity",string.Empty).Pluralize());

            builder.HasKey(x => x.Id);
        }
    }
}
