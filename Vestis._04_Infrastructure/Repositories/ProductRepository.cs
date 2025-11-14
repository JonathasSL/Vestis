using Vestis._03_Domain.Entities;
using Vestis._04_Infrasctructure.Data;
using Vestis._04_Infrasctructure.Repositories.Interfaces;

namespace Vestis._04_Infrasctructure.Repositories;

internal class ProductRepository : Repository<ProductEntity, Guid>, IProductRepository
{
    //private ProductEntity[] testProducts = new [] {
    //    new ProductEntity
    //    (
    //        //Id = "17238392-2E77-6479-6D34-B6260980A75D",
    //        "Keaton Shepherd",
    //        "vitae",
    //        "Camisas",
    //        140.05,
    //        2,
    //        "https://instagram.com?ab=441&aad=2"
    //    )
    //    new ProductEntity
    //    {
    //        //Id = "24015A4C-3425-4266-5612-17C8E61D181D",
    //        Name = "Anne Alston",
    //        Description = "vel pede blandit congue. In scelerisque scelerisque dui.",
    //        Category = "sociais",
    //        Price = "R$136.97",
    //        UnitCount = 7,
    //        ImgUrl = "https://yahoo.com?p=8"
    //    },
    //    new ProductEntity
    //    {
    //        //Id = "C0578746-E87A-7E33-1B45-1DAEC9E53FD6",
    //        Name = "Russell Watson",
    //        Description = "erat. Sed nunc est, mollis non, cursus",
    //        Category = "Calças",
    //        Price = "R$68.49",
    //        UnitCount = 9,
    //        ImgUrl = "https://guardian.co.uk?gi=100"
    //    },
    //    new ProductEntity
    //    {
    //        //Id = "78B887CD-3819-2FDC-B46E-71E631BBAEC1",
    //        Name = "Montana Ramsey",
    //        Description = "ornare tortor at risus. Nunc ac sem",
    //        Category = "Camisas",
    //        Price = "R$293.81",
    //        UnitCount = 2,
    //        ImgUrl = "http://whatsapp.com?search=1&q=de"
    //    },
    //    new ProductEntity
    //    {
    //        //Id = "45C88458-FE2D-CA4C-A4AC-A284C6CDB842",
    //        Name = "Winter Sharp",
    //        Description = "at, egestas",
    //        Category = "Camisas",
    //        Price = "R$52.42",
    //        UnitCount = 5,
    //        ImgUrl = "https://naver.com?search=1&q=de"
    //    }
    //};

    public async Task<IEnumerable<ProductEntity>> GetProductsByStudioIdAsync(Guid studioId)
    {
        //var products = testProducts.Where(p => p.Id.Equals(studioId));
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
