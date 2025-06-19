using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Vestis._02_Application.Common;
using Vestis._02_Application.Models;
using Vestis._02_Application.Services.Interfaces;
using Vestis._02_Application.Studio.Commands;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrasctructure.Repositories.Interfaces;

namespace Vestis._02_Application.Services;

public class StudioService : CRUDService<StudioModel, StudioEntity, Guid>, IStudioService
{
    private readonly BusinessNotificationContext _businessNotificationContext;

    public StudioService(
        IMapper mapper,
        IMediator mediator,
        BusinessNotificationContext businessNotificationContext,
        ILogger<CRUDService<StudioModel, StudioEntity, Guid>> logger, 
        IRepository<StudioEntity, Guid> repository) : base(mapper, mediator, logger, repository)
    {
        _businessNotificationContext = businessNotificationContext;
    }

    public async Task<CommandResult<StudioModel>> CreateByCommand(StudioModel model)
    {
        var command = new CreateStudioCommand(model.Name, model.ContactEmail, model.PhoneNumber);

        var result = await _mediator.Send(command);
        
        if (_businessNotificationContext.HasNotifications)
            return CommandResult<StudioModel>.Failure(
                "Houve um erro ao criar seu ateliê. Verifique suas informações e tente novamente",
                _businessNotificationContext.Notifications.ToList()
            );
        else
            return CommandResult<StudioModel>.Success(model);
    }

}
