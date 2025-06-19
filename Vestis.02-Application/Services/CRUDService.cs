using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Vestis._02_Application.Services.Interfaces;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrasctructure.Repositories.Interfaces;
using Vestis.Shared.Extensions;

namespace Vestis._02_Application.Services;


public abstract class CRUDService<TModel, TEntity, TId> : ICRUDService<TModel, TEntity, TId>
    where TModel : class
    where TEntity : BaseEntity<TId>
    where TId : struct
{
    protected readonly IMapper _mapper;
    protected readonly IMediator _mediator;
    protected readonly ILogger<CRUDService<TModel, TEntity, TId>> _logger;
    private readonly IRepository<TEntity, TId> _repository;

    protected CRUDService(IMapper mapper, IMediator mediator, ILogger<CRUDService<TModel, TEntity, TId>> logger, IRepository<TEntity, TId> repository)
    {
        _mapper = mapper;
        _mediator = mediator;
        _logger = logger;
        _repository = repository;
    }

    public virtual async Task<TModel> Create(TModel model)
    {
        try
        {
            var entity = _mapper.Map<TEntity>(model);
            var result = await _repository.CreateAsync(entity);
            return _mapper.Map<TModel>(result);
        }
        catch (Exception e)
        {
            var exceptionStack = e.ExceptionStack(out _);
            _logger.LogError(exceptionStack);
            throw;
        }
    }

    public virtual async Task<IEnumerable<TModel>>? GetAllAsync()
    {
        try
        {
            var entityList = await _repository.GetAllAsync();
            if (entityList == null)
                return null;

            var result = _mapper.Map<IEnumerable<TModel>>(entityList);
            return result;
        }
        catch (Exception e)
        {
            var exceptionStack = e.ExceptionStack(out _);
            _logger.LogError(exceptionStack);
            throw;
        }
    }

    public virtual async Task<TModel>? GetById(TId id)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return null;

            return _mapper.Map<TModel>(entity);
        }
        catch (Exception e)
        {
            var exceptionStack = e.ExceptionStack(out _);
            _logger.LogError(exceptionStack);
            throw;
        }
    }

    public virtual async Task<TModel>? Update(TId id, TModel model)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return null;

            _mapper.Map(model, entity);

            var result = _repository.Update(entity);
            return _mapper.Map<TModel>(result);
        }
        catch (Exception e)
        {
            var exceptionStack = e.ExceptionStack(out _);
            _logger.LogError(exceptionStack);
            throw;
        }
    }

    public virtual async Task<TModel> Delete(TId id)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return null;

            await _repository.SoftDeleteAsync(entity);
            return _mapper.Map<TModel>(entity);
        }
        catch (Exception e)
        {
            var exceptionStack = e.ExceptionStack(out _);
            _logger.LogError(exceptionStack);
            throw;
        }
    }
}