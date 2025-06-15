namespace UserMessages
{
    public class CreateUserMessage
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Role { get; set; } = null!;
    }
}
