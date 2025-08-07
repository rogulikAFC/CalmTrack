using Mail.Domain.Template;

namespace Mail.Domain.User;
    
public class User
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string FullName => FirstName + " " + LastName;

    public string Email { get; set; } = null!;

    public ICollection<BanOnSending> BansOnSending { get; set; } = [];

    public override string ToString() =>
        $"{FullName} ({Id})";

    public User(Guid id, string firstName, string lastName, string email)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
}

