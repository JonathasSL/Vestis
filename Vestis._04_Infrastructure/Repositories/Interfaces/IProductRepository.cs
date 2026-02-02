using Vestis._03_Domain.Entities;
using Vestis._04_Infrastructure.ObjectQuery;

namespace Vestis._04_Infrastructure.Repositories.Interfaces;

public interface IProductRepository : IRepository<ProductEntity, Guid>
{
    Task<ProductEntity?> GetProductByIdAndStudioIdAsync(Guid productId, Guid studioGuid);
    Task<List<ProductEntity>> GetProductsByStudioIdAsync(ProductFilters filters, CancellationToken cancellationToken);
}
