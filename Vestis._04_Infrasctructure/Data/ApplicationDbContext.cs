using Humanizer;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Vestis._04_Infrasctructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var types = Assembly.Load("Vestis._03_Domain").GetTypes();
        var entities = types.Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Entity"));

        foreach (var entity in entities)
        {
            var tableName = entity.Name.Replace("Entity", string.Empty).Pluralize();
            modelBuilder.Entity(entity).ToTable(tableName);
        }

        base.OnModelCreating(modelBuilder);
    }
}
