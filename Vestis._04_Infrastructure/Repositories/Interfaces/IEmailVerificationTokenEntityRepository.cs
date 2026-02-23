using Vestis._03_Domain.Entities;

namespace Vestis._04_Infrastructure.Repositories.Interfaces;

public interface IEmailVerificationTokenEntityRepository : IRepository<EmailVerificationTokenEntity, Guid>
{
}
