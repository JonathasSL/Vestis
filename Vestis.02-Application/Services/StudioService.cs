using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Vestis._02_Application.Address.Commands;
using Vestis._02_Application.Common;
using Vestis._02_Application.Models;
using Vestis._02_Application.Services.Interfaces;
using Vestis._02_Application.Studio.Commands;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrasctructure.Repositories.Interfaces;
using Vestis.Shared.Extensions;

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
        StudioModel result;
        try
        {
            var addressCommand = new CreateAddressCommand(
                model.Address.Street.EmptyToNull(),
                model.Address.Number.EmptyToNull(),
                model.Address.Neighborhood.EmptyToNull(),
                model.Address.City.EmptyToNull(),
                model.Address.State.EmptyToNull(),
                model.Address.ZipCode.EmptyToNull(),
                model.Address.Neighborhood.EmptyToNull(),
                model.Address.Country.EmptyToNull()
            );

            var studioCommand = new CreateStudioCommand(model.Name, model.ContactEmail, model.PhoneNumber, addressCommand);
            
            var entity = await _mediator.Send(studioCommand);
            result = _mapper.Map<StudioModel>(entity);


            if (_businessNotificationContext.HasNotifications)
                return CommandResult<StudioModel>.Failure("Houve um erro ao criar seu ateliê.", _businessNotificationContext.Notifications.ToList());
            else
                return CommandResult<StudioModel>.Success(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ExceptionStack(out _));

            return CommandResult<StudioModel>.Failure( "Houve um erro ao criar seu ateliê. Verifique suas informações e tente novamente", _businessNotificationContext.Notifications.ToList());
        }
    }

}
