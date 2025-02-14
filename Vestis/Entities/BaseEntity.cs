using System.ComponentModel.DataAnnotations;

namespace Vestis.Entities
{
    public class BaseEntity<TId> where TId : struct
    {
        [Key]
        public TId Id { get; protected set; }
        [Required]
        public DateTime CreatedDate { get; protected set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
