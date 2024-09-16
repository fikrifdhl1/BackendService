using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace BackendService.Utils
{
    public class CustomeHash : ICustomeHash
    {
        private readonly PasswordHasher<string> _hasher;
        public CustomeHash()
        {
            _hasher = new PasswordHasher<string>();
        }
        public bool Compare(string password, string hashedPassword)
        {
            var result = _hasher.VerifyHashedPassword(null, hashedPassword, password);
            return result == PasswordVerificationResult.Success;
        }

        public string Hash(string password)
        {
            return _hasher.HashPassword(null,password);
        }
    }
}
