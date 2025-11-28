using MediatR;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.CQRS.Product.Commands;

public record DeleteProductCommand(
    Guid ProductId, 
    Guid StudioId) : IRequest<bool>
{
}
