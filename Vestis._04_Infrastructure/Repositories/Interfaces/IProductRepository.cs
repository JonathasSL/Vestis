using Vestis._03_Domain.Entities;

namespace Vestis._04_Infrasctructure.Repositories.Interfaces;

public interface IProductRepository : IRepository<ProductEntity, Guid>
{
    Task<ProductEntity?> GetProductByIdAndStudioIdAsync(Guid productId, Guid studioGuid);
    Task<IEnumerable<ProductEntity>> GetProductsByStudioIdAsync(Guid studioId);
}
