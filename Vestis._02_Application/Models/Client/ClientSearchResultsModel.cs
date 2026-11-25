namespace Vestis._02_Application.Models.Client;

public class ClientSearchResultsModel : BaseModel<Guid>
{
    public string Name { get; set; }
    public string ContactInfo { get; set; }
    public DateOnly ClientSince { get; set; }
    public long TotalPurchases { get; set; } = 0;
}
