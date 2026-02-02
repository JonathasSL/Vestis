namespace Vestis._03_Domain.Entities;

public class ProductEntity : BaseEntity<Guid>
{
    public Guid StudioId { get; private set; }
    public virtual StudioEntity Studio { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string? Category { get; private set; }
    public double? Price { get; private set; }
    public int UnitCount { get; private set; }
    public string? ImgUrl { get; private set; }
    public virtual ISet<ProductUnitEntity> ProductUnits { get; private set; } = new HashSet<ProductUnitEntity>();

    public ProductEntity(
        StudioEntity studio,
        string name,
        string? category,
        string? description = null,
        double? price = 0,
        int unitCount = 1,
        string? imgUrl = null)
    {
        this.Studio = studio;
        this.StudioId = studio.Id;
        this.Name = name;
        this.Description = description;
        this.Category = category;
        this.Price = price;
        this.UnitCount = unitCount;
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
