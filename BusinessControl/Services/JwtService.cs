using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessControlService.Services
{
    public class JwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateJwt(string userId, string email)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, email)
            }),

                Expires = DateTime.UtcNow.AddHours(2),

                SigningCredentials =
                    new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
            };

            var token = handler.CreateToken(descriptor);

            return handler.WriteToken(token);
        }
    }
}
