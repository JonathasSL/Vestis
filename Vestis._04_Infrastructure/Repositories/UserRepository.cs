using Microsoft.EntityFrameworkCore;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrasctructure.Data;
using Vestis._04_Infrasctructure.Repositories.Interfaces;

namespace Vestis._04_Infrasctructure.Repositories;

internal class UserRepository : Repository<UserEntity, Guid>, IUserRepository
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
