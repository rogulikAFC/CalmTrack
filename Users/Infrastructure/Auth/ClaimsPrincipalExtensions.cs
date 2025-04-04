using System.Security.Claims;

namespace Infrastructure.Auth
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            var stringId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (stringId == null)
            {
                throw new Exception("Name identifier claim not found.");
            }

            return Guid.Parse(stringId);
        }
    }
}
