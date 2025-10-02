using Vestis._03_Domain.Entities;

namespace Vestis._04_Infrasctructure.Repositories.Interfaces;

public interface IRepository<T, TId>
    where T : BaseEntity<TId>
    where TId : struct
{
    Task<T> GetByIdAsync(TId id, CancellationToken cancellationToken);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
    Task<T> CreateAsync(T entity);
    Task<T> Update(T entity);
    Task SoftDeleteAsync(T entity);
}
