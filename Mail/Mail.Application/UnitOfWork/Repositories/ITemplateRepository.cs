using Mail.Domain.Template;

namespace Mail.Application.UnitOfWork.Repositories;

public interface ITemplateRepository
{
    void AddTemplate(Template template);

    void RemoveTemplate(Template template);

    Task<Template?> GetTemplateByNameAsync(string name);
}