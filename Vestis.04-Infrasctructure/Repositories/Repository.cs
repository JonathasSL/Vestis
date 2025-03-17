using Microsoft.EntityFrameworkCore;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrasctructure.Data;
using Vestis._04_Infrasctructure.Repositories.Interfaces;

namespace Vestis._04_Infrasctructure.Repositories;

internal class Repository<T, TId> : IRepository<T, TId>
    where T : BaseEntity<TId>
    where TId : struct
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T> GetByIdAsync(TId id) => await _dbSet.FindAsync(id);

    public Task<IEnumerable<T>> GetAllAsync() => Task.FromResult(BeginQuery().AsEnumerable());

    public Task<T> CreateAsync(T entity)
    {
        _dbSet.Add(entity);
        _context.SaveChanges();
        return Task.FromResult(entity);
    }

    public async Task SoftDeleteAsync(T entity)
    {
        entity.SetAsDeleted();
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<T> Update(T entity)
    {
        entity.SetAsUpdated();
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    protected IQueryable<T> BeginQuery() => _dbSet.Where(e => !e.DeletedDate.HasValue);
    protected IQueryable<T> BeginQueryReadOnly() => _dbSet.AsNoTracking().Where(e => !e.DeletedDate.HasValue);

}
