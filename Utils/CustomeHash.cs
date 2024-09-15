using System.Security.Cryptography;
using System.Text;

namespace BackendService.Utils
{
    public class CustomeHash : ICustomeHash
    {
        private readonly string _staticSalt;
        private readonly int _iteration = 10000;
        private readonly int _keySize = 64;
        public CustomeHash(IConfiguration configuration)
        {
            _staticSalt = configuration["Hash:Salt"];
        }
        public bool Compare(string password, string hashedPassword)
        {
            var salt = Encoding.UTF8.GetBytes(_staticSalt);
            var comparePassword = Rfc2898DeriveBytes.Pbkdf2(password, salt, _iteration, HashAlgorithmName.SHA512, _keySize);
            return CryptographicOperations.FixedTimeEquals(comparePassword,Encoding.UTF8.GetBytes(hashedPassword));
        }

        public string Hash(string password)
        {
            var salt = Encoding.UTF8.GetBytes(_staticSalt);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, _iteration, HashAlgorithmName.SHA512,_keySize);

            return Encoding.UTF8.GetString(hash);
        }
    }
}
