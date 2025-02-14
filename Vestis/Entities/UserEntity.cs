using System.ComponentModel.DataAnnotations;

namespace Vestis.Entities
{
    public class UserEntity : BaseEntity<Guid>
    {
        [Required]
        public string Name { get; private set; }
        [Required]
        [EmailAddress]
        public string Email { get; private set; }
        [Required]
        public string Password { get; private set; }
        [Required]
        public string Role { get; private set; }

        public UserEntity(string name, string email, string password, string role)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Password = password;
            Role = role;
            CreatedDate = DateTime.UtcNow;
        }
    }
}
