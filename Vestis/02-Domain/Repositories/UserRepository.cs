using Microsoft.EntityFrameworkCore;
using Vestis._02_Domain.Repositories.Interfaces;
using Vestis.Data;
using Vestis.Entities;

namespace Vestis._02_Domain.Repositories;

public class UserRepository : Repository<UserEntity, Guid>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> Exists(string email)
    {
        return await BeginQuery()
            .Where(user => user.Email == email)
            .AnyAsync();
    }

    public async Task<UserEntity> GetByEmailAsync(string email)
    {
        return await BeginQuery()
            .FirstOrDefaultAsync(user => user.Email == email);
    }
}
