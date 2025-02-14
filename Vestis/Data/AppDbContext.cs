using Microsoft.EntityFrameworkCore;
using Vestis.Entities;

namespace Vestis.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<UserEntity> Users { get; set; }
    }
}
