using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrasctructure.Data;
using Vestis._04_Infrasctructure.Repositories.Interfaces;

namespace Vestis._04_Infrasctructure.Repositories;

internal class UserConfirmationTokenRepository : Repository<UserConfirmationTokenEntity, Guid>, IUserConfirmationTokenRepository
{
    public UserConfirmationTokenRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Task CreateAsync(UserConfirmationTokenEntity tokenEntity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<UserConfirmationTokenEntity?> GetByTokenAsync(string token, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task MarkAsUsedAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
