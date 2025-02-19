using AutoMapper;
using Vestis._01_Application.Services.Interfaces;
using Vestis._02_Domain.Repositories.Interfaces;
using Vestis.Entities;

namespace Vestis._01_Application.Services;


public abstract class CRUDService<TModel, TEntity, TId> : ICRUDService<TModel, TEntity, TId>
    where TModel : class
    where TEntity : BaseEntity<TId>
    where TId : struct
{
    private readonly IRepository<TEntity, TId> _repository;
    protected readonly IMapper _mapper;

    protected CRUDService(IRepository<TEntity, TId> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public virtual async Task<TModel> Create(TModel model)
    {
        var entity = _mapper.Map<TEntity>(model);
        var result = await _repository.CreateAsync(entity);
        return _mapper.Map<TModel>(result);
    }

    public virtual async Task<IEnumerable<TModel>>? GetAllAsync()
    {
        var entityList = await _repository.GetAllAsync();
        if (entityList == null)
            return null;

        var result = _mapper.Map<IEnumerable<TModel>>(entityList);
        return result;
    }

    public virtual async Task<TModel>? GetById(TId id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            return null;

        return _mapper.Map<TModel>(entity);
    }

    public virtual async Task<TModel>? Update(TId id, TModel model)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            return null;

        _mapper.Map(model, entity);

        var result = _repository.Update(entity);
        return _mapper.Map<TModel>(result);
    }

    public virtual async Task<TModel> Delete(TId id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            return null;

        await _repository.SoftDeleteAsync(entity);
        return _mapper.Map<TModel>(entity);
    }
}