using Vestis._02_Application.Models;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.Services.Interfaces;

public interface IProductService : ICRUDService<ProductModel, ProductEntity, Guid>
{
    IEnumerable<ProductModel> GetProductsByStudioWithFilters(Guid studioId, Dictionary<string, string>? filters);
    ProductModel GetProductByStudio(Guid productId, Guid studioGuid);
}
