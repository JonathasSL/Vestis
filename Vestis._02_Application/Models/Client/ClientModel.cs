namespace Vestis._02_Application.Models.Client;

public class ClientModel : BaseModel<Guid>
{
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public AddressModel? Address { get; set; }
}
