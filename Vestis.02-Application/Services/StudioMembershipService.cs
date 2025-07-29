using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Vestis._02_Application.Common;
using Vestis._02_Application.Models;
using Vestis._02_Application.Services.Interfaces;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrasctructure.Repositories.Interfaces;

namespace Vestis._02_Application.Services;

internal class StudioMembershipService : CRUDService<StudioMembershipModel, StudioMembershipEntity, Guid>, IStudioMembershipService
{
    private readonly IStudioMembershipRepository _repository;

    public StudioMembershipService(
        IMapper mapper,
        IMediator mediator,
        BusinessNotificationContext businessNotificationContext,
        ILogger<StudioMembershipService> logger,
        IStudioMembershipRepository repository)
        : base(mapper, mediator, businessNotificationContext, logger, repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<StudioMembershipModel>?> GetFromStudioId(Guid studioId, CancellationToken cancellationToken)
    {
        if (studioId == Guid.Empty) return null;

        var entities = await _repository.GetFromStudioIdAsync(studioId, cancellationToken);
        var models = _mapper.Map<IEnumerable<StudioMembershipModel>>(entities);

        return models;
    }

    public async Task<StudioMembershipModel> GetByUserAndStudioAsync(Guid userId, Guid studioId, CancellationToken cancellationToken)
    {
        if (userId == Guid.Empty || studioId == Guid.Empty)
            return null;
        
        var entity = await _repository.GetByUserAndStudioAsync(userId, studioId,cancellationToken);
        if (entity == null) return null;

        var model = _mapper.Map<StudioMembershipModel>(entity);

        return model;
    }

    public async Task<IEnumerable<StudioMembershipModel>> GetFromUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        if (userId == Guid.Empty)
            return null;

        var entities = await _repository.GetFromUserIdAsync(userId, cancellationToken);
        var models = _mapper.Map<IEnumerable<StudioMembershipModel>>(entities);

        return models;
    }
}