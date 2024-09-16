using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace BPKBBackend.Utils
{

    public interface IJwtHelper
    {
        string GenerateToken(string username,int id);
    }
    public class JwtHelper : IJwtHelper
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly int _expiration;

        public JwtHelper(IConfiguration configuration)
        {
            _secretKey = configuration["Jwt:Key"];
            _issuer = configuration["Jwt:Issuer"];
            _expiration = Convert.ToInt32(configuration["Jwt:Expired"]);
        }

        public string GenerateToken(string username,int id)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Sid,id.ToString()),
                }),
                Issuer = _issuer,
                Expires = DateTime.Now.AddMinutes(_expiration),
                Audience = _issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = handler.CreateToken(tokenDesc);
            return handler.WriteToken(token);
        }
    }
}
