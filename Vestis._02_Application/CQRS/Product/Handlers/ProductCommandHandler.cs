using MediatR;
using Vestis._02_Application.CQRS.Product.Commands;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrasctructure.Repositories.Interfaces;

namespace Vestis._02_Application.CQRS.Product.Handlers;

public class ProductCommandHandler : IRequestHandler<CreateProductCommand, ProductEntity>, IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IProductRepository _repository;
    public async Task<ProductEntity> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = new ProductEntity(
            request.name,
            request.category,
            request.description,
            request.price,
            request.unitCount ?? 1,
            request.imgUrl
        );

        entity = await _repository.CreateAsync(entity, cancellationToken);
        return entity;
    }

    public Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var entity = _repository.GetByIdAsync(request.ProductId).Result;
        var result = _repository.SoftDeleteAsync(entity);
        return result;
    }


    public ProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }
}
