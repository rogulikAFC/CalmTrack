namespace Mail.Application.Exceptions;

public class TemplateNotFound(string name)
    : Exception($"Template with name {name} not found.");