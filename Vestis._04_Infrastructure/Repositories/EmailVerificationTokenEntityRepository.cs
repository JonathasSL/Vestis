using Vestis._03_Domain.Entities;
using Vestis._04_Infrastructure.Data;
using Vestis._04_Infrastructure.Repositories.Interfaces;

namespace Vestis._04_Infrastructure.Repositories;

internal class EmailVerificationTokenEntityRepository : Repository<EmailVerificationTokenEntity, Guid>, IEmailVerificationTokenEntityRepository
{
    public EmailVerificationTokenEntityRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Task<EmailVerificationTokenEntity?> GetByUserIdAsync(Guid userId)
    {
        return Task.FromResult(
            BeginQuery()
            .FirstOrDefault(e => e.UserId == userId)
            );
    }

}