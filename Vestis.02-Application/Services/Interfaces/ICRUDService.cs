namespace Vestis._02_Application.Services.Interfaces;

public interface ICRUDService<TModel, TEntity, TId>
        where TModel : class
        where TEntity : class
{
    Task<TModel> CreateByMapping(TModel model);
    Task<IEnumerable<TModel>>? GetAllAsync(CancellationToken cancellationToken);
    Task<TModel>? GetById(TId id, CancellationToken cancellationToken);
    Task<TModel>? Update(TId id, TModel model, CancellationToken cancellationToken);
    Task<TModel> Delete(TId id, CancellationToken cancellationToken);
}