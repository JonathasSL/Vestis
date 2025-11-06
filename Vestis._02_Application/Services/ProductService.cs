using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Vestis._02_Application.Common;
using Vestis._02_Application.Models;
using Vestis._02_Application.Services.Interfaces;
using Vestis._03_Domain.Entities;
using Vestis._04_Infrasctructure.Repositories.Interfaces;

namespace Vestis._02_Application.Services;

internal class ProductService : CRUDService<ProductModel, ProductEntity, Guid>, IProductService
{
    private IEnumerable<ProductModel> testProducts;

    public IEnumerable<ProductModel> GetProductsByStudioWithFilters(Guid studioId, Dictionary<string, string>? filters)
    {
        //var inventoryModelList = _mapper.Map<IEnumerable<ProductModel>>(
        //    _repository.GetProductsByStudioIdAsync(studioId).Result
        //);

        //var inventoryModelList = _mapper.Map<IEnumerable<ProductModel>>(testProducts);
        IEnumerable<ProductModel> result = new List<ProductModel>();
        if (filters.Any())
        {
            foreach (var filter in filters)
            {
                switch (filter.Key.ToLower())
                {
                    case "name":
                        result = result.Concat(testProducts.Where(p => p.Name != null && p.Name.Contains(filter.Value, StringComparison.OrdinalIgnoreCase)));
                        break;
                    case "category":
                        result = result.Concat(testProducts.Where(p => p.Category != null && p.Category.Equals(filter.Value, StringComparison.OrdinalIgnoreCase)));
                        break;
                    case "minprice":
                        if (decimal.TryParse(filter.Value, out var minPrice))
                            result = result.Concat(testProducts.Where(p => p.Price.HasValue && p.Price.Value >= minPrice));
                        break;
                    case "maxprice":
                        if (decimal.TryParse(filter.Value, out var maxPrice))
                            result = result.Concat(testProducts.Where(p => p.Price.HasValue && p.Price.Value <= maxPrice));
                        break;
                
                }
            }
        }
        else
        {
            return testProducts;
        }

        return result;
    }

    public ProductModel GetProductByStudio(Guid productId, Guid studioGuid)
    {
        return _mapper.Map<ProductModel>(
            _repository.GetProductByIdAndStudioIdAsync(productId, studioGuid)
        );
    }

    private IProductRepository _repository;

