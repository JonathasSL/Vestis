using Vestis.Application.Models;

namespace Vestis._01_Application.Models
{
    public class BusinessModel : BaseModel<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
