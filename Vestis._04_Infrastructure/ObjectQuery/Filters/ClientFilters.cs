namespace Vestis._04_Infrastructure.ObjectQuery.Filters;

public class ClientFilters
{
    public Guid StudioId { get; set; }
    public string? SearchTerm { get; set; }
    public DateOnly? ClientSinceStart { get; set; }
    public DateOnly? ClientSinceEnd { get; set; }
    public long? MinTotalPurchases { get; set; }
    public long? MaxTotalPurchases { get; set; }
}
