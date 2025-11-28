namespace Vestis._02_Application.Services.Interfaces;

public interface ICRUDService<TModel, TEntity, TId>
        where TModel : class
        where TEntity : class
{
    Task<TModel> CreateByMapping(TModel model, CancellationToken cancellationToken);
    Task<IEnumerable<TModel>>? GetAllAsync();
    Task<TModel>? GetById(TId id);
    Task<TModel>? Update(TId id, TModel model);
    Task<TModel> Delete(TId id);
}