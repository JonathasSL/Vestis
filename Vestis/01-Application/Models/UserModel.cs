using Vestis.Application.Models;

namespace Vestis._01___Application.Models
{
    public class UserModel : BaseModel<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
