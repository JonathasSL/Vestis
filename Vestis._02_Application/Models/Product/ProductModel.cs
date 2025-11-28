namespace Vestis._02_Application.Models.Product;

public class ProductModel : BaseModel<Guid>
{
    public Guid StudioId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public double? Price { get; set; }
    public int? UnitCount { get; set; }
    public string? ImgUrl { get; set; }
}
