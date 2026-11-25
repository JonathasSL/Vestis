using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Vestis._02_Application.Common;
using Vestis._02_Application.CQRS.Client.Query;
using Vestis._02_Application.Models.Client;
using Vestis._02_Application.Services.Interfaces;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrastructure.Repositories.Interfaces;

namespace Vestis._02_Application.Services;

internal class ClientService : CRUDService<ClientModel, ClientEntity, Guid>, IClientService
{
    public async Task<CommandResult<IEnumerable<ClientSearchResultsModel>>> GetClientsByStudioWithFilters(Guid studioGuid, Dictionary<string, string> filters, CancellationToken cancellationToken)
    {
        var query = CreateSearchQuery(studioGuid, filters);
        var searchResult = await _mediator.Send(query, cancellationToken);

        var result = CommandResult<IEnumerable<ClientSearchResultsModel>>.Success(searchResult);
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
