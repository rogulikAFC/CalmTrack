using System.Net;
using System.Net.Mail;
using Mail.Application.Exceptions;
using Mail.Application.MailRendering;
using Mail.Application.MailSending;
using Mail.Application.UnitOfWork;
using Mail.Domain.User;
using Mail.Infrastructure.MailRendering;
using Microsoft.Extensions.Logging;
using UserMessages;

namespace Mail.Infrastructure.MailSending;

public class MailSendingService
    : IMailSendingService
{
    private readonly IMailRenderer _mailRenderer;

    private readonly IUnitOfWork _unitOfWork;
    
    private readonly ILogger<MailSendingService> _logger;

    private readonly SmtpClient _smtpClient;
    
    private readonly string _smtpFromEmail = Environment.GetEnvironmentVariable("SMTP_FROM_EMAIL")
        ?? throw new Exception("SMTP_FROM_EMAIL is required.");

    public MailSendingService(IUnitOfWork unitOfWork, ILogger<MailSendingService> logger)
    {
        // Initialize Unit of work
        _unitOfWork = unitOfWork;

        // Initialize SMTP client
        var smtpUsername = Environment.GetEnvironmentVariable("SMTP_USERNAME");
        
        var smtpPassword = Environment.GetEnvironmentVariable("SMTP_PASSWORD");
        
        var smtpServer = Environment.GetEnvironmentVariable("SMTP_SERVER");
        
        var smtpPort = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT")!);

        _smtpClient = new SmtpClient
        {
            Host = smtpServer!,
            Port = smtpPort,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(
                smtpUsername, smtpPassword)
        };
        
        // Initialize logger
        _logger = logger;
        
        // Initialize template renderer
        var templatesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MailTemplates");
        
        _mailRenderer = new MailRenderer(templatesPath);
    }
    
    /**    
     * <exception cref="TemplateNotFound">The template was not found by template name that is set in template entity.</exception>
     * <inheritdoc/>
     */
    public async Task SendUserCreatedMailAsync(CreateUserMessage createUserMessage)
    {
        // Create user
        var user = new User(
            createUserMessage.Id, createUserMessage.FirstName, 
            createUserMessage.LastName, createUserMessage.Email);

        _unitOfWork.Users.AddUser(user);
        
        await _unitOfWork.SaveChangesAsync();
        
        // Render template
        var templateEntity = await _unitOfWork.Templates
            .GetTemplateByNameAsync("UserCreated");

        if (templateEntity == null)
        {
            throw new TemplateNotFound("UserCreated");
        }
        
        var messageBody = await _mailRenderer.RenderUserCreatedMailAsync(
            templateEntity.TemplateFileName, createUserMessage);


        var mailMessage = new MailMessage(
            _smtpFromEmail, user.Email,
            "CalmTrack account created", messageBody)
        {
            IsBodyHtml = true
        };

        await _smtpClient.SendMailAsync(mailMessage);
    
        _logger.LogInformation(
            "The email about user account created sent to {recipient}", user.Email);
    }

    /**
     * <exception cref="TemplateNotFound">The template was not found by template name that is set in template entity.</exception>
     * <exception cref="UserNotFound">User with specified ID was not found</exception>
     * <inheritdoc/>
     */
    public async Task SendUserDeletedMailAsync(Guid userId)
    {
        // Remove user
        var user = await _unitOfWork.Users
            .GetUserByIdAsync(userId);

        if (user == null)
        {
            throw new UserNotFound(userId);
        }
        
        _unitOfWork.Users.RemoveUser(user);

        await _unitOfWork.SaveChangesAsync();
        
        // Render template
        var templateEntity = await _unitOfWork.Templates
            .GetTemplateByNameAsync("UserDeleted");
        
        if (templateEntity == null)
        {
            throw new TemplateNotFound("UserDeleted");
        }

        var messageBody = await _mailRenderer.RenderUserDeletedMailAsync(
            templateEntity.TemplateFileName, userId);
        
        var mailMessage = new MailMessage(
            _smtpFromEmail, user.Email,
            "CalmTrack account created", messageBody)
        {
            IsBodyHtml = true
        };
        
        await _smtpClient.SendMailAsync(mailMessage);
        
        _logger.LogInformation(
            "The email about user account deleted sent to {recipient}", user.Email);
    }
}