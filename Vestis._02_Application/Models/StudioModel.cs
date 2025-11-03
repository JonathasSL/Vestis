namespace Vestis._02_Application.Models;

public class StudioModel : BaseModel<Guid>
{
    public string? Name { get; set; }
    public string? ContactEmail { get; set; }
    public string? PhoneNumber { get; set; }
    public AddressModel? Address { get; set; }

}
