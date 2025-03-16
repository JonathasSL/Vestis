using Vestis.Entities;

namespace Vestis._02_Domain.Repositories.Interfaces;

public interface IRepository<T, TId>
    where T : BaseEntity<TId>
    where TId : struct
{
    Task<T> GetByIdAsync(TId id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> CreateAsync(T entity);
    Task<T> Update(T entity);
    Task SoftDeleteAsync(T entity);
}
