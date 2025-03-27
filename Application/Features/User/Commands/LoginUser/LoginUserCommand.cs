using MediatR;

namespace Application.Features.User.Commands.LoginUser
{
    public record LoginUserCommand(Guid userId, string password)
        : IRequest<string>;
}
