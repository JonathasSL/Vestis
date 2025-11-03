namespace Vestis._03_Domain.Entities;

public class ProductEntity : BaseEntity<Guid>
{
    //StudioEntity OwnerStudio { get; }
    string Name { get; }
    string? Description { get; }
    string? Category { get; }
    decimal? Price { get; }
    int UnitCount { get; }
    string? ImgUrl { get; }

    //List<ProductUnitEntity> InventoryList { get; }

    public ProductEntity(
        //StudioEntity ownerStudio,
        string name,
        string? description,
        string? category,
        decimal? price,
        int unitCount)
    {
        //this.OwnerStudio = ownerStudio;
        this.Name = name;
        this.Description = description;
        this.Category = category;
        this.Price = price;
        this.UnitCount = unitCount;
        //this.InventoryList = new ();
    }

    [Obsolete("For ORM use only", true)]
    public ProductEntity() { }
}
