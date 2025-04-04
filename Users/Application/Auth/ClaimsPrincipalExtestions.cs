using System.Security.Claims;

namespace Application.Auth
{
    public static class ClaimsPrincipalExtestions
    {
        public static Guid GetUserId(
            this ClaimsPrincipal? principal)
        {
            var stringUserId = principal?
                .FindFirst(ClaimTypes.NameIdentifier)?
                .Value;

            return Guid.TryParse(
                stringUserId,
                out Guid parsedUserId) ?
                parsedUserId :
                throw new Exception(
                    "User ID is unavailable");
        }
    }
}
