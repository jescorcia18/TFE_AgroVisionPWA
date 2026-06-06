using System.Security.Cryptography;
using System.Text;

namespace CoffeePestDetection.Infrastructure.Security
{
    public static class PasswordHasher
    {
        public static string Hash(string password)
        {
            //using var sha256 = SHA256.Create();
            //var bytes = Encoding.UTF8.GetBytes(password);
            //return Convert.ToBase64String(sha256.ComputeHash(bytes));
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool Verify(string password, string hash)
        {
            //return Hash(password) == hash;
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
