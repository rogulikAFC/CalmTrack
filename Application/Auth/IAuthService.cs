using Domain.User;

namespace Application.Auth
{
    public interface IAuthService
    {
        string GenerateToken(User user);
    }
}
