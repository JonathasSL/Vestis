namespace Vestis._03_Domain.Entities;

public class ProductEntity : BaseEntity<Guid>
{
    //StudioEntity OwnerStudio { get; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string? Category { get; private set; }
    public double? Price { get; private set; }
    public int UnitCount { get; private set; }
    public string? ImgUrl { get; private set; }

    //Lazy<List<ProductUnitEntity>> InventoryList { get; }

    public ProductEntity(
        //StudioEntity ownerStudio,
        string name,
        string? category,
        string? description = null,
        double? price = 0,
        int unitCount = 1,
        string? imgUrl = null)
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

    public void UpdateAmmount(int quantity)
    {
        if (quantity < 0 && Math.Abs(quantity) > this.UnitCount)
            throw new InvalidOperationException("Insufficient stock to reduce by the specified quantity.");
        this.UnitCount += quantity;
    }
}
