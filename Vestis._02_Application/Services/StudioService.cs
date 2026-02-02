using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using Vestis._02_Application.Common;
using Vestis._02_Application.CQRS.Address.Commands;
using Vestis._02_Application.CQRS.Studio.Commands;
using Vestis._02_Application.CQRS.Studio.Query;
using Vestis._02_Application.Models.Studio;
using Vestis._02_Application.Services.Interfaces;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrastructure.Repositories.Interfaces;
using Vestis.Shared.Extensions;

namespace Vestis._02_Application.Services;

public class StudioService : CRUDService<StudioModel, StudioEntity, Guid>, IStudioService
{
    private readonly IStudioRepository _repository;
    public StudioService(
        IMapper mapper,
        IMediator mediator,
        BusinessNotificationContext businessNotificationContext,
        ILogger<StudioService> logger, 
        IStudioRepository repository) : base(mapper, mediator, businessNotificationContext, logger, repository)
    {
        _repository = repository;
    }

    public async Task<CommandResult<StudioModel>> Create(Guid contextUser, StudioModel model)
    {
        StudioModel result;
        try
        {
            Guid userId = contextUser;
            CreateAddressCommand addressCommand = default;

            if (model.Address is not null)
            {
                addressCommand = new (
                    model.Address.Street,
                    model.Address.Number,
                    model.Address.Neighborhood,
                    model.Address.City,
                    model.Address.State,
                    model.Address.ZipCode,
                    model.Address.Neighborhood,
                    model.Address.Country
                );
            }

            var studioCommand = new CreateStudioCommand(userId, model.Name, model.ContactEmail, model.PhoneNumber, addressCommand);
            
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


    public CommandResult<List<StudioSummaryModel>> GetStudiosByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var query = new GetStudiosByUserIdQuery(userId);
        return _mediator.Send(query, cancellationToken).Result;
    }
}
