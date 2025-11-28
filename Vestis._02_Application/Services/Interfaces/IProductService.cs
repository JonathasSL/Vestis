using Vestis._02_Application.Models.Product;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.Services.Interfaces;

public interface IProductService : ICRUDService<ProductModel, ProductEntity, Guid>
{
    List<ProductModel> GetProductsByStudioWithFiltersAsync(Guid studioId, Dictionary<string, string>? filters);
    Task<ProductModel> GetProductByStudio(Guid productId, Guid studioGuid);
    ProductModel RegisterProduct(ProductModel requestModel);
}
