using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Vestis._02_Application.Common;
using Vestis._02_Application.CQRS.Address.Commands;
using Vestis._02_Application.CQRS.Client.Commands;
using Vestis._02_Application.CQRS.Client.Query;
using Vestis._02_Application.Models.Client;
using Vestis._02_Application.Services.Interfaces;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrastructure.Repositories.Interfaces;

namespace Vestis._02_Application.Services.Client;

internal class ClientService : CRUDService<ClientModel, ClientEntity, Guid>, IClientService
{
    public async Task<CommandResult<IEnumerable<ClientSearchResultsModel>>> GetClientsByStudioWithFilters(Guid studioGuid, Dictionary<string, string> filters, CancellationToken cancellationToken)
    {
        var query = CreateSearchQuery(studioGuid, filters);
        var searchResult = await _mediator.Send(query, cancellationToken);

        var result = CommandResult<IEnumerable<ClientSearchResultsModel>>.Success(searchResult);
        return result;
    }

    public async Task<CommandResult<ClientModel>> GetClientByIdAndStudioId(Guid clientId, Guid studioGuid, CancellationToken cancellationToken)
    {
        var query = new GetClientByIdQuery(clientId, studioGuid);
        var clientResult = await _mediator.Send(query, cancellationToken);

        if (clientResult is null)
            return CommandResult<ClientModel>.NotFound("Client not found.");

        return clientResult;
    }

    public async Task<CommandResult<ClientModel>> CreateClient(Guid studioGuid, CreateClientRequestModel request, CancellationToken cancellationToken)
    {
        CreateAddressCommand addressCommand = null;
        if (request.Address is not null)
            addressCommand = new CreateAddressCommand(
                request.Address.Street, 
                request.Address.Number, 
                request.Address.Complement, 
                request.Address.Neighborhood, 
                request.Address.City, 
                request.Address.State, 
                request.Address.ZipCode, 
                request.Address.Country);

        var command = new CreateClientCommand(
            request.Name, 
            request.Email, 
            request.PhoneNumber, 
            studioGuid, 
            addressCommand);
        
        var result = await _mediator.Send(command, cancellationToken);

        if (_businessNotificationContext.HasNotifications)
            result = CommandResult<ClientModel>.Failure(_businessNotificationContext.Notifications);

        return result;
    }

    private SearchClientsQuery CreateSearchQuery(Guid studioGuid, Dictionary<string, string> filters)
    {
        return new SearchClientsQuery(
            StudioId: studioGuid,
            searchTerm: filters.TryGetValue("searchTerm", out var searchTerm) ? searchTerm : null,
            ClientSinceStart: filters.TryGetValue("clientSinceStart", out var clientSinceStartStr) && DateOnly.TryParse(clientSinceStartStr, out var clientSinceStart) ? clientSinceStart : null,
            ClientSinceEnd: filters.TryGetValue("clientSinceEnd", out var clientSinceEndStr) && DateOnly.TryParse(clientSinceEndStr, out var clientSinceEnd) ? clientSinceEnd : null,
            MinTotalPurchases: filters.TryGetValue("minTotalPurchases", out var minTotalPurchasesStr) && long.TryParse(minTotalPurchasesStr, out var minTotalPurchases) ? minTotalPurchases : null,
            MaxTotalPurchases: filters.TryGetValue("maxTotalPurchases", out var maxTotalPurchasesStr) && long.TryParse(maxTotalPurchasesStr, out var maxTotalPurchases) ? maxTotalPurchases : null
        );
    }
    
    public ClientService(IMapper mapper, IMediator mediator, BusinessNotificationContext businessNotificationContext, ILogger<CRUDService<ClientModel, ClientEntity, Guid>> logger, IRepository<ClientEntity, Guid> repository) : base(mapper, mediator, businessNotificationContext, logger, repository)
    {
    }
}
