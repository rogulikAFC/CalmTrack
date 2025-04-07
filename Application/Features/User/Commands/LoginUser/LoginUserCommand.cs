using MediatR;

namespace Application.Features.User.Commands.LoginUser
{
    public record LoginUserCommand(Guid UserId, string Password)
        : IRequest<string>;
}
