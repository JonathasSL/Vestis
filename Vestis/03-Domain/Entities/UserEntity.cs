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

        public UserEntity(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        //Constructor for EF
        public UserEntity() { }

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty.");
            else if (Name != name)
            {
                Name = name;
                SetAsUpdated();
            }
        }
        public void ChangeEmail(string email)
        {
            if (Email != email)
            {
                Email = email;
                SetAsUpdated();
            }
        }
        public void ChangePassword(string password)
        {
            if (Password != password)
            {
                Password = password;
                SetAsUpdated();
            }
        }
    }
}
