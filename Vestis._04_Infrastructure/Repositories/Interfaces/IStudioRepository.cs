using Vestis._03_Domain.Entities;

namespace Vestis._04_Infrasctructure.Repositories.Interfaces;

public interface IStudioRepository : IRepository<StudioEntity,Guid>
{
    Task<List<StudioEntity>> GetByUserAsync(Guid userId, CancellationToken cancellationToken);
}
