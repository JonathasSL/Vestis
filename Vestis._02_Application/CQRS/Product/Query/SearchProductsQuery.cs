using MediatR;
using Vestis._02_Application.Models.Product;

namespace Vestis._02_Application.CQRS.Product.Query;

internal record SearchProductsQuery(
    Guid StudioId,
    string? Name,
    string? Category,
    double? MinPrice,
    double? MaxPrice
) : IRequest<List<ProductModel>>;
