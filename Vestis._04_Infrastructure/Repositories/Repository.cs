using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrasctructure.Data;
using Vestis._04_Infrasctructure.Repositories.Interfaces;

namespace Vestis._04_Infrasctructure.Repositories;

internal class Repository<T, TId> : IRepository<T, TId>
    where T : BaseEntity<TId>
    where TId : struct
{
    private readonly DbSet<T> _dbSet;
    private readonly Logger<Repository<T, TId>> _logger;

    public Repository(ApplicationDbContext context)
    {
        _dbSet = context.Set<T>();
    }

    public async Task<T> GetByIdAsync(TId id) => await _dbSet.FindAsync(id);

    public Task<IEnumerable<T>> GetAllAsync() => Task.FromResult(BeginQuery().AsEnumerable());

    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken)
    {
        try
        {
            var entityEntry = await _dbSet.AddAsync(entity, cancellationToken);
            return entityEntry.Entity;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error when creating entity: {entity.GetType().Name}, Id: {entity.Id} ", e);
            return null;
        }

    }

    public async Task<bool> SoftDeleteAsync(T entity)
    {
        try
        {
            entity.SetAsDeleted();
            _dbSet.Update(entity);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error when softDeleting entity: {entity.GetType().Name}, Id: {entity.Id} ", e);
            return false;
        }
    }

    public async Task<T> Update(T entity)
    {
        try
        {
            entity.SetAsUpdated();
            var result = _dbSet.Update(entity).Entity;
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error when updating entity: {entity.GetType().Name}, Id: {entity.Id} ", e);
            return null;
        }
    }

    protected IQueryable<T> BeginQuery() => _dbSet.Where(e => !e.DeletedDate.HasValue);
    protected IQueryable<T> BeginQueryReadOnly() => _dbSet.AsNoTracking().Where(e => !e.DeletedDate.HasValue);

}
