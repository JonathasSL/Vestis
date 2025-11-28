using Vestis._03_Domain.Entities;
using Vestis._04_Infrasctructure.Data;
using Vestis._04_Infrasctructure.ObjectQuery;
using Vestis._04_Infrasctructure.Repositories.Interfaces;

namespace Vestis._04_Infrasctructure.Repositories;

internal class ProductRepository : Repository<ProductEntity, Guid>, IProductRepository
{
    public async Task<List<ProductEntity>> GetProductsByStudioIdAsync(ProductFilters filters, CancellationToken cancellationToken)
    {
        var query = BeginQueryReadOnly();
        //query = query.Where(p => p.StudioId.equals(filters.studioId));
        
        if (!string.IsNullOrWhiteSpace(filters.Name))
            query = query.Where(p => p.Name.Contains(filters.Name));

        if (!string.IsNullOrWhiteSpace(filters.Category))
            query = query.Where(p => p.Category == filters.Category);

        if (filters.MinPrice.HasValue)
            query = query.Where(p => p.Price >= filters.MinPrice);

        if (filters.MaxPrice.HasValue)
            query = query.Where(p => p.Price <= filters.MaxPrice);


        return query.ToList();
    }

    public async Task<ProductEntity?> GetProductByIdAndStudioIdAsync(Guid productId, Guid studioGuid)
    {
        return BeginQueryReadOnly()
           .FirstOrDefault(product => productId.Equals(product.Id));       
    }

    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }
}
