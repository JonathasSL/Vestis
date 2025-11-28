using MediatR;
using Vestis._02_Application.CQRS.Product.Query;
using Vestis._02_Application.Models.Product;
using Vestis._04_Infrasctructure.ObjectQuery;
using Vestis._04_Infrasctructure.Repositories.Interfaces;
using Vestis.Shared.Extensions;

namespace Vestis._02_Application.CQRS.Product.Handlers;

internal class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, List<ProductModel>>
{
    private readonly IProductRepository _repository;
    
    public Task<List<ProductModel>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
    {
        var filters = new ProductFilters
        {
            StudioId = request.StudioId,
            Name = request.Name?.EmptyToNull(),
            Category = request.Category?.EmptyToNull(),
            MinPrice = request.MinPrice,
            MaxPrice = request.MaxPrice
        };
        var entities = _repository.GetProductsByStudioIdAsync(filters, cancellationToken).Result;
        
        
        var models = entities.Select(p => new ProductModel
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Category = p.Category,
            Price = p.Price,
            UnitCount = p.UnitCount,
            ImgUrl = p.ImgUrl
        }).OrderBy(product => product.Name).ToList();

        return Task.FromResult(models);
    }

    public SearchProductsQueryHandler(IProductRepository repository)
    {
        _repository = repository;
    }
}
