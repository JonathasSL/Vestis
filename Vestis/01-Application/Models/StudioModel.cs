using Vestis.Application.Models;

namespace Vestis._01_Application.Models
{
    public class StudioModel : BaseModel<Guid>
    {
        public string Name { get; set; }

        public string ContactEmail { get; set; }

        public string PhoneNumber { get; set; }

    }
}
