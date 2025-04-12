namespace Domain.User
{
    public class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string FullName => FirstName + " " + LastName;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public Guid RoleId { get; set; }

        public Role Role { get; set; } = null!;

        public override string ToString() =>
            $"{FullName} ({Id})";

        public User(
            string firstName, string lastName,
            string email, Guid roleId)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            RoleId = roleId;
        }
    }
}
