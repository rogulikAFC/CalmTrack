using Mail.Application.UnitOfWork.Repositories;
using Mail.Domain.Template;
using Microsoft.EntityFrameworkCore;

namespace Mail.Infrastructure.Persistence.UnitOfWork.Repositories;

public class TemplateRepository(MailDbContext context)
    : ITemplateRepository
{
    public void AddTemplate(Template template)
    {
        context.Templates.Add(template);
    }

    public void RemoveTemplate(Template template)
    {
        context.Templates.Remove(template);
    }

    public Task<Template?> GetTemplateByNameAsync(string name)
    {
        return context.Templates
            .FirstOrDefaultAsync(t => t.Name == name);
    }
}