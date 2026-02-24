using Microsoft.EntityFrameworkCore;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrastructure.Data;
using Vestis._04_Infrastructure.Repositories.Interfaces;

namespace Vestis._04_Infrastructure.Repositories;

internal class EmailVerificationTokenEntityRepository : Repository<EmailVerificationTokenEntity, Guid>, IEmailVerificationTokenEntityRepository
{
    public EmailVerificationTokenEntityRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<EmailVerificationTokenEntity>> GetValidByEmailAndCodeAsync(string email, string code, CancellationToken cancellationToken)
    {
        var query = BeginQuery()
            .Include(e => e.User)
            .Where(e => e.Token.Equals(code)
                        && e.User.Email.Equals(email)
                        && !e.IsUsed);

        var x = query.ToList();
        return await query.ToListAsync(cancellationToken);
    }
}