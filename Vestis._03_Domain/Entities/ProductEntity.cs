using System.Security.Cryptography.X509Certificates;

namespace Vestis._03_Domain.Entities;

public class ProductEntity : BaseEntity<Guid>
{
    //StudioEntity OwnerStudio { get; }
    public string Name { get; }
    public string? Description { get; }
    public string? Category { get; }
    public double? Price { get; }
    public int UnitCount { get; }
    public string? ImgUrl { get; }

    //List<ProductUnitEntity> InventoryList { get; }

    public ProductEntity(
        //StudioEntity ownerStudio,
        string name,
        string? description,
        string? category,
        double? price,
        int unitCount,
        string imgUrl)
    {
        //this.OwnerStudio = ownerStudio;
        this.Name = name;
        this.Description = description;
        this.Category = category;
        this.Price = price;
        this.UnitCount = unitCount;
        //this.InventoryList = new ();
        this.ImgUrl = imgUrl;
    }

    [Obsolete("For ORM use only")]
    public ProductEntity() { }
}
