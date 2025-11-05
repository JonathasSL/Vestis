using Vestis._02_Application.Models;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.Services.Interfaces;

public interface IProductService : ICRUDService<ProductModel, ProductEntity, Guid>
{
    IEnumerable<ProductModel> GetAllProductsByStudio(Guid studioId);
    IEnumerable<ProductModel> GetByNameAndStudio(string productName, Guid studioId);
    ProductModel GetProductByStudio(Guid productId, Guid studioGuid);
}
