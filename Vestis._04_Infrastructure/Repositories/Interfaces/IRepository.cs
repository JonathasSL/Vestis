using Vestis._03_Domain.Entities;

namespace Vestis._04_Infrastructure.Repositories.Interfaces;

public interface IRepository<T, TId>
    where T : BaseEntity<TId>
    where TId : struct
{
    Task<T> GetByIdAsync(TId id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken);
    Task<T> Update(T entity);
    Task<bool> SoftDeleteAsync(T entity);
}
