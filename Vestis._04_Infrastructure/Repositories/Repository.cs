using Microsoft.EntityFrameworkCore;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrasctructure.Data;
using Vestis._04_Infrasctructure.Repositories.Interfaces;

namespace Vestis._04_Infrasctructure.Repositories;

internal class Repository<T, TId> : IRepository<T, TId>
    where T : BaseEntity<TId>
    where TId : struct
{
    private readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _dbSet = context.Set<T>();
    }

    public async Task<T> GetByIdAsync(TId id) => await _dbSet.FindAsync(id);

    public Task<IEnumerable<T>> GetAllAsync() => Task.FromResult(BeginQuery().AsEnumerable());

    public Task<T> CreateAsync(T entity)
    {
        _dbSet.Add(entity);
        return Task.FromResult(entity);
    }

    public async Task SoftDeleteAsync(T entity)
    {
        entity.SetAsDeleted();
        _dbSet.Update(entity);
    }

    public async Task<T> Update(T entity)
    {
        entity.SetAsUpdated();
        _dbSet.Update(entity);
        return entity;
    }

    protected IQueryable<T> BeginQuery() => _dbSet.Where(e => !e.DeletedDate.HasValue);
    protected IQueryable<T> BeginQueryReadOnly() => _dbSet.AsNoTracking().Where(e => !e.DeletedDate.HasValue);

}
