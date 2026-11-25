using MediatR;
using Vestis._02_Application.Models.Client;

namespace Vestis._02_Application.CQRS.Client.Query;

internal record SearchClientsQuery(
    Guid StudioId,
    string? searchTerm,
    DateOnly? ClientSinceStart,
    DateOnly? ClientSinceEnd,
    long? MinTotalPurchases,
    long? MaxTotalPurchases
) : IRequest<List<ClientSearchResultsModel>>;
