using Vestis._02_Application.Common;

namespace Vestis._02_Application.Services.Interfaces;

public interface ICRUDService<TModel, TEntity, TId>
        where TModel : class
        where TEntity : class
{
    Task<CommandResult<TModel>> CreateByMapping(TModel model, CancellationToken cancellationToken);
    Task<CommandResult<IEnumerable<TModel>>> GetAllAsync();
    Task<CommandResult<TModel>> GetById(TId id);
    Task<CommandResult<TModel>> Update(TId id, TModel model);
    Task<CommandResult<TModel>> Delete(TId id);
}