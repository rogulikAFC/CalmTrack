using Application.Auth;
using Domain.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;

        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(
                Environment.GetEnvironmentVariable("JWT_SECRET")!);

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

            var claims = new ClaimsIdentity(
                [
                    new Claim("sub", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                ]);

            var token = handler.CreateToken(
                new SecurityTokenDescriptor
                {
                    SigningCredentials = credentials,
                    Expires = DateTime.UtcNow.AddMinutes(15),
                    Subject = claims,
                    Issuer = _config["JwtSettings:Issuer"],
                    Audience = _config["JwtSettings:Audience"]
                });

            return handler.WriteToken(token);
        }
    }
}
