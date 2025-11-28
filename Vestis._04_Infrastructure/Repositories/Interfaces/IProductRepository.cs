using Vestis._03_Domain.Entities;
using Vestis._04_Infrasctructure.ObjectQuery;

namespace Vestis._04_Infrasctructure.Repositories.Interfaces;

public interface IProductRepository : IRepository<ProductEntity, Guid>
{
    Task<ProductEntity?> GetProductByIdAndStudioIdAsync(Guid productId, Guid studioGuid);
    Task<List<ProductEntity>> GetProductsByStudioIdAsync(ProductFilters filters, CancellationToken cancellationToken);
}
