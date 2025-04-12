namespace Application.Features.DTOs.User
{
    public class CreatedUserDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Role { get; set; } = null!;

        public string Token { get; set; } = null!;

        public static CreatedUserDto MapFromUser(
            Domain.User.User user, string token)
        {
            return new CreatedUserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role.ToString(),
                Token = token
            };
        }
    }
}
