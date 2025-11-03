using Vestis._03_Domain.Entities;
using Vestis._04_Infrasctructure.Data;
using Vestis._04_Infrasctructure.Repositories.Interfaces;

namespace Vestis._04_Infrasctructure.Repositories;

internal class ProductRepository : Repository<ProductEntity, Guid>, IProductRepository
{

    public async Task<IEnumerable<ProductEntity>> GetProductsByStudioIdAsync(Guid studioId)
    {
        return BeginQueryReadOnly().Where(studio => studio.Id.Equals(studioId));
    }

    public async Task<ProductEntity?> GetProductByIdAndStudioIdAsync(Guid productId, Guid studioGuid)
    {
        return BeginQueryReadOnly().FirstOrDefault(product =>
            product.Id.Equals(productId) /*&& product.OwnerStudio.Id.Equals(studioGuid)*/
        );
    }

    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }
}
