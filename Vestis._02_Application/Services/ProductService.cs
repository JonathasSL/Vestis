using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Vestis._02_Application.Common;
using Vestis._02_Application.Models;
using Vestis._02_Application.Services.Interfaces;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrasctructure.Repositories.Interfaces;

namespace Vestis._02_Application.Services;

internal class ProductService : CRUDService<ProductModel, ProductEntity, Guid>, IProductService
{

    public IEnumerable<ProductModel> GetAllProductsByStudio(Guid studioId)
    {
        var inventoryModelList = _mapper.Map<IEnumerable<ProductModel>>(
            _repository.GetProductsByStudioIdAsync(studioId).Result
        );
        return inventoryModelList;
    }

    public ProductModel GetProductByStudio(Guid productId, Guid studioGuid)
    {
        return _mapper.Map<ProductModel>(
            _repository.GetProductByIdAndStudioIdAsync(productId, studioGuid)
        );
    }

    private IProductRepository _repository;

    public ProductService(
        IMapper mapper,
        IMediator mediator,
        BusinessNotificationContext businessNotificationContext,
        ILogger<ProductService> logger,
        IProductRepository repository) : base(mapper, mediator, businessNotificationContext, logger, repository)
    {
        this._repository = repository;
    }
}
