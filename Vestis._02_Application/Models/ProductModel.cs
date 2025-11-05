namespace Vestis._02_Application.Models;

public class ProductModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public decimal? Price { get; set; }
    public int UnitCount { get; set; }
    public string? ImgUrl { get; set; }
}
