using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestis._02_Application.Common;
using Vestis._02_Application.CQRS.Client.Commands;
using Vestis._02_Application.Mappings.Client;
using Vestis._02_Application.Models;
using Vestis._02_Application.Models.Client;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrastructure.Repositories.Interfaces;

namespace Vestis._02_Application.CQRS.Client.Handlers;

public class ClientCommandHandler :
    IRequestHandler<CreateClientCommand, CommandResult<ClientModel>>
{
    private readonly IMediator _mediator;
    private readonly IClientRepository _repository;
    private readonly BusinessNotificationContext _notificationContext;

    public ClientCommandHandler(IMediator mediator, IClientRepository repository, BusinessNotificationContext businessNotificationContext)
    {
        _mediator = mediator;
        _repository = repository;
        _notificationContext = businessNotificationContext;
    }

    public Task<CommandResult<ClientModel>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var clientEntity = new ClientEntity(request.Name, request.FromStudioId)
            .ChangeEmail(request.Email)
            .ChangePhoneNumber(request.PhoneNumber);

        if (request.Address is not null)
        {
            var address = _mediator.Send(request.Address, cancellationToken).Result;
            
            if (_notificationContext.HasNotifications)
            {
                var errorMessages = new List<string>(_notificationContext.Notifications);

                return Task.FromResult(CommandResult<ClientModel>.Failure(errorMessages));
            }

            clientEntity.ChangeAddress(address);
        }

        var savedEntity = _repository.CreateAsync(clientEntity, cancellationToken).Result;
        if (savedEntity is null)
            return Task.FromResult(CommandResult<ClientModel>.Failure(new List<string> { "Failed to create client." }));

        var model = savedEntity.ToClientModel();


        return Task.FromResult(CommandResult<ClientModel>.Success(model));
    }
}
