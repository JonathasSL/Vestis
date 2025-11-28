namespace Vestis._04_Infrasctructure.ObjectQuery;

public class ProductFilters
{
    public Guid StudioId { get; set; }
    public string? Name { get; set; }
    public string? Category { get; set; }
    public double? MinPrice { get; set; }
    public double? MaxPrice { get; set; }
}