namespace Mail.Domain.Template;

public class BanOnSending
{
    public string TemplateName { get; set; } = null!;
    
    public Guid UserId { get; set; }

    public Template Template { get; set; } = null!;

    public User.User User { get; set; } = null!;
}