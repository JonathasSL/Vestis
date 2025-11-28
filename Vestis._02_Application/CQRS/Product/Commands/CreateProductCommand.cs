using MediatR;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.CQRS.Product.Commands;

public record CreateProductCommand(
    Guid studioId,
    string name,
    string? description,
    string? category,
    double? price,
    int? unitCount,
    string imgUrl) : IRequest<ProductEntity>;
