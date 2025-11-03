using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.Models;

public class StudioMembershipModel
{
    public Guid UserId { get; set; }
    public Guid StudioId { get; set; }
    public string? Role { get; set; }
}
