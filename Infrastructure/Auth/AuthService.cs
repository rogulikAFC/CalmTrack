using Application.Auth;
using Domain.User;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Auth
{
    public class AuthService : IAuthService
    {
        public string GenerateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes("secret-key secret-key secret-key"); // Test key

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

            var claims = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                ]);

            var token = handler.CreateToken(
                new SecurityTokenDescriptor
                {
                    SigningCredentials = credentials,
                    Expires = DateTime.UtcNow.AddMinutes(15),
                    Subject = claims
                });

            return handler.WriteToken(token);
        }
    }
}
