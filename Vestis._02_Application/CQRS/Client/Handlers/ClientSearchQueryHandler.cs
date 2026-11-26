using MediatR;
using Vestis._02_Application.CQRS.Client.Query;
using Vestis._02_Application.Models.Client;
using Vestis._04_Infrastructure.ObjectQuery.Filters;
using Vestis._04_Infrastructure.Repositories.Interfaces;

namespace Vestis._02_Application.CQRS.Client.Handlers;

internal class ClientSearchQueryHandler : IRequestHandler<SearchClientsQuery, List<ClientSearchResultsModel>>
{
    private readonly IClientRepository _repository;
    public Task<List<ClientSearchResultsModel>> Handle(SearchClientsQuery request, CancellationToken cancellationToken)
    {
        var filters = new ClientFilters
        {
            StudioId = request.StudioId,
            SearchTerm = request.searchTerm,
            ClientSinceStart = request.ClientSinceStart,
            ClientSinceEnd = request.ClientSinceEnd,
            MinTotalPurchases = request.MinTotalPurchases,
            MaxTotalPurchases = request.MaxTotalPurchases
        };

        var entities = _repository.SearchStudioClientsWithFilters(filters, cancellationToken);

        var model = entities.Select(e => new ClientSearchResultsModel
        {
            Id = e.Id,
            Name = e.Name,
            Email = e.Email,
            PhoneNumber = e.PhoneNumber,
            ClientSince = DateOnly.FromDateTime(e.CreatedDate),
        }).OrderBy(e => e.Name).ToList();

        return Task.FromResult(model);

        
    }
    private string GetSearchedContactInfo(string searchTerm, string info1, string info2)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return info1;
        if (info1 is null)
            return info2;
        if (info2 is null)
            return info1;

        if (info1.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            return info1;
        if (info2.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            return info2;

        return info1;
    }


    public ClientSearchQueryHandler(IClientRepository repository)
    {
        _repository = repository;
    }
}
