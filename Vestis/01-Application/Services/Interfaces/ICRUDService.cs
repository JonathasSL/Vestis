namespace Vestis._01_Application.Services.Interfaces;

public interface ICRUDService<TModel, TEntity, TId>
        where TModel : class
        where TEntity : class
{
    Task<TModel> Create(TModel model);
    Task<IEnumerable<TModel>>? GetAllAsync();
    Task<TModel>? GetById(TId id);
    Task<TModel>? Update(TId id, TModel model);
    Task<TModel> Delete(TId id);
}