using Vestis._02_Application.Common;
using Vestis._02_Application.Models.Product;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.Services.Interfaces;

public interface IProductService : ICRUDService<ProductModel, ProductEntity, Guid>
{
    CommandResult<List<ProductModel>> GetProductsByStudioWithFiltersAsync(Guid studioId, Dictionary<string, string>? filters);
    Task<CommandResult<ProductModel>> GetProductByStudio(Guid productId, Guid studioGuid);
    CommandResult<ProductModel> RegisterProduct(ProductModel requestModel);
    CommandResult<bool> DeleteProduct(Guid productId, Guid studioId);
}
