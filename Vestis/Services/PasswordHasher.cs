using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Vestis.Services
{
    public class PasswordHasher
    {
        public string Hash(string password)
        {
            byte[] salt = new byte[16];
            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            byte[] hash = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, 100000, 32);

            return Convert.ToBase64String(salt) + "." + Convert.ToBase64String(hash);
            //return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string password, string storedHash)
        {
            var parts = storedHash.Split('.');
            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] storedHashBytes = Convert.FromBase64String(parts[1]);

            byte[] hash = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, 100000, 32);

            return hash.SequenceEqual(storedHashBytes);
            //return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
