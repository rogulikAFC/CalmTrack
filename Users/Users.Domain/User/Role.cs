namespace Domain.User
{
    public class Role
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public List<User> Users = [];

        public override string ToString() => Name;

        public Role(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Role(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