    public ProductService(
        IMapper mapper,
        IMediator mediator,
        BusinessNotificationContext businessNotificationContext,
        ILogger<ProductService> logger,
        IProductRepository repository) : base(mapper, mediator, businessNotificationContext, logger, repository)
    {
        this._repository = repository;
        

        testProducts = new List<ProductModel>
        {
            new ProductModel
            {
                Id = new Guid("ff364e8a-38c7-4cf4-bc37-a156b26c08d0"),
                Name = "Classic Navy Suit",
                Description = "Two-piece wool blend suit with a tailored fit and notch lapel. Ideal for formal and business occasions.",
                Category = "Suits",
                Price = 249.99M,
                UnitCount = 15,
                ImgUrl = "https://picsum.photos/800/1200"
            },
            new ProductModel
            {
                Id = new Guid("2ed4895d-919f-421d-ae9d-89ddc5456950"),
                Name = "Evening Gown - Ruby Red",
                Description = "Floor-length chiffon evening gown with a fitted bodice and flowing skirt. Perfect for formal events.",
                Category = "Dresses",
                Price = 299.99M,
                UnitCount = 8,
                ImgUrl = "https://picsum.photos/800/1200"
            },
            new ProductModel
            {
                Id = new Guid("c9a1e2b3-45d6-4f7a-8b1c-111111111111"),
                Name = "Men's White Poplin Shirt",
                Description = "100% cotton poplin shirt with a slim fit and button-down collar. Breathable and easy to care for.",
                Category = "Shirts",
                Price = 49.99M,
                UnitCount = 60,
                ImgUrl = "https://picsum.photos/800/1200"
            },
            new ProductModel
            {
                Id = new Guid("b7a8c9d0-12e3-4b2c-8f01-222222222222"),
                Name = "Women's Chiffon Midi Dress",
                Description = "Lightweight chiffon midi dress with delicate pleats and adjustable straps. Casual elegance for daytime events.",
                Category = "Dresses",
                Price = 89.50M,
                UnitCount = 30,
                ImgUrl = "https://picsum.photos/800/1200"
            },
            new ProductModel
            {
                Id = new Guid("c3d4e5f6-7a81-4c3d-7b02-333333333333"),
                Name = "Slim Fit Chinos",
                Description = "Stretch-cotton chinos with a modern slim silhouette. Versatile for smart-casual outfits.",
                Category = "Trousers",
                Price = 59.99M,
                UnitCount = 40,
                ImgUrl = "https://picsum.photos/800/1200"
            },
            new ProductModel
            {
                Id = new Guid("d4e5f6a7-8b92-4d4e-6c03-444444444444"),
                Name = "Wool Overcoat - Charcoal",
                Description = "Mid-length wool overcoat with a warm insulated lining and classic tailored shape.",
                Category = "Coats",
                Price = 199.99M,
                UnitCount = 12,
                ImgUrl = "https://picsum.photos/800/1200"
            },
            new ProductModel
            {
                Id = new Guid("e5f6a7b8-9ca3-4e5f-5d04-555555555555"),
                Name = "Casual Linen Blazer",
                Description = "Unstructured linen blazer with a relaxed fit — perfect for summer events and smart-casual looks.",
                Category = "Blazers",
                Price = 129.99M,
                UnitCount = 25,
                ImgUrl = "https://picsum.photos/800/1200"
            },
            new ProductModel
            {
                Id = new Guid("f6a7b8c9-0db4-4f6a-4e05-666666666666"),
                Name = "Pleated Midi Skirt",
                Description = "High-waist pleated skirt in a soft fabric that drapes beautifully for day-to-night styling.",
                Category = "Skirts",
                Price = 39.99M,
                UnitCount = 30,
                ImgUrl = "https://picsum.photos/800/1200"
            },
            new ProductModel
            {
                Id = new Guid("07b8c9d0-1ec5-407b-3f06-777777777777"),
                Name = "Silk Tie - Navy",
                Description = "100% silk tie with a subtle texture. A refined accessory for formal wear.",
                Category = "Accessories",
                Price = 29.99M,
                UnitCount = 100,
                ImgUrl = "https://picsum.photos/800/1200"
            },
            new ProductModel
            {
                Id = new Guid("18c9d0e1-2fd6-418c-2a07-888888888888"),
                Name = "Leather Oxford Shoes",
                Description = "Classic cap-toe oxford crafted from full-grain leather with a leather sole and cushioned insole.",
                Category = "Shoes",
                Price = 139.99M,
                UnitCount = 20,
                ImgUrl = "https://picsum.photos/800/1200"
            },
            new ProductModel
            {
                Id = new Guid("29d0e1f2-30e7-429d-1b08-999999999999"),
                Name = "Denim Jacket",
                Description = "Durable denim jacket with a regular fit and classic button front. A wardrobe staple for casual styles.",
                Category = "Jackets",
                Price = 89.99M,
                UnitCount = 35,
                ImgUrl = "https://picsum.photos/800/1200"
            },
            new ProductModel
            {
                Id = new Guid("3ae1f203-41f8-43ae-0c09-aaaaaaaaaaaa"),
                Name = "Cashmere Crewneck Sweater",
                Description = "Soft cashmere sweater with a classic crewneck — luxurious comfort for cooler days.",
                Category = "Sweaters",
                Price = 179.00M,
                UnitCount = 18,
                ImgUrl = "https://picsum.photos/800/1200"
            }
        };
    }
}
