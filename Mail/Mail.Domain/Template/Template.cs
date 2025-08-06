namespace Mail.Domain.Template;

public class Template
{
    public string Name { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string TemplateFileName { get; set; } = null!;

    public IEnumerable<BanOnSending> BansOnSending = [];

    public bool HasSendingToUserAllowed(User.User user)
    {
        var hasBanOnSending = BansOnSending
            .Any(bos => bos.User == user);

        return !hasBanOnSending;
    }
}