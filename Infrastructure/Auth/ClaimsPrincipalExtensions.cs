using System.Security.Claims;

namespace Infrastructure.Auth
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            var stringId = principal.FindFirstValue("sub");

            if (stringId == null)
            {
                throw new Exception("sub claim not found.");
            }

            return Guid.Parse(stringId);
        }
    }
}
