using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Vestis._02_Application.Common;
using Vestis._02_Application.Services.Interfaces;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrastructure.Repositories.Interfaces;
using Vestis.Shared.Extensions;

namespace Vestis._02_Application.Services;

public abstract class CRUDService<TModel, TEntity, TId> : ICRUDService<TModel, TEntity, TId>
    where TModel : class
    where TEntity : BaseEntity<TId>
    where TId : struct
{
    private readonly IRepository<TEntity, TId> _repository;
    protected readonly IMapper _mapper;
    protected readonly IMediator _mediator;
    protected readonly BusinessNotificationContext _businessNotificationContext;
    protected readonly ILogger<CRUDService<TModel, TEntity, TId>> _logger;

    protected CRUDService(IMapper mapper, IMediator mediator, BusinessNotificationContext businessNotificationContext, ILogger<CRUDService<TModel, TEntity, TId>> logger, IRepository<TEntity, TId> repository)
    {
        _mapper = mapper;
        _mediator = mediator;
        _businessNotificationContext = businessNotificationContext;
        _logger = logger;
        _repository = repository;
    }

    public virtual async Task<CommandResult<TModel>> CreateByMapping(TModel model, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TEntity>(model);
        var result = await _repository.CreateAsync(entity, cancellationToken);
        return CommandResult<TModel>.Success(_mapper.Map<TModel>(result));
    }

    public virtual async Task<CommandResult<IEnumerable<TModel>>> GetAllAsync()
    {
        var entityList = await _repository.GetAllAsync();
        if (entityList == null)
            return CommandResult<IEnumerable<TModel>>.NotFound();

        var result = _mapper.Map<IEnumerable<TModel>>(entityList);
        return CommandResult<IEnumerable<TModel>>.Success(result);
    }

    public virtual async Task<CommandResult<TModel>?> GetById(TId id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            return CommandResult<TModel>.NotFound();

        return CommandResult<TModel>.Success(_mapper.Map<TModel>(entity));
    }

    public virtual async Task<CommandResult<TModel>?> Update(TId id, TModel model)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            return CommandResult<TModel>.NotFound();

        _mapper.Map(model, entity);

        var result = _repository.Update(entity);
        return CommandResult<TModel>.Success(_mapper.Map<TModel>(result));
    }

    public virtual async Task<CommandResult<TModel>> Delete(TId id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            return CommandResult<TModel>.NotFound();

        if (await _repository.SoftDeleteAsync(entity))
            return CommandResult<TModel>.Success(_mapper.Map<TModel>(entity));
        else
            throw new Exception("Failed to delete entity.");

    }
}